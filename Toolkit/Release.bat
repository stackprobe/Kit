C:\Factory\Tools\RDMD.exe /RC out

COPY Toolkit\Toolkit\bin\Release\Toolkit.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\Toolkit
C:\Factory\SubTools\zip.exe /O out Toolkit
COPY out\Toolkit.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\Toolkit.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
