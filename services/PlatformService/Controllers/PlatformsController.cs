using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController(
    IPlatformRepo platformRepo,
    IMapper mapper,
    ICommandDataClient commandDataClient) : ControllerBase
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

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
    {
        var platformModel = mapper.Map<Platform>(platform);
        platformRepo.CreatePlatform(platformModel);
        platformRepo.SaveChanges();

        var platformReadDto = mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> could not send platform data: {ex.Message}");
        }



        return CreatedAtRoute(nameof(GetPlatformById), new {id = platformReadDto.Id}, platformReadDto);
    }



}