using BankService.DTO;
using BankService.Interfaces;
using BankService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public BanksController(IBanksService banksService, ICardService cardService, IAccountService accountService, ITransactionService transactionService, IPSPService pspService)
        {
            _banksService = banksService;
            _cardService = cardService;
            _accountService = accountService;
            _transactionService = transactionService;
            _pspService = pspService;
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

                    if (await _accountService.WithdrawMoney(card.Account!.UserId, transaction.Amount))
                    {
                        await _accountService.DepositMoney(transaction.IdMerchant, transaction.Amount);
                        responseDTO = await _banksService.SendToPSP(cardInfoDTO, card.Account!.UserId, transaction);
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
                    var acquirerId = await _accountService.GetAccountId(response.AcquirerAccountNumber!);

                    if (await _accountService.DepositMoney(acquirerId, transaction.Amount))
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
        public async Task<IActionResult> WithdrawMoneyFromIssuer([FromBody]PCCRequestDTO pccRequestDTO)
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

            if (_banksService.IsSameBank(pccRequestDTO.Pan!))
            {                
                Card? card = await _cardService.CheckCardInfo(cardInfo);
                if (await _accountService.WithdrawMoney(card.Account!.UserId, pccRequestDTO.Amount))
                {
                    responseDTO = await _banksService.ResendToPCC(pccRequestDTO, card.Account!.AccountNumber!, card.Account!.UserId);
                    return Ok(responseDTO);
                }
            }

            return BadRequest(responseDTO);
        }
    }
}
