#pragma once


extern "C" __declspec(dllexport) void mkl_sin(const int n, double* a, double* y, char m);
extern "C" __declspec(dllexport) void mkl_cos(const int n, double* a, double* y, char m);
//extern "C" __declspec(dllexport) void ppp();
