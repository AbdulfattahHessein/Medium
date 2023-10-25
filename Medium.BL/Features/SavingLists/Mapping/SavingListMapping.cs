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

            CreateMap<SavingList, GetAllSavingListResponse>().
             ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));
            CreateMap<GetAllSavingListResponse, SavingList>().
  ForMember(dest => dest.Publisher, op => op.MapFrom(src => src.PublisherName));


            CreateMap<SavingList, GetSavingListByIdResponse>();


            CreateMap<SavingList, UpdateSavingListResponse>()
                .ForMember(dest => dest.PublisherName, op => op.MapFrom(src => src.Publisher.Name));
            CreateMap<UpdateSavingListRequest, SavingList>();

            CreateMap<SavingList, DeleteSavingListResponse>();

            CreateMap<SavingList, AddStoryToSaveListResponse>()
                  .ForMember(dest => dest.SaveListName, op => op.MapFrom(src => src.Name));
            CreateMap<AddStoryToSaveListResponse, SavingList>()
                  .ForMember(dest => dest.Name, op => op.MapFrom(src => src.SaveListName));
            CreateMap<SavingList, AddStoryToSaveListRequest>().ReverseMap();




            CreateMap<SavingList, RemoveStoryFromSavingListResponse>();



            // CreateMap<RemoveStoryFromSavingListResponse, SavingList>();
            //   CreateMap<RemoveStoryFromSavingListRequest, SavingList>();


        }
    }
}
