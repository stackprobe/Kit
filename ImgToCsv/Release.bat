C:\Factory\Tools\RDMD.exe /RC out

COPY ImgToCsv\ImgToCsv\bin\Release\ImgToCsv.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\kit\ImgToCsv
C:\Factory\SubTools\zip.exe /O out ImgToCsv
COPY out\ImgToCsv.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\ImgToCsv.zip S:\_hidden

PAUSE
