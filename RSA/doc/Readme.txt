----
コマンド

	鍵生成

		RSA.exe /G 鍵ビット数 公開鍵ファイル 秘密鍵ファイル

	暗号化

		RSA.exe /E 公開鍵ファイル 平文ファイル 暗号ファイル
		RSA.exe /E 秘密鍵ファイル 平文ファイル 暗号ファイル

	復号

		RSA.exe /D 秘密鍵ファイル 暗号ファイル 平文ファイル

----
実行例

	鍵生成

		rsa /g 3072 pub.xml prv.xml

	暗号化

		rsa /e pub.xml 123.txt 123.enc

	復号

		rsa /d prv.xml 123.enc 123_dec.txt
