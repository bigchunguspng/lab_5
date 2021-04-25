using System;

namespace Lab_5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Matrix m = new Matrix(4);
            m.RandomFill();
            PrintMatrix(m);
            
            Console.WriteLine(m.Determinant());
            PrintMatrix(m.Transpose());
            
            Matrix n = new Matrix(m.N);
            n.RandomFill();
            PrintMatrix(n);
            
            PrintMatrix(n * m);
            PrintMatrix(m * n);
            
            
        }

        private static void PrintMatrix(Matrix matrix)
        {
            Console.WriteLine();
            for (int i = 0; i < matrix.N; i++)
            {
                for (int j = 0; j < matrix.N; j++)
                {
                    Console.Write(matrix[i,j].ToString().PadLeft(4));
                }
                Console.WriteLine();
            }
        }
    }
}