C:\Factory\Tools\RDMD.exe /RC out

COPY WDrop\WDrop\bin\Release\WDrop.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\WDrop
C:\Factory\SubTools\zip.exe /O out WDrop
COPY out\WDrop.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\WDrop.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
