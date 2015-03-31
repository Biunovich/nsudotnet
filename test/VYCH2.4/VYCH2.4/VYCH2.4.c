#define N 100 
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
double funk(double x)
{
	return (pow(x, 3) - (2.2)*pow(x, 2) + (1.56)*x - 0.36);
}
double deffunk(double x)
{
	return (3 * pow(x, 2) - (4.4)*x + 1.56);
}
void separ(double *arr, int n)
{
	double step = 3.0 / n;
	int i;
	for (i = 0; i < n; i++)
		if (funk(-1 + step*i)*funk(-1 + step*(i + 1)) < 0){
			arr[i] = 1;
		}
		else if (deffunk(-1 + step*i)*deffunk(-1 + step*(i + 1)) < 0)
		{
			arr[i] = 2;
		}
		else
		{
			arr[i] = 0;
		}
		for (i = 0; i < n; i++)
			if (arr[i] != 0)
				printf("%lf %lf %lf\n", arr[i],(-1+step*i),(-1+(i+1)*step));
}
double bisek(double x, double y,double eps)
{
	while (fabs(x - y) > eps)
	{
		if (funk((x + y) / 2) == 0)
			return (x + y) / 2;
		else if (funk(x)*funk((x+y)/2)< 0)
		{
			return bisek(x, (x + y) / 2, eps);
		}
		else
		{
			return bisek((x + y) / 2, y, eps);
		}
	}
	return (x + y) / 2;
}
double defbisek(double x, double y, double eps)
{
	while (fabs(x - y) > eps)
	{
		if (deffunk((x + y) / 2) == 0)
		{
			if (fabs(funk((x + y) / 2)) < eps)
				return (x + y) / 2;
		}
		else if (deffunk(x)*deffunk((x + y) / 2) < 0)
		{
			return defbisek(x, (x + y) / 2, eps);
		}
		else
		{
			return defbisek((x + y) / 2, y, eps);
		}
	}
	if (fabs(funk((x + y) / 2)) < eps)
		return (x + y) / 2;
	else
	{
		return -9999;
	}
}
double newton(double x,double eps)
{
	double x1;
	x1 = x - (funk(x) / deffunk(x));
	while (fabs(x-x1)>eps)
	{
		x = x1;
		x1 = x1 - (funk(x1) / deffunk(x1));
	}
	return x1;
}
double paramnewton(double x, double eps)
{
	double x1;
	x1 = x - 2 * (funk(x) / deffunk(x));
	while (fabs(x1-x)>eps)
	{
		x = x1;
		x1 = x - 2 * (funk(x) / deffunk(x));
	}
	return x1;
}
void main()
{
	int i;
	double *arr,eps,step = 3.0/N,rez;
	arr = malloc(sizeof(double)*N);
	separ(arr, N);
	printf("ENTER EPS: ");
	scanf("%lf", &eps);
	for (i = 0; i < N; i++)
		if (arr[i] == 1)
			printf("GENERAL METHOD BISEK: %lf\n",bisek((-1 + step*i), (-1 + step*(i + 1)), eps));
	for (i = 0; i < N;i++)
		if (arr[i] == 2){
			rez = defbisek((-1 + step*i), (-1 + step*(i + 1)), eps);
			if (rez != -9999)
				printf("DIFFERENTIAL METHOD BISEK: %lf\n", rez);
		}
	for (i = 0; i < N; i++)
		if (arr[i] == 1)
			printf("NEWTON METHOD: %lf\n", newton(((-1 + step*i) + (-1 + step*(i + 1))) / 2, eps));
	for (i = 0; i < N; i++)
		if (arr[i] != 0)
			printf("NEWTON WITH PARAMETRS: %lf\n", paramnewton(((-1 + step*i) + (-1 + step*(i + 1))) / 2, eps));
}