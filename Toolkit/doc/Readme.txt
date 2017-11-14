----
コマンド

	Toolkit.exe /MASK-RESOURCE-IMAGE 入力画像ファイル 出力pngファイル [ハッシュ入力ファイル]

	Toolkit.exe /IMG-TO-IMG 入力画像ファイル 出力画像ファイル

	Toolkit.exe /EVENT-LOG 開始日時 終了日時 メッセージ出力の有無 出力csvファイル

		開始日時              --  YYYYMMDDhhmmss
		終了日時              --  YYYYMMDDhhmmss
		メッセージ出力の有無  --  0:無し、1:有り

		ex.
			Toolkit /event-log 20170101000000 20181231235959 1 output.csv
