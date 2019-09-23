C:\Factory\Tools\RDMD.exe /RC out

COPY Toolkit2\Toolkit2\bin\Release\Chocolate.dll out
COPY Toolkit2\Toolkit2\bin\Release\Toolkit2.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\Toolkit2
C:\Factory\SubTools\zip.exe /O out Toolkit2
COPY out\Toolkit2.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\Toolkit2.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
