using AutoMapper;
using Comabit.BL.Company.Dto;
using Comabit.BL.File.Dto;
using Comabit.BL.Geo.Dto;
using Comabit.BL.Identity.Dto;
using Comabit.BL.Inquiry.Dto;
using Comabit.BL.Match.Dto;
using Comabit.BL.Message.DTO;
using Comabit.BL.Porfolio.Dto;
using Comabit.UI.Areas.Admin.Models;
using Comabit.UI.Areas.Authentication.Models;
using Comabit.UI.Areas.Authentication.Models.Geo;
using Comabit.UI.Areas.Buyer.Models.Inquiry;
using Comabit.UI.Areas.Buyer.Models.Project;
using Comabit.UI.Areas.Seller.Models.Match;
using Comabit.UI.Areas.Seller.Models.Settings;
using Comabit.UI.Models.Company;
using Comabit.UI.Models.File;
using Comabit.UI.Models.Match;
using Comabit.UI.Models.Message;
using Comabit.UI.Models.Portfolio;
using System;

namespace Comabit.UI.AutoMapper
{
    public class ViewModelMapperProfile : Profile
    {
        public ViewModelMapperProfile()
        {
            CreateMap<CompanyItem, CompanyViewModel>().ReverseMap();
            CreateMap<SellerItem, PortfolioEditViewModel>().ReverseMap();
            CreateMap<SellerItem, SellerViewModel>().ReverseMap();

            CreateMap<InquiryItem, Areas.Buyer.Models.Inquiry.EditViewModel>().ReverseMap();

            CreateMap<PortfolioAreaItem, AreaViewModel>().ReverseMap();
            CreateMap<PortfolioCategoryItem, CategoryViewModel>().ReverseMap();
            CreateMap<PortfolioSubCategoryItem, SubCategoryViewModel>().ReverseMap();

            CreateMap<StateItem, StateViewModel>().ReverseMap();
            CreateMap<CommunityItem, CommunityViewModel>().ReverseMap();

            CreateMap<RegisterSellerItem, RegisterSellerViewModel>().ReverseMap();
            CreateMap<RegisterBaseItem, RegisterBaseViewModel>().ReverseMap();

            CreateMap<BuyerItem, BuyerViewModel>().ReverseMap();
            CreateMap<MatchItem, MatchViewModel>().ReverseMap();
            CreateMap<InquiryItem, InquiryViewModel>().ReverseMap();
            CreateMap<InquirySellerExclusionItem, InquirySellerExclusionViewModel>().ReverseMap();

            CreateMap<ProjectItem, ProjectViewModel>().ReverseMap();
            CreateMap<ProjectItem, CreateEditViewModel>().ReverseMap();
            CreateMap<CreateOfferItem, CreateOfferViewModel>().ReverseMap();
            CreateMap<OfferItem, OfferViewModel>().ReverseMap();
            CreateMap<MatchItem, Comabit.UI.Areas.Seller.Models.Match.DeleteViewModel>().ReverseMap();
            CreateMap<BL.File.Dto.FileItem, FileViewModel>().ReverseMap();
            CreateMap<BL.Inquiry.Dto.FileItem, FileViewModel>().ReverseMap();
            CreateMap<FileDataItem, FileDataViewModel>().ReverseMap();
            CreateMap<LogItem, LogViewModel>().ReverseMap();

            CreateMap<MatchChatItem, MatchChatViewModel>().ReverseMap();
            CreateMap<ChatMessageViewModel, BL.Message.DTO.ChatMessageItem>().ReverseMap();
        }
    }

    public class ViewModelMapper
    {
        public static IMapper Mapper
        {
            get { return mapper.Value; }
        }

        public static IConfigurationProvider Configuration
        {
            get { return config.Value; }
        }

        public static Lazy<IMapper> mapper = new Lazy<IMapper>(() =>
        {
            var mapper = new Mapper(Configuration);

            return mapper;
        });

        public static Lazy<IConfigurationProvider> config = new Lazy<IConfigurationProvider>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMapperProfile>();
            });

            return config;
        });
    }
}