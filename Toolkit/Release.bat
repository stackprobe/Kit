C:\Factory\Tools\RDMD.exe /RC out

COPY Toolkit\Toolkit\bin\Release\Toolkit.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\Toolkit
C:\Factory\SubTools\zip.exe /O out Toolkit
COPY out\Toolkit.zip S:\_kit
CALL C:\home\bat\env\env.bat
C:\Factory\SubTools\nrun.exe /s %mimiko% syncKit
COPY out\Toolkit.zip S:\_release\Kit

PAUSE
