----
コマンド

	SpicaToolkit.exe /MUTEX mutex名 待ち時間millis enterイベント名(set) timeoutイベント名(set) leaveイベント名(wait) 呼び出し側プロセスid

	SpicaToolkit.exe /NAMED-EVENT 作成イベント名(open) enterイベント名(set) timeoutイベント名(wait) 呼び出し側プロセスid

	SpicaToolkit.exe /NAMED-EVENT-WAIT 待ちイベント名(wait) 待ち時間millis 呼び出し側プロセスid

	SpicaToolkit.exe /NAMED-EVENT-SET setイベント名(set)


----
ファイルから

	SpicaToolkit.exe //R Args.txt

		Args.txt ... Shift_JIS, 改行区切り

