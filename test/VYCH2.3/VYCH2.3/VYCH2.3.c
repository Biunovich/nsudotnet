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
double eps_integral_trap(double eps)
{
	int i=0;
	double step = 1,rezult1 = 0, rezult2 = 1000;
	while (fabs(rezult1 - rezult2) > eps)
	{
		i++;
		rezult1 = integral_trap(step);
		step *= 2;
		rezult2 = integral_trap(step);
	}
	printf("ITERATION: %d\n", i);
	return rezult2;
}
double eps_integral_simp(double eps)
{
	int i = 0;
	double step = 1, rezult1 = 0, rezult2 = 1000;
	while (fabs(rezult1 - rezult2) > eps)
	{
		i++;
		rezult1 = integral_simp(step);
		step *= 2;
		rezult2 = integral_simp(step);
	}
	printf("ITERATION: %d\n", i);
	return rezult2;
}
int main()
{
	int n[] = { 20, 50, 100 }, i;
	double znach,eps;
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
	printf("ENTER EPS: ");
	scanf("%lf", &eps);
	printf("FORMULA TRAPETSYI\n");
	znach = eps_integral_trap(eps);
	printf("ANSWER: %lf\n", znach);
	printf("FORMULA SIMPSONA\n");
	znach = eps_integral_simp(eps);
	printf("ANSWER: %lf\n", znach);
	return 0;
}