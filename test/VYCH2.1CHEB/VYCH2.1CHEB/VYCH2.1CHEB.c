#define M_PI 3.14159265358979323846
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
int  vvod1(double **arr)
{
	int n, i, j;
	FILE *f;
	double **x;
	f = fopen("input1.txt", "r");
	fscanf(f, "%d", &n);
	x = calloc(sizeof(double*), 2);
	for (i = 0; i < 2; i++)
	{
		x[i] = calloc(sizeof(double), n);
		for (j = 0; j < n; j++)
			fscanf(f, "%lf", &x[i][j]);
	}
	fclose(f);
	*arr = x;
	return n;
}
int  vvod(double **arr)
{
	int n, i, j;
	FILE *f;
	double **x;
	f = fopen("input.txt", "r");
	fscanf(f, "%d", &n);
	x = calloc(sizeof(double*), 2);
	for (i = 0; i < 2; i++)
	{
		x[i] = calloc(sizeof(double), n);
		for (j = 0; j < n; j++)
			fscanf(f, "%lf", &x[i][j]);
	}
	fclose(f);
	*arr = x;
	return n;
}
void write(double **arr, int n)
{
	int i, j;
	for (i = 0; i < 2; i++)
	{
		printf("\n");
		for (j = 0; j < n; j++)
			printf("%lf ", arr[i][j]);
	}
	printf("\n");
}
double lagr(double **arr, int n, double toch)
{
	int i, j;
	double rezult = 0;
	for (i = 0; i < n; i++)
	{
		double baza = 1;
		for (j = 0; j < n; j++)
			if (j != i)
			{
				baza = baza* (toch - arr[0][j]) / (arr[0][i] - arr[0][j]);
			}
		rezult += baza*arr[1][i];
	}
	return rezult;
}
int fact(int n)
{
	int i, rezult = 1;
	for (i = 1; i <= n; i++)
		rezult *= i;
	return rezult;
}
double pogresh(double **arr, int n, double toch)
{
	int i;
	double rezult;
	rezult = arr[1][1] / fact(n);
	for (i = 0; i < n; i++)
		rezult *= (toch - arr[0][i]);
	return fabs(rezult);
}
void swap(double *a, double *b)
{
	*a = *a + *b;
	*b = *a - *b;
	*a = *a - *b;
}
double ** cheb_array(double b, double a, int n)
{
	int i;
	double ** cheb;
	cheb = malloc(sizeof(double*) * 2);
	for (i = 0; i < 2; i++)
		cheb[i] = malloc(sizeof(double)*n);
	for (i = 0; i < n; i++)
		cheb[0][i] = (b + a) / 2 + (b - a)*cos(((2 * i + 1)*M_PI) / (2 * n)) / 2;
	for (i = 0; i < n / 2; i++)
		swap(&cheb[0][i], &cheb[0][n - 1 - i]);
	for (i = 0; i < n; i++)
		cheb[1][i] = exp(cheb[0][i]);
	return cheb;
}
int main()
{
	int uzl, uzl1;
	double **arr, znach, toch, **arr1, znach1, **cheb, raznost, znach2;
	uzl = vvod(&arr);
	raznost = arr[0][uzl - 1] - arr[0][0];
	cheb = cheb_array(arr[0][uzl - 1], arr[0][0], uzl);
	uzl1 = vvod1(&arr1);
	write(arr, uzl);
	write(arr1, uzl1);
	write(cheb, uzl);
	printf("ENTER POINT: ");
	scanf("%lf", &toch);
	znach = lagr(arr, uzl, toch);
	znach1 = lagr(arr1, uzl1, toch);
	znach2 = lagr(cheb, uzl, toch);
	printf("\nANSWER:\n1: %lf\n2: %lf\nCHEB: %lf\n", znach, znach1, znach2);
	//printf("DIFFERENCE 1 AND 2: %lf\n", fabs(znach - znach1));
	printf("PRIBLIZHENIE: \n1: %lf\n2: %lf\nCHEB: %lf\n", pogresh(arr, uzl, toch), pogresh(arr1, uzl1, toch), pogresh(cheb, uzl, toch));
	return 0;
}
