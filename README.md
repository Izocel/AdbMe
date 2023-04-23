# mvc

.NET 6.0 MVC Website with ServiceStack APIs

[![](https://raw.githubusercontent.com/ServiceStack/Assets/master/csharp-templates/mvc.png)](http://mvc.web-templates.io/)

> Browse [source code](https://github.com/NetCoreTemplates/mvc), view live demo [mvc.web-templates.io](http://mvc.web-templates.io) and install with [dotnet-new](https://docs.servicestack.net/dotnet-new):

    $ dotnet tool install -g x

    $ x new mvc ProjectName

Alternatively write new project files directly into an empty repository, using the Directory Name as the ProjectName:

    $ git clone https://github.com/<User>/<ProjectName>.git
    $ cd <ProjectName>
    $ x new mvc

## Some Scrcpy notes:
```
@REM Todo: USING CLI-WRAP: https://github.com/Tyrrrz/CliWrap
@REM scrcpy --select-usb
@REM scrcpy --list-displays
@REM scrcpy --stay-awake (usb-only)
@REM scrcpy --turn-screen-off
@REM scrcpy --tcpip=192.168.0.1:5555
@REM adb shell ip route | awk '{print $9}' <-- GET LOCAL IP

@REM Start server and add authorized devices/protocol to it.
adb kill-server
adb tcpip 5555
adb devices -l

@REM for USB
scrcpy --serial=R5CW10PPKLL
@REM for TCPIP
scrcpy --serial=192.168.0.1:5555
```


## You can call the APi at : api/any... ()
## You can check the APi definitions at : url/ui

### Good to do for API calls:
