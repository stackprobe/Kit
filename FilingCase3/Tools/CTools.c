#include "C:\Factory\Common\all.h"

#define RET_PREFIX "*RET="

static void Main2(void)
{
	if(argIs("/F"))
	{
		cout(RET_PREFIX "%s\n", toFairFullPathFltr(makeFullPath(nextArg()))); // gomi
		return;
	}
}
int main(int argc, char **argv)
{
	Main2();
	termination(0);
}
