using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Logging;
using TransactionServices.Model.Entity;
using TransactionServices.Model.ViewModel;
using TransactionServices.Repository;

namespace TransactionServices.Service
{
    public class TransactionService : ITransactionService
    {
        private ITransactionContext _context;
        private ITransactionRepository _repository;
        private readonly IMapper _mapper;
        private ILogManager logManager;
        public TransactionService(ITransactionContext context, ITransactionRepository repository, IMapper mapper, ILogManager logManager)
        {
            this._context = context;
            this._repository = repository;
            this._mapper = mapper;
            this.logManager = logManager;
        }

        public async Task<TransactionResponsModel> GetAllTransactionAsync()
        {
            TransactionResponsModel response = new TransactionResponsModel();
            try
            {
                var result = await _repository.GetAllTransactions();
                response.Results = _mapper.Map<List<Transactions>, List<TransactionModel>>(result);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.GetBaseException().Message;
            }
            return response;
        }

        public async Task<TransactionResponsModel> UploadTransaction(TransactionRequestModel requestModel)
        {
            TransactionResponsModel response = new TransactionResponsModel();
            try
            {
                var enitityModel = _mapper.Map<List<TransactionModel>, List<Transactions>>(requestModel.filter);
                var result = await _repository.UploadTransaction(enitityModel);
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Message = ex.GetBaseException().Message;
            }
            return response;
        }
    }
}
