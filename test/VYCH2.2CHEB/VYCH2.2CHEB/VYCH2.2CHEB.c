#define M_PI 3.14159265358979323846
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
int  vvod(double **arr)
{
	int n, i, j;
	FILE *f;
	double **x;
	f = fopen("input1.txt", "r");
	fscanf(f, "%d", &n);
	x = calloc(sizeof(double*), 2);
	for (i = 0; i < 2; i++)
	{
		x[i] = calloc(sizeof(double), n + 1);
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
double razdel(double **arr, int i)
{
	int j, k;
	double baza, rezult = 0;
	for (j = 0; j <= i; j++)
	{
		baza = 1;
		for (k = 0; k <= i; k++)
			if (k != j)
				baza = baza*(1 / (arr[0][j] - arr[0][k]));
		rezult = rezult + baza*arr[1][j];
	}
	return rezult;
}
double fuk(double **arr, int i, double toch)
{
	double rezult = 1;
	int j;
	for (j = 0; j < i; j++)
	{
		rezult *= (toch - arr[0][j]);
	}
	return rezult;
}
double nuton(double **arr, int n, double toch)
{
	int i, j;
	double rezult = 0;
	for (i = 1; i < n; i++)
	{
		rezult = rezult + razdel(arr, i) * fuk(arr, i, toch);
	}
	return rezult + arr[1][0];
}
double pogresh(double **arr, int n, double toch, double rez)
{
	int i, j;
	double rezult = 0, baza = 1;
	arr[0][n] = toch;
	arr[1][n] = rez;
	for (i = 0; i < n; i++)
	{
		baza = 1;
		for (j = 0; j < n; j++)
			if (j != i){
				baza = baza*(1 / (arr[0][i] - arr[0][j]));
			}
		rezult = rezult + baza*arr[1][i];
	}
	baza = 1;
	for (i = 0; i < n; i++)
		baza *= (toch - arr[0][i]);
	rezult = rezult *baza;
	return rezult;
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
		cheb[1][i] = log10(cheb[0][i]);
	return cheb;
}
int main()
{
	int uzl;
	double **arr, rezult, toch, **cheb, rezult1;
	uzl = vvod(&arr);
	cheb = cheb_array(arr[0][uzl - 1], arr[0][0], uzl);
	write(arr, uzl);
	write(cheb, uzl);
	printf("ENTER PARAMETR: ");
	scanf("%lf", &toch);
	rezult = nuton(arr, uzl, toch);
	rezult1 = nuton(cheb, uzl, toch);
	printf("ANSWER: %lf\nCHEB: %lf\n", rezult, rezult1);
	printf("POGRESHNOST: %lf\nCHEB: %lf\n", pogresh(arr, uzl, toch, rezult), pogresh(cheb, uzl, toch, rezult1));
	return 0;
}