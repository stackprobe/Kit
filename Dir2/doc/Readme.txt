----
�R�}���h

	Dir2.exe DIR WRITE-FILE SUCCESSFUL-FILE

		DIR ... �f�B���N�g��
		WRITE-FILE ... �o�̓t�@�C��
		SUCCESSFUL-FILE ... �������쐬����t�@�C��

	Dir2Tools.exe (/MD DIR | /RD DIR | /DEL FILE) SUCCESSFUL-FILE

		DIR  ... �f�B���N�g��
		FILE ... �t�@�C��
		SUCCESSFUL-FILE ... �������쐬����t�@�C��

	AddFilePart.exe READ-FILE WRITE-FILE START-POS SUCCESSFUL-FILE

		READ-FILE  ... �ǂݍ��݃t�@�C��
		WRITE-FILE ... �o�̓t�@�C��
		START-POS  ... �o�͊J�n�ʒu
		SUCCESSFUL-FILE ... �������쐬����t�@�C��

	GetFilePart.exe READ-FILE WRITE-FILE START-POS READ-SIZE SUCCESSFUL-FILE

		READ-FILE  ... �ǂݍ��݃t�@�C��
		WRITE-FILE ... �o�̓t�@�C��
		START-POS  ... �ǂݍ��݊J�n�ʒu
		READ-SIZE  ... �ǂݍ��݃T�C�Y
		SUCCESSFUL-FILE ... �������쐬����t�@�C��

	SetFileTime.exe WRITE-FILE WRITE-TIME SUCCESSFUL-FILE

		WRITE-FILE ... �o�̓t�@�C��
		WRITE-TIME ... �ݒ肷��ŏI�X�V����, YYYYMMDDhhmmssLLL �`�� ex. 20160217041730123
		SUCCESSFUL-FILE ... �������쐬����t�@�C��

----
�������t�@�C���Ŏw�肷��B

	���s�t�@�C�� //R �������X�g�t�@�C��

	��

	AddFilePart.exe abc.txt def.txt 123000 s.flg

	->

	123.txt �Ɉȉ����L�q����B(Shift_JIS)

		abc.txt
		def.txt
		123000
		s.flg

	AddFilePart.exe //R 123.txt
