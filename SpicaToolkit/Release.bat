C:\Factory\Tools\RDMD.exe /RC out

COPY SpicaToolkit\SpicaToolkit\bin\Release\SpicaToolkit.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\SpicaToolkit
C:\Factory\SubTools\zip.exe /O out SpicaToolkit
COPY out\SpicaToolkit.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\SpicaToolkit.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
