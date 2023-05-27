using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicroserviceDemo.domain;
using MicroserviceDemo.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceDemo.Controllers;

[ApiController]
[Route("item")]
public class ItemController : ControllerBase
{
    public readonly IMapper _mapper;
    public static readonly List<ItemEntity> ItemEntities = new List<ItemEntity>();

    public ItemController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public IEnumerable<ItemDto> GetAll()
    {
        return _mapper.Map<List<ItemEntity>, List<ItemDto>>(ItemEntities);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
        var itemDto = ItemEntities.SingleOrDefault(item => id.Equals(item.Id));
        if (itemDto == null)
        {
            return NotFound();
        }
        return _mapper.Map<ItemEntity, ItemDto>(itemDto);
    }

    [HttpPost]
    public ActionResult<ItemDto> Create(CreateItemDto createItemDto)
    {
        var itemEntity = _mapper.Map<CreateItemDto, ItemEntity>(createItemDto);
        ItemEntities.Add(itemEntity);
        return CreatedAtAction(nameof(GetById), new { itemEntity.Id }, itemEntity);
    }

    [HttpPut]
    public IActionResult Update(UpdateItemDto updateItemDto)
    {
        var updatedItem = _mapper.Map<UpdateItemDto, ItemEntity>(updateItemDto);
        var index = ItemEntities.FindIndex(item => item.Id == updateItemDto.Id);
        ItemEntities[index] = updatedItem;
        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        ItemEntities.RemoveAt(ItemEntities.FindIndex(item => item.Id.Equals(id)));
        return NoContent();
    }
}