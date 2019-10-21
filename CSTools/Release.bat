C:\Factory\Tools\RDMD.exe /RC out

COPY C:\Dev\CSharp\Chocolate\Chocolate\bin\Release\Chocolate.dll out

sub\ReleaseCSExe.exe . out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\CSTools
C:\Factory\SubTools\zip.exe /O out CSTools
COPY out\CSTools.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\CSTools.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
