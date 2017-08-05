C:\Factory\Tools\RDMD.exe /RC out

COPY ImgTools\ImgTools\bin\Release\ImgTools.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\ImgTools
C:\Factory\SubTools\zip.exe /O out ImgTools
COPY out\ImgTools.zip S:\_kit
CALL C:\home\bat\env\env.bat
CALL C:\home\bat\env\env.bat
C:\Factory\SubTools\nrun.exe /s %mimiko% syncKit
COPY out\ImgTools.zip S:\_release\Kit

PAUSE
