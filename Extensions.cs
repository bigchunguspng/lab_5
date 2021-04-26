namespace Lab_5
{
    static class Extensions
    {
        public static bool StartsWithNumber(this string line, int number) => line.StartsWith($"#{number}:");
    }
}