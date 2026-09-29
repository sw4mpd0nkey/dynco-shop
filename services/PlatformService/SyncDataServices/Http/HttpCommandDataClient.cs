using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.DTOs;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient(HttpClient httpClient, IConfiguration config) : ICommandDataClient
{
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json"

        );

        var response = await
            httpClient.PostAsync(config["CommandService"], httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POSt to command srv ok!");
        }
        else
        {
            Console.WriteLine("==> sync post failed!!!");
        }

    }
}
