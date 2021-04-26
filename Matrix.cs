using System;

namespace Lab_5
{
    public class Matrix
    {
        public readonly int N;
        private readonly int[,] _values;

        public Matrix(int n)
        {
            N = n;
            _values = new int[N, N];
        }

        public int this[int i, int j]
        {
            get => _values[i, j];
            set => _values[i, j] = value;
        }

        public void RandomFill(int range = 10)
        {
            Random random = new Random();
            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                this[i, j] = random.Next(-range, range);

        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.N);
            
            for (int i = 0; i < result.N; i++)
            for (int j = 0; j < result.N; j++)
            for (int k = 0; k < result.N; k++)
                result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public int Determinant()
        {
            int result = 0;
            if (N <= 3)
            {
                for (int i = 0; i < N; i++)
                {
                    int m = 1;
                    for (int j = 0; j < N; j++)
                        m *= this[j, (j + i) % N];
                    result += m;
                }
                for (int i = 0; i < N; i++)
                {
                    int m = 1;
                    for (int j = 0; j < N; j++)
                        m *= this[j, (2 * N - 1 - j - i) % N];
                    result -= m;
                }
            }
            else
            {
                for (int j = 0; j < N; j += 2) result += this[0, j] * Addition(0, j).Determinant();
                for (int j = 1; j < N; j += 2) result -= this[0, j] * Addition(0, j).Determinant();
            }
            
            return result;
        }

        private Matrix Addition(int i, int j)
        {
            Matrix result = new Matrix(N - 1);

            int x = 0;
            for (int a = 0; a < N; a++)
            {
                if (a == i)
                    continue;
                int y = 0;
                for (int b = 0; b < N; b++)
                {
                    if (b == j)
                        continue;
                    result[x, y] = this[a, b];
                    y++;
                }
                x++;
            }

            return result;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(N);
            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                result[i, j] = this[j, i];

            return result;
        }

        public string Serialize()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                result += this[i, j] + " ";

            return result.Trim();
        }

        public static Matrix Deserialize(string line)
        {
            Matrix result;
            line = line.Trim();
            string[] content = line.Split(' ');
            int length = content.Length - 1;
            int n = (int) Math.Round(Math.Sqrt(length));
            if (Math.Abs(Math.Sqrt(length) - Math.Round(Math.Sqrt(length))) < 0.0001)
            {
                result = new Matrix(n);
                for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    result[i, j] = Int32.Parse(content[1 + i * n + j]);
            }
            else
                result = new Matrix(1);

            return result;
        }
    }
}