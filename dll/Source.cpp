
#include <time.h>
#include "mkl.h"
#include "mkl_vml_functions.h"
#include <iostream>
#include "Header.h"

void mkl_sin(int n, double* a, double* y, char m)
{
	//std::cout << "\n\n2\n\n";
	MKL_INT64 mode;
	if (m == 'H') mode = VML_HA | VML_FTZDAZ_OFF | VML_ERRMODE_DEFAULT;
	else if (m == 'E') mode = VML_EP | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	else mode = VML_LA | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	vmdSin(n, a, y, mode);
}

void mkl_cos(int n, double* a, double* y, char m)
{
	MKL_INT64 mode;
	if (m == 'H') mode = VML_HA | VML_FTZDAZ_OFF | VML_ERRMODE_DEFAULT;
	else if (m == 'E') mode = VML_EP | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	else mode = VML_LA | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	vmdCos(n, a, y, mode);
}

void mkl_sincos(int n, double* a, double* y, double* z, char m)
{
	//std::cout << "2";
	MKL_INT64 mode;
	if (m == 'H') mode = VML_HA | VML_FTZDAZ_OFF | VML_ERRMODE_DEFAULT;
	else if (m == 'E') mode = VML_EP | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	else mode = VML_LA | VML_FTZDAZ_ON | VML_ERRMODE_DEFAULT;
	vmdSinCos(n, a, y, z, mode);
}

/*
void ppp()
{
	std::cout << "111";
}
*/