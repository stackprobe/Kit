C:\Factory\Tools\RDMD.exe /RC out

COPY Dir2\Dir2\bin\Release\Dir2.exe out
COPY Dir2Tools\Dir2Tools\bin\Release\Dir2Tools.exe out
COPY AddFilePart\AddFilePart\bin\Release\AddFilePart.exe out
COPY GetFilePart\GetFilePart\bin\Release\GetFilePart.exe out
COPY SetFileTime\SetFileTime\bin\Release\SetFileTime.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\Dir2
C:\Factory\SubTools\zip.exe /O out Dir2
COPY out\Dir2.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s 192.168.222.102 syncKit
COPY out\Dir2.zip S:\_hidden

PAUSE