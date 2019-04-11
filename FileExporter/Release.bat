C:\Factory\Tools\RDMD.exe /RC out

COPY FileExporter\FileExporter\bin\Release\FileExporter.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\FileExporter
C:\Factory\SubTools\zip.exe /O out FileExporter
COPY out\FileExporter.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\FileExporter.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
