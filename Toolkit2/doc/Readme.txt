----
コマンド

	Toolkit2.exe /CS-PROJ-ADJUST-CS プロジェクトのディレクトリ プロジェクトファイルのローカル名 ソースディレクトリの相対パス 成功時作成ファイル

		ex.
			Toolkit2.exe /CS-PROJ-ADJUST-CS C:\Dev\Game3\Brilliant\Brilliant\Brilliant Brilliant.csproj Common C:\temp\Successful.flg

		ソースディレクトリと、そのソースディレクトリ内のソースファイルがプロジェクトファイルに１つでも登録されていなければならない。
			パスの間違いによる誤実行防止のため。

		初めて実行する場合 hint
			1. ソースディレクトリの作成
			2. Class1.cs など適当なクラスの追加
			3. CopyLib 実行
