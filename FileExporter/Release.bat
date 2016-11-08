C:\Factory\Tools\RDMD.exe /RC out

COPY FileExporter\FileExporter\bin\Release\FileExporter.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\FileExporter
C:\Factory\SubTools\zip.exe /O out FileExporter
COPY out\FileExporter.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s mimiko syncKit
COPY out\FileExporter.zip S:\_hidden\release\Kit

PAUSE
