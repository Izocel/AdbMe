using AdbMe.CLI;
using AdbMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponents.Devices;

public class DevicesTable : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(Scrcpy screanner)
    {   
        await screanner.Init();
        return View(GetItems(screanner));
    }

    private List<Device> GetItems(Scrcpy screanner)
    {
        return screanner.ConnectedDevices;
    }
}