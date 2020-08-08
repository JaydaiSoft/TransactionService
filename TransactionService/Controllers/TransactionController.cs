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
            logManager.Instance.Info("Log Ping HealthCheck");
            string message = "Service is OK";
            return message;
        }

        [HttpGet, Route("All")]
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
                {
                    logManager.Instance.Info(responseModel.Message);
                    return Ok(responseModel);
                }

                if (responseModel.Status == "Failed")
                {
                    logManager.Instance.Info("Upload Transaction Failed!!");
                    return BadRequest(responseModel);
                }
            }
            catch (Exception ex)
            {
                responseModel.Status = "Error";
                responseModel.Message = ex.GetBaseException().Message;
                logManager.Instance.Error(ex.GetBaseException().Message);
                return StatusCode(500, ex.GetBaseException());
            }

            return responseModel;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTransactions([FromQuery] string CurrencyCode, [FromQuery] string TransactionStatus, 
            [FromQuery] DateTime transDateFrom, [FromQuery] DateTime transDateTO)
        {
            TransactionFilter transactionFilter = new TransactionFilter();
            transactionFilter.CurrencyCode = CurrencyCode;
            transactionFilter.TransactionStatus = TransactionStatus;
            transactionFilter.TransDateFrom = transDateFrom;
            transactionFilter.TransDateTO = transDateTO;

            try
            {
                TransactionResponsModel responseModel = await transactionService.GetAllTransactionAsync(transactionFilter);
                if (responseModel.TransactionItems.Count == 0)
                {
                    logManager.Instance.Info("Transaction NotFound!!");
                    return NotFound();
                }

                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                logManager.Instance.Error(ex.GetBaseException().Message);
                return StatusCode(500, ex.GetBaseException());
            }
        }

        [HttpGet, Route("Currency")]
        public async Task<ActionResult> GetCurrency()
        {
            try
            {
                var result = await transactionService.GetCurrency();
                if (result.Length > 1)
                {
                    logManager.Instance.Info("GetCurrency Successfull");
                    return Ok(result);
                }

                logManager.Instance.Info("Currency NotFound!!");
                return NotFound(new string[] {""});
            }
            catch (Exception ex)
            {
                logManager.Instance.Error(ex.GetBaseException().Message);
                return StatusCode(500, ex.GetBaseException());
            }
        }
    }
}
