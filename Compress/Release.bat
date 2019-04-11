C:\Factory\Tools\RDMD.exe /RC out

COPY Compress\Compress\bin\Release\Compress.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\Compress
C:\Factory\SubTools\zip.exe /O out Compress
COPY out\Compress.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\Compress.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
