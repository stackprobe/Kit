----
�R�}���h

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

		/-TLS12 ... TLS 1.2 �𖳌��ɂ���B

		/K  ... �I�����L�[�҂��B
		/K- ... �G���[�I�����L�[�҂��B

----
�R�}���h�������t�@�C���Ŏw�肷��B

	HGet.exe //R args-file

		args-file ... �R�}���h�������s���ɋL�q����B�����R�[�h�� Shift_JIS
