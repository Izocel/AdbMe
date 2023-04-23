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
