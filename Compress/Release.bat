C:\Factory\Tools\RDMD.exe /RC out

COPY Compress\Compress\bin\Release\Compress.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\kit\Compress
C:\Factory\SubTools\zip.exe /O out Compress
COPY out\Compress.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\Compress.zip S:\_hidden

PAUSE