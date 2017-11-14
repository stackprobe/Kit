C:\Factory\Tools\RDMD.exe /RC out

COPY BmpToCsv\BmpToCsv\bin\Release\BmpToCsv.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\BmpToCsv
C:\Factory\SubTools\zip.exe /O out BmpToCsv
COPY out\BmpToCsv.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\BmpToCsv.zip S:\_release\Kit

PAUSE
