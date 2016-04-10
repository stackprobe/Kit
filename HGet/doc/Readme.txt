----
コマンド

	HGet.exe [/P (DIRECT | IE | SPECIAL PROXY-HOST PROXY-PORT)] [/T TIMEOUT-MILLIS]
	         [/M (HEAD | GET | POST)] [/U URL] [/V (10 | 11)]
	         [/H HEADER-FIELD-NAME HEADER-FIELD-VALUE] ...
	         [/B BODY-STRING | /BF BODY-FILE]
	         [/RSF SUCCESSFULE-FILE] [/RHF RES-HEADER-FIELDS-FILE] [/RBF RES-BODY-FILE] [/RBFX RES-BODY-FILE-SIZE-MAX]
	         [/-]
	         [URL | URL BODY-FILE]

----
コマンド引数をファイルで指定する。

	HGet.exe //R ARGS-FILE

		ARGS-FILE ... コマンド引数を行毎に記述する。文字コードは Shift_JIS
