// Дылгыржапов Буин Группа 13123 ММФ 2 курс МКН biunovich@gmail.com
#include <stdio.h>
#include <stdlib.h>
class Matrix //  Создаем класс матрицы в котором будем хранить саму матрицу а так же колличество строк (n) и столбцов (v); 
{
public:
	int n, v,**Mat;
	void print() // Метод выводящий матрицу в консоле
	{
		for (int i = 0; i < this->n; i++)
		{
			for (int j = 0; j < this->v; j++)
				if (this->Mat[i][j] == 42)
					printf("%c ", this->Mat[i][j]);
				else
				{
					printf("%d ", this->Mat[i][j]);
				}
			printf("\n");
		}
		printf("\n");
	}
	void writeToFile() // Метод выводящий матрицу в файл
	{
		FILE *f = fopen("output.txt","w");
		for (int i = 0; i < this->n; i++){
			for (int j = 0; j < this->v; j++)
				if (this->Mat[i][j] == 42)
					fprintf(f, "%c ", this->Mat[i][j]);
				else
				{
					fprintf(f, "%d ", this->Mat[i][j]);
				}
			fprintf(f, "\n");
		}
		fclose(f);
	}
	~Matrix() // Деструктор
	{
		for (int i = 0; i<this->n; i++)
			free(this->Mat[i]);
		free(this->Mat);
	}
};
bool prover(Matrix *A, Matrix *B, int n, int v) //Возвращает тру если остальная часть подматрицы Б совпадает с А
{
	if (A->n + n >B->n || A->v + v > B->v) // Убираем заранее ложные варианты
		return false;
	int rez = A->n*A->v;
	int count = 0;
	for (int i = n; i < A->n+n; i++)
		for (int j = v; j<A->v+v; j++)
			if (A->Mat[i - n][j - v] == B->Mat[i][j])
				count++;
	if (count == rez)
		return true;
	else
	{
		return false;
	}
}
void change(Matrix *A, Matrix *B, int n, int v) // Изменяет матрицу Б
{
	for (int i = n; i < A->n+n; i++)
		for (int j = v; j<A->v+v; j++)
			if (B->Mat[i][j] == 1)
				B->Mat[i][j] = 2;
			else
			{
				B->Mat[i][j] = '*';
			}
}
int search(Matrix *A, Matrix *B) // Функция поиска соответствий
{
	if ((A->n>B->n) || (A->v > B->v)) // Если матрица А больше Б то выходим
		return 0;
	int s = B->n - A->n + 1, h = B->v - A->v + 1, a = A->Mat[0][0]; // Находим пределы поиска в матрице (убираем лишние итерации)
	for (int i = 0; i < s; i++)
		for (int j = 0; j < h; j++)
			if (a == B->Mat[i][j]) //Находим соответствие с верхним левым элементом матрицы А в матрице Б
				if (prover(A, B, i, j))
					change(A, B, i, j);
	return 1;
}
void main()
{
	FILE *f;
	f = fopen("input.txt","r");
	Matrix * A = new Matrix(),*B = new Matrix(); // Создаем объекты класса затем последовательно считываем размерности и сами матрицы
	fscanf(f, "%d", &A->n);
	fscanf(f, "%d", &A->v);
	A->Mat = (int**)malloc(sizeof(int*)*A->n);
	for (int i = 0; i < A->n; i++){
		A->Mat[i] = (int*)malloc(sizeof(int)*A->v);
		for (int j = 0; j < A->v; j++)
			fscanf(f, "%d", &A->Mat[i][j]);
	}
	fscanf(f, "%d", &B->n);
	fscanf(f, "%d", &B->v);
	B->Mat = (int**)malloc(sizeof(int*)*B->n);
	for (int i = 0; i < B->n; i++){
		B->Mat[i] = (int*)malloc(sizeof(int)*B->v);
		for (int j = 0; j < B->v; j++)
			fscanf(f, "%d", &B->Mat[i][j]);
	}
	A->print();
	B->print();
	search(A, B);
	B->print();
	B->writeToFile();
	B->~Matrix();
	A->~Matrix();
	fclose(f);
}