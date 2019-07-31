C:\Factory\Tools\RDMD.exe /RC out

COPY WDialog\WDialog\bin\Release\WDialog.exe out
COPY WDialog\WDialog\bin\Release\Chocolate.dll out
COPY WDialog\WDialog\bin\Release\Chocomint.dll out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\WDialog
C:\Factory\SubTools\zip.exe /O out WDialog
COPY out\WDialog.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\WDialog.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
