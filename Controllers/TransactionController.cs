using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TestBankingAPI.Contracts.CustomerBankAccount;
using TestBankingAPI.Contracts.Transaction;
using TestBankingAPI.Model;
using TestBankingAPI.Services.BankTransactions;

namespace TestBankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public TransactionController(ITransactionService _transactionService)
        {
            this.transactionService = _transactionService;
        }
        //public async Task<IActionResult> CreateTransaction(TransactionRequest request) 
        //{
        //    try
        //    {
        //        var transactionDetails = new TransactionDetails(request.TransactionID, request.SenderAccountID, request.RecieverAccountID,0, request.TransactionAmount, 0, DateTime.Now);
        //        await transactionService.CreateTransaction(transactionDetails);
        //        var response = new TransactionResponse(request.SenderAccountID, request.TransactionAmount, request.RecieverAccountID, DateTime.Now);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new transaction record");
        //    }
        //}

        [HttpGet("{acccountId:int}")]
        public async Task<ActionResult> GetTransferHistory(int acccountId)
        {
            try
            {
                if (acccountId != null) 
                {
                    var result = await transactionService.GetTransferHistory(acccountId);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



    }
}
