using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Features.SavingLists.Response;
using Medium.Core.Entities;

namespace Medium.BL.Features.SavingLists.Mapping
{
    public partial class SavingListProfile
    {
        void SavingListMapping()
        {
            CreateMap<SavingList, GetSavingListByIdRequest>();
            CreateMap<SavingList, CreateSavingListResponse>()
                .ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name)); ;


            CreateMap<SavingList, GetSavingListWithStoriesResponse>();
            CreateMap<GetSavingListWithStoriesResponse, SavingList>();


            CreateMap<SavingList, GetAllSavingListResponse>().
             ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));
            CreateMap<GetAllSavingListResponse, SavingList>().
  ForMember(dest => dest.Publisher, op => op.MapFrom(src => src.PublisherName));


            CreateMap<SavingList, GetSavingListByIdResponse>();

            // ==================== UpdateSavingListResponse ==============================
            CreateMap<SavingList, UpdateSavingListResponse>()
                .ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));
            CreateMap<UpdateSavingListRequest, SavingList>();

            // ==================== DeleteSavingListResponse ==============================
            CreateMap<SavingList, DeleteSavingListResponse>();

            // ==================== AddStoryToSaveListResponse ============================
            CreateMap<SavingList, AddStoryToSaveListResponse>();
            //.ForMember(dest => dest.Stories, opt => opt.MapFrom(src => src.Stories));



            // ==================== RemoveStoryFromSavingListResponse ============================
            CreateMap<SavingList, RemoveStoryFromSavingListResponse>();
            CreateMap<RemoveStoryFromSavingListResponse, SavingList>();
            CreateMap<RemoveStoryFromSavingListRequest, SavingList>();


        }
    }
}
