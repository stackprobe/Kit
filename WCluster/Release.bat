C:\Factory\Tools\RDMD.exe /RC out

COPY WCluster\WCluster\bin\Release\WCluster.exe out
C:\Factory\Tools\xcp.exe doc out

C:\Factory\Tools\zcp.exe out C:\app\Kit\WCluster
C:\Factory\SubTools\zip.exe /O out WCluster
COPY out\WCluster.zip S:\_kit
C:\Factory\SubTools\nrun.exe /s nazrin syncKit
COPY out\WCluster.zip S:\_hidden

PAUSE
