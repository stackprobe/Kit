C:\Factory\Tools\RDMD.exe /RC out

COPY GmailSend\GmailSend\bin\Release\GmailSend.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe /F out C:\app\Kit\GmailSend
C:\Factory\SubTools\zip.exe /O out GmailSend
COPY out\GmailSend.zip S:\_kit
CALL C:\home\bat\syncKit.bat
COPY out\GmailSend.zip S:\_release\Kit

C:\Factory\Tools\PauseEx.exe
