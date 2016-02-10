C:\Factory\Tools\RDMD.exe /RC out

COPY SetFileTime\SetFileTime\bin\Release\SetFileTime.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\SetFileTime
C:\Factory\SubTools\zip.exe /O out SetFileTime
COPY out\SetFileTime.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\SetFileTime.zip S:\_hidden

PAUSE
