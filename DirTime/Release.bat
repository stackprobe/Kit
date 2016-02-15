C:\Factory\Tools\RDMD.exe /RC out

COPY DirTime\DirTime\bin\Release\DirTime.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\DirTime
C:\Factory\SubTools\zip.exe /O out DirTime
COPY out\DirTime.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\DirTime.zip S:\_hidden

PAUSE
