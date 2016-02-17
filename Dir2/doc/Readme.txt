----
コマンド

	Dir2.exe DIR

		DIR ... ディレクトリ

	AddFilePart.exe READ-FILE WRITE-FILE START-POS

		READ-FILE  ... 読み込みファイル
		WRITE-FILE ... 出力ファイル
		START-POS  ... 出力開始位置

	GetFilePart.exe READ-FILE WRITE-FILE START-POS READ-SIZE

		READ-FILE  ... 読み込みファイル
		WRITE-FILE ... 出力ファイル
		START-POS  ... 読み込み開始位置
		READ-SIZE  ... 読み込みサイズ

	SetFileTime.exe WRITE-FILE WRITE-TIME

		WRITE-FILE ... 出力ファイル
		WRITE-TIME ... 設定する最終更新日時, YYYYMMDDhhmmssLLL 形式 ex. 20160217041730123
