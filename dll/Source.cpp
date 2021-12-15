
#include <time.h>
#include "mkl.h"
#include "mkl_vml_functions.h"
#include <iostream>
#include "Header.h"

void mkl_sin(int n, double* a, double* y, char m)
{
	//std::cout << "2";
	MKL_INT64 mode;
	if (m == 'H') mode = VML_HA | VML_FTZDAZ_OFF | VML_ERRMODE_DEFAULT;
	else mode = VML_EP | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	vmdSin(n, a, y, mode);
}

/*
void ppp()
{
	std::cout << "111";
}
*/