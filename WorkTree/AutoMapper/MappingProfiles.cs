﻿using AutoMapper;
using WorkTree.Database.DTO.Request;
using WorkTree.Database.DTO.Response;
using WorkTree.Database.Models;
using WorkTree.Database.Models.Tree;

namespace WorkTree.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Base Item
            CreateMap<BaseItem, BaseItemResponseDTO>().ReverseMap();
            CreateMap<BaseItem, BaseItemRequestDTO>().ReverseMap();

            //Base Item Relation
            CreateMap<BaseItemRelation, BaseItemRelationResponseDTO>().ReverseMap();
            CreateMap<BaseItemRelation, BaseItemRelationRequestDTO>().ReverseMap();

            //Tree
            CreateMap<TreeBaseItemRelation, TreeBaseItemRelationResponseDTO>().ReverseMap();
            CreateMap<TreeBuilderOptions, TreeBuilderOptionsRequestDTO>().ReverseMap();

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