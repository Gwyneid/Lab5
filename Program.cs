using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
int n = 10;
double[] U = new double[10];
int[] A = new int[10] { 0, 1, 15, 15, 19, 1, 12, 10, 25, 15 };
int[] B = new int[10] { 19, 74, 84, 98, 52, 89, 88, 56, 81, 78 };
int[] C = new int[10] { 57, 60, 34, 19, 26, 67, 22, 40, 50, 0 };
int[] F = new int[10] { 79, 96, 20, 59, 88, 47, 36, 76, 75, 16 };
double y;
double[] a = new double[10];
double[] b = new double[10];
y = B[0];
a[0] = -C[0] / y;
b[0] = F[0] / y;

for (int i = 1; i < n - 1; i++)
{
    y = B[i] + A[i] * a[i - 1];
    a[i] = -C[i] / y;
    b[i] = (F[i] - A[i] * b[i - 1]) / y;
}

U[n - 1] = (F[n - 1] - A[n - 1] * b[n - 2]) / (B[n - 1] + A[n - 1] * a[n - 2]);

for (int i = n - 2; i >= 0; i--)
{
    U[i] = a[i] * U[i + 1] + b[i];
}


var matA = Matrix<double>.Build.DenseOfArray(new double[,] {
    { 19, 57, 0, 0, 0, 0, 0, 0, 0, 0},
    { 1, 74, 60 ,0,  0, 0, 0, 0, 0, 0},
    { 0, 15, 84, 34, 0, 0, 0, 0, 0, 0},
    { 0, 0, 15, 98, 19, 0, 0, 0, 0, 0},
    { 0, 0, 0, 19, 52, 26, 0, 0, 0, 0},
    { 0, 0, 0, 0, 1, 89, 67, 0, 0, 0},
    { 0, 0, 0, 0, 0, 12, 88, 22, 0, 0},
    { 0, 0, 0, 0, 0, 0, 10, 56, 40, 0},
    { 0, 0, 0, 0, 0, 0, 0, 25, 81, 50},
    { 0, 0, 0, 0, 0, 0, 0, 0, 15, 78}
});
var d = Vector<double>.Build.Dense(new double[] { 79, 96, 20, 59, 88, 47, 36, 76, 75, 16 });
var x = matA.Solve(d);

Console.WriteLine(matA);
Console.WriteLine(("").PadRight(24, '-'));

Console.WriteLine("Решение монотонной прогонки:             Решение СЛАУ:");
Console.WriteLine("");
for (int i = 0; i < n; i++)
{
    Console.WriteLine(U[i]+"                   " + x[i]);
}


Console.WriteLine(("").PadRight(24, '-'));

for (int i = 0; i < n; i++)
{
    Console.WriteLine(Math.Abs(U[i] - x[i]));
}