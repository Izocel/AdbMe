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
scrcpy --select-usb
or
scrcpy --serial=R5CW10PPKLL
@REM for TCPIP
scrcpy --tcpip
or
scrcpy --serial=192.168.0.1:5555
```

Bower:
bower.io

## List device manager type e.g: Usb | Bluetooth
- win10: Get-PnPDevice | Where-Object -FilterScript { $_.Class -eq 'Bluetooth' -and $_.FriendlyName -like '*' }


## Generate app desktop application
https://docs.servicestack.net/netcore-windows-desktop

### getIp of a device from local computer:
windows
`for /f "tokens=9" %a in ('adb shell ip route') do echo %a`

## You can call the APi at : api/any... ()
## You can check the APi definitions at : url/ui

As part of our [Physical Project Structure](https://docs.servicestack.net/physical-project-structure) convention we recommend maintaining any shared non Request/Response DTOs in the `ServiceModel.Types` namespace.

### How to call from C-Sharp:
![Screenshot 2023-04-23 184516](https://user-images.githubusercontent.com/68454661/233870359-a9918dc5-9cf1-4492-ba71-99909260656d.png)

## Diagrams
- [Concept](https://github.com/Izocel/AdbMe/blob/c974509a00cbdf7501b2758fa85f8eded65d827c/Diagrams/AdbMe.drawio.pdf)
