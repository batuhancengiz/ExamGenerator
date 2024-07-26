using System.Collections.ObjectModel;

namespace ExamGenerator.Utilities
{
    public static class Extensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this ObservableCollection<T> collection)
        {
            int n = collection.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = collection[k];
                collection[k] = collection[n];
                collection[n] = value;
            }
        }
    }
}
