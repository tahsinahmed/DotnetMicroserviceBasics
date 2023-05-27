using AutoMapper;
using MicroserviceDemo.domain;
using MicroserviceDemo.Dto;

namespace MicroserviceDemo.mapper;

public class ItemMapper : Profile
{
    public ItemMapper()
    {
        CreateMap<CreateItemDto, ItemEntity>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid())
            )
            .ForMember(
                dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTimeOffset.Now)
            );
        CreateMap<ItemEntity, ItemDto>();
        CreateMap<UpdateItemDto, ItemEntity>();
    }
}