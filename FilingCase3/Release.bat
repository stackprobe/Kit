C:\Factory\Tools\RDMD.exe /RC out

COPY WFilingCase3\WFilingCase3\bin\Release\WFilingCase3.exe out
COPY C:\Factory\Program\FilingCase3\Server.exe out
COPY C:\Factory\Resource\CP932.txt out
COPY C:\Factory\Resource\JIS0208.txt out
COPY Tools\CTools.exe out
C:\Factory\Tools\xcp.exe doc out
COPY res\app_16_01.ico out\app_16_01.dat
COPY res\app_16_11.ico out\app_16_11.dat

C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\Server.exe
C:\Factory\SubTools\EmbedConfig.exe --factory-dir-disabled out\CTools.exe

C:\Factory\Tools\zcp.exe out C:\app\Kit\FilingCase3
C:\Factory\SubTools\zip.exe /O out FilingCase3
COPY out\FilingCase3.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s mimiko syncKit
COPY out\FilingCase3.zip S:\_hidden\release\Kit

PAUSE
