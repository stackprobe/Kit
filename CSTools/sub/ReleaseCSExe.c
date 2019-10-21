/*
	ReleaseCSExe.exe リリース元ルートDIR リリース先DIR
*/

#include "C:\Factory\Common\all.h"

static char *RRootDir;
static char *WDir;

static void Main2(void)
{
	autoList_t *dirs = lsDirs(RRootDir);
	char *dir;
	uint index;

	foreach(dirs, dir, index)
	{
		char *localDir = getLocal(dir);
		char *rExeFile;

		rExeFile = xcout("%s\\%s\\bin\\Release\\%s.exe", dir, localDir, localDir);

		if(existFile(rExeFile))
		{
			char *wExeFile = combine(WDir, getLocal(rExeFile));

			cout("< %s\n", rExeFile);
			cout("> %s\n", wExeFile);

			copyFile(rExeFile, wExeFile);

			memFree(wExeFile);
		}
		memFree(rExeFile);
	}
	releaseDim(dirs, 1);
}
int main(int argc, char **argv)
{
	RRootDir = makeFullPath(nextArg());
	WDir     = makeFullPath(nextArg());

	errorCase(!existDir(RRootDir));
	errorCase(!existDir(WDir));
	errorCase(!_stricmp(RRootDir, WDir));

	Main2();
}
