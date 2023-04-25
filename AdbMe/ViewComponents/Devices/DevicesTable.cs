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
        
        return View(GetItems(screanner));
    }

    private List<Device> GetItems(Scrcpy screanner)
    {
        return screanner.ConnectedDevices;
    }
}