#include <stdio.h>
#include <stdlib.h>
#include <math.h>
double funk(double x)
{
	return x = sqrt(1 - pow(x,3));
}
double integral_pryam(int n)
{
	int i;
	double step =(double) 1 / n;
	double point = step / 2, rezult=0;
	for (i = 0; i < n; i++)
		rezult += funk(point + step*i)*step;
	return rezult;
}
double integral_trap(int n)
{
	int i;
	double step = (double)1 / n;
	double point1 = 0, point2 = step,rezult = 0;
	for (i = 0; i < n; i++)
		rezult += (funk(point1 + step*i) + funk(point2 + step*i))*step / 2;
	return rezult;
}
double integral_simp(int n)
{
	int i;
	double step = (double)1 / n;
	double point1 = 0,point2 = step/2,point3 = step,rezult = 0;
	for (i = 0; i < n; i++)
		rezult += (funk(point1 + step*i) + 4 * funk(point2 + step*i) + funk(point3 + step*i))*step / 6;
	return rezult;
}
int main()
{
	int n[] = { 20, 50, 100 }, i;
	double znach;
	printf("FORMULA PRYAMOUG\n");
	for (i = 0; i < 3; i++){
		znach = integral_pryam(n[i]);
		printf("SETKA: %d  ANSWER: %lf\n", n[i], znach);
	}
	printf("FORMULA TRAPETSYI\n");
	for (i = 0; i < 3; i++){
		znach = integral_trap(n[i]);
		printf("SETKA: %d  ANSWER: %lf\n", n[i], znach);
	}
	printf("FORMULA SIMPSONA\n");
	for (i = 0; i < 3; i++){
		znach = integral_simp(n[i]);
		printf("SETKA: %d  ANSWER: %lf\n", n[i], znach);
	}
	return 0;
}