using Medium.BL.Features.SavingLists.Request;
using Medium.BL.Features.SavingLists.Response;
using Medium.Core.Entities;

namespace Medium.BL.Features.SavingLists.Mapping
{
    public partial class SavingListProfile
    {
        void SavingListMapping()
        {
            // ==================== GetAll SavingList Response ==============================
            CreateMap<SavingList, GetAllSavingListResponse>().
             ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));

            // ==================== Get SavingList ById Response ==============================
            CreateMap<SavingList, GetSavingListByIdResponse>();

            // ==================== Create SavingList Response ==============================
            CreateMap<SavingList, CreateSavingListResponse>();


            // ==================== UpdateSavingListResponse ==============================
            CreateMap<SavingList, UpdateSavingListResponse>()
                .ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));
            CreateMap<UpdateSavingListRequest, SavingList>();

            // ==================== DeleteSavingListResponse ==============================
            CreateMap<SavingList, DeleteSavingListResponse>();

            // ==================== GetSavingList With Stories Response ==============================
            CreateMap<SavingList, GetSavingListWithStoriesResponse>()
                .ForMember(dest => dest.Stories, op => op.MapFrom(src => src.Stories));
            CreateMap<GetSavingListWithStoriesResponse, SavingList>();

            // ==================== AddStoryToSaveListResponse ============================
            CreateMap<SavingList, AddStoryToSaveListResponse>();

            // ==================== RemoveStoryFromSavingListResponse ============================
            CreateMap<SavingList, RemoveStoryFromSavingListResponse>();

            // ==================== GetAllPaginationSaveListRequest ============================
            CreateMap<SavingList, GetAllPaginationSaveListResponse>();



        }
    }
}
