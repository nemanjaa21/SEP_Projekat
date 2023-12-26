using BankService.DTO;
using BankService.Enums;
using BankService.Interfaces;
using BankService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace BankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBanksService _banksService;
        private readonly ICardService _cardService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IPSPService _pspService;
        private readonly IMerchantService _merchantService;

        public BanksController(IBanksService banksService, ICardService cardService, IAccountService accountService, ITransactionService transactionService, IPSPService pspService, IMerchantService merchantService)
        {
            _banksService = banksService;
            _cardService = cardService;
            _accountService = accountService;
            _transactionService = transactionService;
            _pspService = pspService;
            _merchantService = merchantService;
        }

        [HttpPost("pay-with-card")]
        public async Task<IActionResult> PayWithCard(CardInfoDTO cardInfoDTO)
        {
            PSPResponseDTO responseDTO;

            try
            {
                Transaction? transaction = await _transactionService.GetByPaymentId(cardInfoDTO.PaymentId!);
                if (_banksService.IsSameBank(cardInfoDTO.Pan!))
                {
                    Card? card = await _cardService.CheckCardInfo(cardInfoDTO);

                    if (await _accountService.WithdrawMoney(card.Account!.UserId!.Value, transaction.Amount))
                    {
                        await _accountService.DepositMoney(transaction.IdMerchant, transaction.Amount);
                        responseDTO = await _banksService.SendToPSP(card.Account!.UserId!.Value, transaction);
                        return Ok(responseDTO);
                    }
                    else
                    {
                        responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.FAILED);
                        return BadRequest(responseDTO);
                    }
                }
                else
                {
                    PCCResponseDTO response = await _banksService.SendToPCC(cardInfoDTO, transaction);

                    if (await _accountService.DepositMoneyViaAccount(response.AcquirerAccountNumber!, transaction.Amount))
                    {
                        responseDTO = await _banksService.SendToPSPFromPCC(response);
                        return Ok(responseDTO);
                    }

                    responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.FAILED);
                    return BadRequest(responseDTO);
                }
            }
            catch (SystemException)
            {
                responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.FAILED);
                return BadRequest(responseDTO);
            }
            catch (Exception)
            {
                responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.ERROR);
                return BadRequest(responseDTO);
            }
        }

        [HttpPost("withdraw-money")]
        public async Task<IActionResult> WithdrawMoneyFromIssuer([FromBody] PCCRequestDTO pccRequestDTO)
        {
            CardInfoDTO cardInfo = new CardInfoDTO()
            {
                Pan = pccRequestDTO.Pan,
                CardHolderName = pccRequestDTO.CardHolderName,
                PaymentId = pccRequestDTO.PaymentId,
                ExpirationDate = pccRequestDTO.ExpirationDate,
                SecurityCode = pccRequestDTO.SecurityCode,
            };
            PCCResponseDTO responseDTO = new PCCResponseDTO();

            Card? card = await _cardService.CheckCardInfo(cardInfo);
            if (await _accountService.WithdrawMoney(card.Account!.UserId!.Value, pccRequestDTO.Amount))
            {
                responseDTO = await _banksService.ResendToPCC(pccRequestDTO, card.Account!.AccountNumber!, card.Account!.UserId!.Value);
                return Ok(responseDTO);
            }

            return BadRequest(responseDTO);
        }

        [HttpPost("pay-with-qr-code")]
        public async Task<IActionResult> PayWithQRCode([FromBody] QRCodeDataDTO QRCodeDataDTO)
        {
            PSPResponseDTO responseDTO;

            try
            {
                Transaction? transaction = await _transactionService.GetByPaymentId(QRCodeDataDTO.PaymentId!);
                transaction.Amount = ConvertToUSD(QRCodeDataDTO.Currency, transaction.Amount);

                if (await _accountService.WithdrawMoneyViaAccount(QRCodeDataDTO.UserAccount!, transaction.Amount))
                {
                    await _accountService.DepositMoneyViaAccount(QRCodeDataDTO.MerchantAccount!, transaction.Amount);
                    responseDTO = await _banksService.UpdateTransaction(QRCodeDataDTO.UserAccount!, QRCodeDataDTO.MerchantAccount!, QRCodeDataDTO.UserId ,transaction);
                    return Ok(responseDTO);
                }
                else
                {
                    responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.FAILED);
                    return BadRequest(responseDTO);
                }
            }
            catch (SystemException)
            {
                responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.FAILED);
                return BadRequest(responseDTO);
            }
            catch (Exception)
            {
                responseDTO = _pspService.GenerateResponseBasedOnURL(Enums.Url.ERROR);
                return BadRequest(responseDTO);
            }
        }

        //Dobiti informacije o trenutnom kupcu i prodavcu a zatim generisati QR Code. i poslati sliku na front-end.
        [HttpPost("generate-qr-code")]
        public async Task<IActionResult> GenerateQRCode(GenerateQRCodeDTO generateQRCodeDTO)
        {
            var accountMerchant = await _accountService.GetAccountNumberByMerchant(generateQRCodeDTO.MerchantId);
            var accountUser = await _accountService.GetAccountNumberByUser(generateQRCodeDTO.UserId);
            if (accountUser == null || accountMerchant == null)
                return NotFound("There was an error getting account!");

            var merchant = await _merchantService.GetByMerchantId(generateQRCodeDTO.MerchantId);

            string qrText = $"Merchant Full Name: {merchant.FullName}\nMerchant Account: {accountMerchant}\nUser ID: {generateQRCodeDTO.UserId}\nUser Account: {accountUser}\nCurrency: {GetCurrencyString(generateQRCodeDTO.Currency)}\n PaymentID:{generateQRCodeDTO.PaymentId}";

            var qrCodeImage = GenerateQRCodeImage(qrText);
            return Ok(new { ImageBase64 = qrCodeImage });
        }

        private string GenerateQRCodeImage(string qrText)
        {
            QRCodeGenerator qr = new QRCodeGenerator();
            QRCodeData data = qr.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(data);

            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                qrCodeImage.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private string GetCurrencyString(Currency currency)
        {
            if (currency == Currency.USD)
                return "USD";
            else if (currency == Currency.EUR)
                return "EUR";
            else
                return "RSD";
        }

        private decimal ConvertToUSD(Currency currency, decimal amount)
        {
            if (currency == Currency.USD)
                return amount;
            else if (currency == Currency.EUR)
                return amount * (decimal)1.1;
            else
                return amount * (decimal)0.0094;
        }
    }
}