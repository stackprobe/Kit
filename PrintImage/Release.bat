C:\Factory\Tools\RDMD.exe /RC out

COPY PrintImage\PrintImage\bin\Release\PrintImage.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\PrintImage
C:\Factory\SubTools\zip.exe /O out PrintImage
COPY out\PrintImage.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s mimiko syncKit
COPY out\PrintImage.zip S:\_release\Kit

PAUSE
