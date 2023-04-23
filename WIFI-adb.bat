@REM Todo: USING CLI-WRAP: https://github.com/Tyrrrz/CliWrap
@REM adb kill-server
@REM adb devices -l
@REM scrcpy --select-usb
@REM scrcpy --list-displays
@REM scrcpy --stay-awake (usb-only)
@REM scrcpy --turn-screen-off

adb tcip 5555
adb connect 192.168.0.229:55555
call START /B "ADB ME" "C:\Program Files\scrcpy-win64-v2.0\scrcpy.exe"

@REM #########
@REM scrcpy --tcpip=192.168.1.1:5555