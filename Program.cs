using System;
using System.Threading;

namespace Lab_5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Matrix[] matrices = new Matrix[5];
            int[] dets = new int[matrices.Length];
            Saver.Instance.OpenOrCreateFile("C:\\temp\\matrices.txt");
            Saver.Instance.ClearFile();
            for (int i = 0; i < matrices.Length; i++)
            {
                matrices[i] = new Matrix(10); // для 11х11 і вище визначники дуже довго рахуються
                matrices[i].RandomFill();
                Saver.Instance.Write(matrices[i]);
                dets[i] = matrices[i].Determinant();
                PrintMatrix(Saver.Instance.Read(i + 1));
            }
            
            Saver.Instance.Write("Визначники: " + string.Join(" ", dets) + "\n");

            Matrix product = new Matrix(matrices[0].N);
            for (int i = 0; i < matrices.Length - 1; i++)
            {
                product = matrices[i] * matrices[i + 1];
                Saver.Instance.Write(product);
            }
            
            Saver.Instance.Write(product.Transpose());
            
            PrintMatrix(product);
            PrintMatrix(product.Transpose());
            
            PressTo("замінити у файлі усі пять початкових матриць на нові розміром 5 на 5");

            Matrix[] matrices2 = new Matrix[matrices.Length];
            for (int i = 0; i < matrices2.Length; i++)
            {
                matrices2[i] = new Matrix(5);
                matrices2[i].RandomFill();
                Saver.Instance.Replace(matrices2[i], i + 1);
                Thread.Sleep(100); //інакше заповнюються однаковими числами
                PrintMatrix(Saver.Instance.Read(i + 1));
            }

            for (int i = 0; i < matrices2.Length - 1; i++)
            {
                product = matrices2[i] * matrices2[i + 1];
            }
            
            Saver.Instance.Write(product);
            PrintMatrix(product);
            
            Saver.Instance.Defragment();
            
            Console.WriteLine("У файлі " + Saver.Instance.Count + " матриць");

            Console.ReadKey();

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

        private static void PressTo(string message)
        {
            Console.WriteLine("Натисніть будь-яку клавішу, щоб " + message);
            Console.ReadKey();
        }
    }
}