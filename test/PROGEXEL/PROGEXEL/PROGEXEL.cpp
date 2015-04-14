#include <stdio.h>
#include <stdlib.h>
class line
{
public:
	double a,b,c;
	line(double x1, double y1, double x2, double y2)
	{
		this->a = y2 - y1; this->b = x1 - x2; this->c = x1*(y1 - y2) + y1*(x2 - x1);
	}
	void write()
	{
		printf("%lf ", a); printf("%lf ", b); printf("%lf ", c);
		printf("\n");
	}
	bool koll(const line *d)
	{
		if (this->a*d->b - d->a*this->b == 0)
			return true;
		else
		{
			return false;
		}
	}
	void intersect(line *d,double *x, double *y)
	{
		if (!this->koll(d))
		{
			double det = this->a*d->b - d->a*this->b;
			*x = -(this->c*d->b - this->b*d->c) / det;
			*y = -(this->a*d->c - this->c*d->a) / det;
		}
	}
};
void swap(double *a, double *b)
{
	*a = *a + *b;
	*b = *a - *b;
	*a = *a - *b;
}
void qsort(double* s_arr, int first, int last)
{
	int i = first, j = last, x = s_arr[(first + last) / 2];

	do {
		while (s_arr[i] < x) i++;
		while (s_arr[j] > x) j--;

		if (i <= j) {
			if (s_arr[i] > s_arr[j]) swap(&s_arr[i], &s_arr[j]);
			i++;
			j--;
		}
	} while (i <= j);

	if (i < last)
		qsort(s_arr, i, last);
	if (first < j)
		qsort(s_arr, first, j);
}
void TochToArr(int x1, int y1, double *ox, double *oy, double *xy, double *yx,int i)
{
	if (y1 == 0)
		ox[i] = x1;
	if (y1 == 100)
		yx[i] = x1;
	if (x1 == 0)
		oy[i] = y1;
	if (x1 == 100)
		xy[i] = y1;
}
void main()
{
	int n;
	double x1, x2, y1, y2, t1, t2, *ox, *oy, *xy, *yx;
	FILE *f,*g;
	g = fopen("output.txt", "w");
	f = fopen("input.txt", "r");
	fscanf(f, "%d", &n);
	ox = (double*)calloc(sizeof(double),n+1);
	oy = (double*)calloc(sizeof(double),n+1);
	xy = (double*)calloc(sizeof(double),n+1);
	yx = (double*)calloc(sizeof(double),n+1);
	line ** l =(line**)malloc(sizeof(line*)*n);
	for (int i = 0; i < n; i++)
	{
		fscanf(f, "%lf", &x1);
		fscanf(f, "%lf", &y1);
		TochToArr(x1, y1, ox, oy, xy, yx, i+1);
		fscanf(f, "%lf", &x2);
		fscanf(f, "%lf", &y2);
		TochToArr(x2, y2, ox, oy, xy, yx, i+1);
		l[i] = (line*)new line(x1,y1,x2,y2);
	}
	ox[0] = 0; oy[0] = 0; xy[0] = 0; yx[0] = 0;
	ox[n+1] = 100; oy[n+1] = 100; xy[n+1] = 100; yx[n+1] = 100;
	fscanf(f, "%lf", &t1); fscanf(f, "%lf", &t2);
	qsort(ox, 1, n); qsort(oy, 1, n); qsort(xy, 1, n); qsort(yx, 1, n);
	double x, y;
	int temp = 0, rezult[] = {n,n,n,n};
	for (int i = 1; i < n + 2; i++)
		if (ox[i] != 0)
		{
			x = (ox[i - 1] + ox[i]) / 2;
			y = 0;
			line *v = new line(x, y, t1, t2);
			for (int j = 0; j < n; j++)
			{
				v->intersect(l[j],&x,&y);
				if (y>0 && y < t2)
					temp++;
			}
			delete[] v;
			if (temp < rezult[0])
				rezult[0] = temp;
			temp = 0;
		}
	for (int i = 1; i < n + 2; i++)
		if (oy[i] != 0)
		{
			y = (oy[i - 1] + oy[i]) / 2;
			x = 0;
			line *v = new line(x, y, t1, t2);
			for (int j = 0; j < n; j++)
			{
				v->intersect(l[j], &x, &y);
				if (x>0 && x < t1)
					temp++;
			}
			delete[] v;
			if (temp < rezult[1])
				rezult[1] = temp;
			temp = 0;
		}
	for (int i = 1; i < n + 2; i++)
		if (xy[i] != 0)
		{
			y = (xy[i - 1] + xy[i]) / 2;
			x = 100;
			line *v = new line(x, y, t1, t2);
			for (int j = 0; j < n; j++)
			{
				v->intersect(l[j], &x, &y);
				if (x>t1 && x <100)
					temp++;
			}
			delete[] v;
			if (temp < rezult[2])
				rezult[2] = temp;
			temp = 0;
		}
	for (int i = 1; i < n + 2; i++)
		if (yx[i] != 0)
		{
			x = (yx[i - 1] + yx[i]) / 2;
			y = 100;
			line *v = new line(x, y, t1, t2);
			for (int j = 0; j < n; j++)
			{
				v->intersect(l[j], &x, &y);
				if (y>t2 && y < 100)
					temp++;
			}
			delete[] v;
			if (temp < rezult[3])
				rezult[3] = temp;
			temp = 0;
		}
	int min = rezult[0];
	for (int i = 1; i < 4; i++)
		if (min>rezult[i])
			min = rezult[i];
	fprintf(g,"Number of doors: %d", min + 1);
}