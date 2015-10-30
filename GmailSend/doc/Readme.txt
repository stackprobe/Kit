----
コマンド

	GmailSend.exe [/F 送信元メールアドレス]
	              [/To 送信先メールアドレス] ...
	              [/CC 送信先メールアドレス] ...
	              [/BCC 送信先メールアドレス] ...
	              [/S 表題]
	              [/B 本文]
	              [/A 添付ファイル] ...
	              [/C ユーザー名 パスワード]
	              [/Host サーバーホスト]
	              [/Port サーバーポート番号]
	              [/-SSL]
	              [/SF SUCCESSFUL-FILE]
	              [/ELF ERROR-LOG-FILE]

	本文

		**XXX -> *XXX
		*XXX -> ファイル XXX の内容, Shift_JIS であること。
		XXX -> XXX

	SUCCESSFUL-FILE

		送信に成功した場合、空のファイルを作成する。

	ERROR-LOG-FILE

		送信に失敗した場合、エラー内容を書き出す。Shift_JIS で書き出す。

----
使用例

	> GmailSend.exe /F from123@gmail.com /To to456@xxx.com /S あああ /B *Body.txt /C from123@gmail.com 1111

----
備忘

	5.5.1 Authentication Required. Learm more at と返される場合、
	アカウント情報 / Google へのログイン / 安全性の低いアプリの許可：有効
	にする。
