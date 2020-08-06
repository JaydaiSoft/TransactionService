using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionServices.Logging;
using TransactionServices.Model.ViewModel;
using TransactionServices.Service;

namespace TransactionServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService transactionService;
        private ILogManager logManager;
        public TransactionController(ITransactionService transactionService, ILogManager logManager)
        {
            this.transactionService = transactionService;
            this.logManager = logManager;
        }

        // GET api alive
        /// <summary>
        /// API Health check
        /// </summary>
        /// <remarks>This API will get the values.</remarks>
        [HttpGet, Route("ping")]
        public ActionResult<string> IsAlive()
        {
            string message = "Service is OK";
            return message;
        }

        [HttpGet, Route("transactions")]
        public async Task<ActionResult> GetAllTransactions()
        {
            try
            {
                TransactionResponsModel responseModel = await transactionService.GetAllTransactionAsync();
                if (responseModel.Results.Count == 0)
                    return NotFound();

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.GetBaseException());
            }
        }

        [HttpPost, Route("uploadtransactions")]
        public async Task<ActionResult<TransactionResponsModel>> UploadTransaction([FromBody] TransactionRequestModel model)
        {
            TransactionResponsModel responseModel = new TransactionResponsModel();
            try
            {
                responseModel = await transactionService.UploadTransaction(model);
                if (responseModel.Status == "OK")
                    return Ok(responseModel);

                if (responseModel.Status == "Failed")
                    return BadRequest(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Status = "Error";
                responseModel.Message = ex.GetBaseException().Message;
                return StatusCode(500, ex.GetBaseException());
            }

            return responseModel;
        }
    }
}
