﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.ViewModel;

namespace TransactionServices.Model.Validator
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequestModel>
    {
        public TransactionRequestValidator()
        {
            RuleFor(request => request.TransactionPayloads)
                   .Must(collection => collection == null || collection.All(item => !string.IsNullOrEmpty(item.TransactionId)))
                   .WithMessage("TransactionId is mandatory.");
            RuleFor(request => request.TransactionPayloads)
                   .Must(collection => collection == null || collection.All(item => item.Amount > 0))
                   .WithMessage("Amount is mandatory and must have value more than zero.");
            RuleFor(request => request.TransactionPayloads)
                   .Must(collection => collection == null || collection.All(item => (!string.IsNullOrEmpty(item.CurrencyCode) && item.CurrencyCode.Length == 3)))
                   .WithMessage("CurrencyCode is mandatory.");
            RuleFor(request => request.TransactionPayloads)
                   .Must(collection => collection == null || collection.All(item => BeAValidDate(item.TransactionDate.ToString())))
                   .WithMessage("TransactionDate is mandatory.");
            //RuleFor(request => request.filter)
            //       .Must(collection => collection == null || collection.All(item => (!string.IsNullOrEmpty(item.Status) && item.Status.Length == 1)))
            //       .WithMessage("Status is mandatory.");
            RuleSet("GetAllTransactions", () =>
            {
                RuleFor(request => request.TransactionFilter)
                    .Must(request => !string.IsNullOrEmpty(request.CurrencyCode)).WithMessage("CurrencyCode is mandatory.");
                RuleFor(request => request.TransactionFilter)
                    .Must(request => !string.IsNullOrEmpty(request.TransactionStatus)).WithMessage("TransactionStatus is mandatory.");
                RuleFor(request => request.TransactionFilter.TransDateFrom).NotEmpty().WithMessage("TransDateFrom is Required");
                RuleFor(request => request.TransactionFilter.TransDateTO).NotEmpty().WithMessage("TransDateTO is Required")
                    .GreaterThan(request => request.TransactionFilter.TransDateFrom)
                    .WithMessage("TransDateFrom must after Start date").When(request => request.TransactionFilter.TransDateFrom != DateTime.MinValue);
            });
            //RuleFor(request => request.TransactionFilter)
            //    .Must(request => !string.IsNullOrEmpty(request.CurrencyCode)).WithMessage("CurrencyCode is mandatory.");
            //RuleFor(request => request.TransactionFilter)
            //    .Must(request => !string.IsNullOrEmpty(request.TransactionStatus)).WithMessage("TransactionStatus is mandatory.");
            //RuleFor(request => request.TransactionFilter.TransDateFrom).NotEmpty().WithMessage("TransDateFrom is Required");
            //RuleFor(request => request.TransactionFilter.TransDateTO).NotEmpty().WithMessage("TransDateTO is Required")
            //    .GreaterThan(request => request.TransactionFilter.TransDateFrom)
            //    .WithMessage("TransDateFrom must after Start date").When(request => request.TransactionFilter.TransDateFrom != DateTime.MinValue);
        }

        private bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }

    }
}
