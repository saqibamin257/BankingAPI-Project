using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TestBankingAPI.Contracts.CustomerBankAccount;
using TestBankingAPI.Model;
using TestBankingAPI.Contracts.DBModel;
using TestBankingAPI.Services.CustomerBankAccount;

namespace TestBankingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class CustomerBankAccountController : ControllerBase
    {
        private readonly ICustomerBankAccountService customerService;
        public CustomerBankAccountController(ICustomerBankAccountService _customerService) 
        {
            this.customerService = _customerService;
        } 


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerBankAccountResponse>> CustomerAccountDetailsByID(int id)
        {
            try 
            {
                CustomerBankAccount_Details customerDetails = await customerService.CustomerAccountDetailsByID(id);
                if(customerDetails == null) 
                {
                    return NotFound();
                }
                var response = new CustomerBankAccountResponse(
                customerDetails.CustomerID,
                customerDetails.CustomerName,
                customerDetails.Accounts
                );
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAndBankAccount(CreateCustomerBankAccountRequest request) 
        {
            try 
            {
                var customerBankAccountDetails = new CustomerBankAccount_Details(request.CustomerID, request.CustomerName, request.Accounts); 
                await customerService.CreateCustomerAndBankAccount(customerBankAccountDetails);

                var response = new CustomerBankAccountResponse(
                        customerBankAccountDetails.CustomerID,
                        customerBankAccountDetails.CustomerName,
                        customerBankAccountDetails.Accounts);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new customer record");
            }
        }
    }
}
