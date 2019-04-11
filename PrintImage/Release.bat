C:\Factory\Tools\RDMD.exe /RC out

COPY PrintImage\PrintImage\bin\Release\PrintImage.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\PrintImage
C:\Factory\SubTools\zip.exe /O out PrintImage
COPY out\PrintImage.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\PrintImage.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
