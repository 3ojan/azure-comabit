namespace Comabit.BL
{
    using AutoMapper;
    using Comabit.BL.Company.Dto;
    using Comabit.BL.File.Dto;
    using Comabit.BL.Geo.Dto;
    using Comabit.BL.Inquiry.Dto;
    using Comabit.BL.Match.Dto;
    using Comabit.BL.Porfolio.Dto;
    using Comabit.DL.Data.Company;
    using Comabit.DL.Data.ElasticSearch;
    using Comabit.DL.Data.File;
    using Comabit.DL.Data.Geo;
    using Comabit.DL.Data.Inquiry;
    using Comabit.DL.Data.Portfolio;
    using System;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DL.Data.Company.Company, CompanyItem>().ReverseMap();
            CreateMap<Seller, SellerItem>().ReverseMap();
            CreateMap<Buyer, BuyerItem>().ReverseMap();
            CreateMap<PortfolioArea, PortfolioAreaItem>().ReverseMap();
            CreateMap<PortfolioCategory, PortfolioCategoryItem>().ReverseMap();
            CreateMap<PortfolioSubCategory, PortfolioSubCategoryItem>().ReverseMap();
            CreateMap<AdditionalPortfolioCategoryTags, AdditionalPortfolioCategoryTagsItem>().ReverseMap();
            CreateMap<State, StateItem>().ReverseMap();
            CreateMap<Project, ProjectItem>().ReverseMap();
            CreateMap<Project, ProjectEditItem>().ReverseMap();
            CreateMap<ProjectItem, ProjectEditItem>().ReverseMap();
            CreateMap<DL.Data.Inquiry.Inquiry, InquiryItem>().ReverseMap();
            CreateMap<InquirySellerExclusion, InquirySellerExclusionItem>().ReverseMap();
            CreateMap<Province, ProvinceItem>().ReverseMap();
            CreateMap<Community, CommunityItem>().ReverseMap();
            CreateMap<City, CityItem>().ReverseMap();
            CreateMap<StateItem, State>().ReverseMap();
            CreateMap<ProvinceItem, Province>().ReverseMap();
            CreateMap<CommunityItem, Community>().ReverseMap();
            CreateMap<CityItem, City>().ReverseMap();
            CreateMap<MatchItem, DL.Data.Match.Match>().ReverseMap();
            CreateMap<MessageItem, DL.Data.Match.UserMessage>().ReverseMap();
            CreateMap<MessageItem, DL.Data.Match.SystemMessage>().ReverseMap();
            CreateMap<MessageItem, DL.Data.Match.Message>().ReverseMap();
            CreateMap<File.Dto.FileItem, DL.Data.File.File>().ReverseMap();
            CreateMap<File.Dto.FileItem, OfferFile>().ReverseMap();
            CreateMap<FileDataItem, FileData>().ReverseMap();
            CreateMap<OfferItem, DL.Data.Match.Offer>().ReverseMap();
            CreateMap<Inquiry.Dto.FileItem, InquiryFile>().ReverseMap();
            CreateMap<LogItem, DL.Data.Log.Log>().ReverseMap();
            
            CreateMap<SellerDoc, SellerItem>().ReverseMap()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.PortfolioCategories))
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.PortfolioSubCategories));

            CreateMap<CategoryDoc, PortfolioCategoryItem>().ReverseMap()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.AdditionalPortfolioCategoryTagsAsString));

            CreateMap<CityDoc, CityItem>().ReverseMap();
        }
    }

    public class ObjectMapper
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
                cfg.AddProfile<Comabit.BL.MapperProfile>();
            });

            return config;
        });
    }
}