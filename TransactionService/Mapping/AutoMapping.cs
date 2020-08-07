using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionServices.Model.Entity;
using TransactionServices.Model.ViewModel;

namespace TransactionServices.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Transactions, TransactionModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opts => opts.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CurrencyCode, opts => opts.MapFrom(src => src.CurrencyCode))
                .ForMember(dest => dest.TransactionDate, opts => opts.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status)).ReverseMap();

            CreateMap<TransactionPayload, Transactions>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opts => opts.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CurrencyCode, opts => opts.MapFrom(src => src.CurrencyCode))
                .ForMember(dest => dest.TransactionDate, opts => opts.MapFrom(src => src.TransactionDate))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status)).ReverseMap();

            CreateMap<Transactions, TransactionItem>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.Payment, opts => opts.MapFrom(src => src.Amount.ToString()+src.CurrencyCode))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status)).ReverseMap();
        }
    }
}
