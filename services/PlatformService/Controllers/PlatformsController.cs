using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController(IPlatformRepo platformRepo, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("====> getting platforms..");

        var platformItems = platformRepo.GetAllPlatforms();

        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        var platformItem = platformRepo.GetPlatformById(id);
        if (platformItem == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(mapper.Map<PlatformReadDto>(platformItem));
        }

    }


}