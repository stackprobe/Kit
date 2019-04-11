C:\Factory\Tools\RDMD.exe /RC out

COPY RSA\RSA\bin\Release\RSA.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\RSA
C:\Factory\SubTools\zip.exe /O out RSA
COPY out\RSA.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\RSA.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
