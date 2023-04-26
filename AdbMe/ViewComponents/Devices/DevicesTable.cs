using AdbMe.CLI;
using AdbMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponents.Devices;

public class DevicesTable : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(DevicesViewModel model)
    {   
        var screanner = await model.Screanner.Init();
        if(model.ConnectTo != null) {
            model.Screanner.Run(model.ConnectTo);
        }
        
        foreach (var item in screanner.NmapCli.PotentialDevices)
        {
            item.TcpAvailable = await screanner.AdbCli.TryAddTcpIp(item.LastIp);
        }

        return View(GetItems(screanner));
    }

    private List<List<Device>> GetItems(Scrcpy screanner)
    {
        return new List<List<Device>> {
            screanner.ConnectedDevices,
            screanner.NmapCli.PotentialDevices
        };
    }
}