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

### How to call from C-Sharp:
![Screenshot 2023-04-23 184516](https://user-images.githubusercontent.com/68454661/233870359-a9918dc5-9cf1-4492-ba71-99909260656d.png)

