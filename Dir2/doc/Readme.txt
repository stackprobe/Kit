----
コマンド

	Dir2.exe DIR WRITE-FILE SUCCESSFUL-FILE

		DIR ... ディレクトリ
		WRITE-FILE ... 出力ファイル
		SUCCESSFUL-FILE ... 成功時作成するファイル

	Dir2Tools.exe (/MD DIR | /RD DIR | /DEL FILE) SUCCESSFUL-FILE

		DIR  ... ディレクトリ
		FILE ... ファイル
		SUCCESSFUL-FILE ... 成功時作成するファイル

	AddFilePart.exe READ-FILE WRITE-FILE START-POS SUCCESSFUL-FILE

		READ-FILE  ... 読み込みファイル
		WRITE-FILE ... 出力ファイル
		START-POS  ... 出力開始位置
		SUCCESSFUL-FILE ... 成功時作成するファイル

	GetFilePart.exe READ-FILE WRITE-FILE START-POS READ-SIZE SUCCESSFUL-FILE

		READ-FILE  ... 読み込みファイル
		WRITE-FILE ... 出力ファイル
		START-POS  ... 読み込み開始位置
		READ-SIZE  ... 読み込みサイズ
		SUCCESSFUL-FILE ... 成功時作成するファイル

	SetFileTime.exe WRITE-FILE WRITE-TIME SUCCESSFUL-FILE

		WRITE-FILE ... 出力ファイル
		WRITE-TIME ... 設定する最終更新日時, YYYYMMDDhhmmssLLL 形式 ex. 20160217041730123
		SUCCESSFUL-FILE ... 成功時作成するファイル

----
引数をファイルで指定する。

	実行ファイル //R 引数リストファイル

	例

	AddFilePart.exe abc.txt def.txt 123000 s.flg

	->

	123.txt に以下を記述する。(Shift_JIS)

		abc.txt
		def.txt
		123000
		s.flg

	AddFilePart.exe //R 123.txt
