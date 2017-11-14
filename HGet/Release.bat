C:\Factory\Tools\RDMD.exe /RC out

COPY HGet\HGet\bin\Release\HGet.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\HGet
C:\Factory\SubTools\zip.exe /O out HGet
COPY out\HGet.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\HGet.zip S:\_release\Kit

PAUSE
