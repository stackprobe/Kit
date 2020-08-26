----
コマンド

	Toolkit.exe /MASK-RESOURCE-IMAGE-NBC 入力画像ファイル 出力pngファイル

		ハッシュのバーコード無し版

	Toolkit.exe /MASK-RESOURCE-IMAGE 入力画像ファイル 出力pngファイル [ハッシュ入力ファイル]

	Toolkit.exe /IMG-TO-IMG 入力画像ファイル 出力画像ファイル

	Toolkit.exe /EVENT-LOG 開始日時 終了日時 メッセージ出力の有無 出力csvファイル

		開始日時              --  YYYYMMDDhhmmss
		終了日時              --  YYYYMMDDhhmmss
		メッセージ出力の有無  --  0:無し、1:有り

		ex.
			Toolkit /event-log 20170101000000 20181231235959 1 output.csv

	Toolkit.exe /MULTI-RUN モード [実行ファイル...]

		モード -- (0, 1, 2) == (Hide, Min, Normal)

	Toolkit.exe /MD5 入力パス 出力ファイル

	Toolkit.exe /SHA-512 入力パス 出力ファイル

	Toolkit.exe /SHA-512-128 入力パス 出力ファイル

	Toolkit.exe /PRINT-SCREEN 出力ファイルbase

		ex.
			Toolkit /print-screen C:\temp\Screen_

			モニタ３つのとき...

				C:\temp\Screen_01.bmp
				C:\temp\Screen_02.bmp
				C:\temp\Screen_03.bmp

	Toolkit.exe /PLAY-WAV wavファイル
