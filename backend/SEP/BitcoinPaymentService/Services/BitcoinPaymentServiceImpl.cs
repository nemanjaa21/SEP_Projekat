using BitcoinPaymentService.DTO;
using BitcoinPaymentService.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;
using System.Numerics;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using Nethereum.Util;

namespace BitcoinPaymentService.Services
{
    public class BitcoinPaymentServiceImpl : IBitcoinPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        private readonly IHelperService _helperService;
        public BitcoinPaymentServiceImpl( IConfiguration configuration, IUnitOfWork unitOfWork, ITransactionService transactionService, IHelperService helperService)
        {
            this._configuration = configuration;
            this._unitOfWork = unitOfWork;
            this._transactionService = transactionService;
            this._helperService = helperService;
        }
        public async Task CancelEthereumPayment(int merchantId)
        {
            var transaction = await _unitOfWork.TransactionsRepository.Get(x => x.Id == merchantId ) ?? throw new Exception("No order");
            _unitOfWork.DeleteTransaction(transaction);
            await _unitOfWork.Save();
        }

        public async Task CheckEthereumPayment(string transactionHash)
        {
            var web3 = new Web3(_configuration["Ethereum:RPC_API"]!);
            var block = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
            var order = await _unitOfWork.TransactionsRepository.Get(x => x.UniqueHash == block.Input)
                ?? throw new Exception("You made wrong transaction.");

            //var sellerAddress = order.ProductKey!.Product!.Seller!.EthereumAddress!;
            //if (string.IsNullOrWhiteSpace(sellerAddress))
            //{
            //    if (!_configuration["Ethereum:Address"]!.ToLower().Contains(block.To.ToLower()))
            //        throw new Exception("You made wrong transaction.");
            //}
            //else if (!sellerAddress.ToLower().Contains(block.To.ToLower()))
            //{
            //    throw new Exception("You made wrong transaction.");
            //}

            //decimal price = await GetPriceInEth((double)order.Price!);
            //if (UnitConversion.Convert.ToWei(price) > block.Value.Value)
            //    throw new Exception("You made wrong transaction value.");

         }

        public async Task<EthereumPaymentDTO> CreateEthereumPayment(int merchantId, int userId)
        {
            var user = await _unitOfWork.UsersRepository.Get(x => x.Id == userId);

            var merchant = await _unitOfWork.MerchantsRepository.Get(x => x.Id == merchantId )
                ?? throw new Exception("Product doesn't exist");

            var transaction = await _transactionService.MakeTransaction(merchantId, userId);
            transaction.UniqueHash = new BigInteger(new Random().Next()).ToHex(true);
            _unitOfWork.TransactionsRepository.Update(transaction);
            await _unitOfWork.Save();

            decimal price = await GetPriceInEth(_helperService.GetPrice(merchant));

            return new EthereumPaymentDTO { To = user.EthereumAddress ?? _configuration["Ethereum:Address"], Value = UnitConversion.Convert.ToWei(price).ToString(), Input = transaction.UniqueHash!, TransactionId = transaction.Id };

        }

        public async Task<decimal> GetPriceInEth(double price)
        {
            using (var cli = new HttpClient())
            {
                HttpResponseMessage response = await cli.GetAsync(_configuration["Ethereum:ExchangeAPI"]! + "&symbols=ETH");
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Api isn't available, please consider paying with PayPal");

                var str = await response.Content.ReadAsStringAsync()
                    ?? throw new Exception("Api isn't available, please consider paying with PayPal");

                dynamic obj = JsonConvert.DeserializeObject<dynamic>(str)!;
                return decimal.Parse(price.ToString()) / decimal.Parse(obj.rates.ETH.ToString());
            }
        }

        public Task<string> MakePayment()
        {
            string paymentMessage = "Bitcoin Payment was successful!";
            return Task.FromResult(paymentMessage);
        }
    }
}
