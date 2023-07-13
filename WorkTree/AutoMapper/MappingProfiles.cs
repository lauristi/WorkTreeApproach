using AutoMapper;
using WorkTree.Database.DTO.Request;
using WorkTree.Database.DTO.Response;
using WorkTree.Database.Models;

namespace WorkTree.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Base Item
            CreateMap<BaseItem, BaseItemResponseDTO>().ReverseMap();
            CreateMap<BaseItem, BaseItemRequestDTO>().ReverseMap();

            CreateMap<BaseItemChild, BaseItemChildResponseDTO>().ReverseMap();
            CreateMap<BaseItemChild, BaseItemChildRequestDTO>().ReverseMap();

            //Job Item
            CreateMap<JobItem, JobItemResponseDTO>().ReverseMap();
            CreateMap<JobItem, JobItemRequestDTO>().ReverseMap();

            CreateMap<JobItemChild, JobItemChildResponseDTO>().ReverseMap();
            CreateMap<JobItemChild, JobItemChildRequestDTO>().ReverseMap();

            //Types
            CreateMap<ItemStatus, ItemStatusResponseDTO>().ReverseMap();
            CreateMap<ItemStatus, ItemStatusRequestDTO>().ReverseMap();

            CreateMap<ItemType, ItemTypeResponseDTO>().ReverseMap();
            CreateMap<ItemType, ItemTypeRequestDTO>().ReverseMap();

            CreateMap<OwnerType, OwnerTypeResponseDTO>().ReverseMap();
            CreateMap<OwnerType, OwnerTypeRequestDTO>().ReverseMap();
        }
    }
}