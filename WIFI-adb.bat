REM adb kill-server
adb tcip 5555
adb connect 192.168.0.229
call START /B "ADB ME" "C:\Program Files\scrcpy-win64-v2.0\scrcpy.exe"