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
int main()
{
	int n[] = {20 , 50 ,100}, i;
	double znach;
	printf("FORMULA PRYAMOUG\n");
	for (i = 0; i < 3; i++){
		znach = integral_pryam(n[i]);
		printf("SETKA: %d  ANSWER: %lf\n", n[i], znach);
	}
	return 0;
}