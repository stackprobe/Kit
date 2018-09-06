----
コマンド

	HGet.exe [/P (DIRECT | IE | SPECIAL proxy-host proxy-port)] [/-TLS12] [/To timeout-millis]
	         [/M (HEAD | GET | POST)] [/U url] [/V (10 | 11)]
	         [/H header-field-name header-field-value] ...
	         [/B body-string | /BF body-file]
	         [/F body-2-file]
	         [/T body-3-string | /TF body-3-file]
	         [/RSF successfule-file] [/RHF res-header-fields-file] [/RBF res-body-file] [/RBFX res-body-file-size-max]
			 [/K | /K-]
	         [/-]
	         [url | url body-file]

		/-TLS12 ... TLS 1.2 を無効にする。

		/K  ... 終了時キー待ち。
		/K- ... エラー終了時キー待ち。

----
コマンド引数をファイルで指定する。

	HGet.exe //R args-file

		args-file ... コマンド引数を行毎に記述する。文字コードは Shift_JIS
