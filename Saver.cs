using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_5
{
    public class Saver
    {
        public int Count;
        private string _path;
        private static Saver _instance;
        
        private Saver() { }
        
        public static Saver Instance => _instance ?? (_instance = new Saver());

        

        public void OpenOrCreateFile(string path)
        {
            Directory.CreateDirectory(path.Remove(path.LastIndexOf('\\')));
            File.OpenWrite(path).Dispose();
            _path = path;
            UpdateCount();
        }

        public void WriteInEmptyPlace(Matrix matrix)
        {
            Count++;
            bool saved = false;
            string[] content = FileContent();
            for (int i = 0; i < content.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(content[i]))
                {
                    content[i] = SerializeMatrix(matrix);
                    saved = true;
                }
            }

            if (!saved)
            {
                Count--;
                Write(matrix);
            }
            File.WriteAllLines(_path, content);
        }

        public Matrix Read(int number)
        {
            string[] content = FileContent();
            foreach (string line in content)
            {
                if (line.StartsWithNumber(number))
                {
                    return Matrix.Deserialize(line);
                }
            }
            return new Matrix(1);
        }

        public void Replace(Matrix matrix, int number)
        {
            string[] content = FileContent();
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i].StartsWithNumber(number))
                {
                    content[i] = $"#{number}: {matrix.Serialize()}";
                    File.WriteAllLines(_path, content);
                    return;
                }
            }
        }

        public void DeleteMatrix(int number)
        {
            string[] content = FileContent();
            for (var i = 0; i < content.Length; i++)
            {
                if (content[i].StartsWithNumber(number))
                {
                    content[i] = "";
                    Count--;
                    File.WriteAllLines(_path, content);
                    return;
                }
            }
        }

        public void Defragment()
        {
            string[] oldContent = FileContent();
            List<string> newContent = new List<string>();
            foreach (string line in oldContent)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    newContent.Add(line);
                }
            }

            File.WriteAllLines(_path, newContent);
        }
        
        public void Write(Matrix matrix)
        {
            Count++;
            File.AppendAllText(_path, SerializeMatrix(matrix) + "\n");
        }

        public void Write(string text) => File.AppendAllText(_path, text + "\n");

        public void ClearFile()
        {
            Count = 0;
            File.WriteAllText(_path, string.Empty);
        }

        private string SerializeMatrix(Matrix matrix) => $"#{Count}: {matrix.Serialize()}";
        private string[] FileContent() => File.ReadAllLines(_path);
        private void UpdateCount()
        {
            int max = 0;
            
            string[] content = FileContent();
            foreach (string line in content)
            {
                int n = 0;
                if (Int32.TryParse(line, out n) && n > max)
                {
                    max = n;
                }
            }

            Count = max;
        }
    }
}