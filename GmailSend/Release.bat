C:\Factory\Tools\RDMD.exe /RC out

COPY GmailSend\GmailSend\bin\Release\GmailSend.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\GmailSend
C:\Factory\SubTools\zip.exe /O out GmailSend
COPY out\GmailSend.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\GmailSend.zip S:\_hidden

PAUSE
