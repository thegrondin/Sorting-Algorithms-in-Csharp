using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting_Algo
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = GenerateRandom();

            var algorithms = new Dictionary<string, Func<List<int>, List<int>>>();

            
            algorithms.Add("SelectionSort", SelectionSort);
            algorithms.Add("InsertionSort", InsertionSort);
            algorithms.Add("MergeSort", MergeSort);
            algorithms.Add("QuickSortWithMerge", QuickSortWithMerge);
            algorithms.Add("QuickSort", QuickSort);

            var watch = new System.Diagnostics.Stopwatch();

            foreach (var algo in algorithms)
            {
                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                algo.Value(new List<int>(list));
                Console.WriteLine(algo.Key + " Finished. Sorting time : " + watch.ElapsedMilliseconds + "ms");
                watch.Stop();
               
            }

        }

        static List<int> SelectionSort(List<int> list)
        {
            var marker = 0;
            while (marker != list.Count - 1)
            {
                int min = list[marker];
                for (int i = marker; i < list.Count; i++)
                {
                    if (min > list[i]) min = list[i];
                }

                Swap(list.IndexOf(min), marker, ref list);
                marker++;
            }

            return list;
        }

        static List<int> InsertionSort(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                var key = list[i];
                var j = i - 1;

                while (j >= 0 && key < list[j])
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = key;
            }

            return list;
        }

        static List<int> Merge(List<int> first, List<int> second)
        {
            var merged = new List<int>();

            while (first.Count > 0 && second.Count > 0)
            {
                if (first[0] > second[0])
                {
                    merged.Add(second[0]);
                    second.RemoveAt(0);
                }
                else
                {
                    merged.Add(first[0]);
                    first.RemoveAt(0);
                }
            }


            while (first.Count > 0)
            {
                merged.Add(first[0]);
                first.RemoveAt(0);
            }

            while (second.Count > 0)
            {
                merged.Add(second[0]);
                second.RemoveAt(0);
            }

            return merged;
        }

        static List<int> MergeSort(List<int> list)
        {

            if (list.Count == 1) return list;

            var first = list.Take((list.Count + 1) / 2).ToList();
            var second = list.Skip((list.Count + 1) / 2).ToList();

            first = MergeSort(first);
            second = MergeSort(second);

            return Merge(first, second);

        }

        static List<int> Partition(List<int> list)
        {
            return list;
        }

        static List<int> QuickSortWithMerge(List<int> list)
        {
            if (list.Count <= 1) return list;

            var pivot = list.Last();
            var i = -1;

            for (int j = 0; j < list.Count - 2; j++)
            {
                if (list[j] < pivot)
                {
                    i++;
                    Swap(i, j, ref list);
                }
            }

            list.Insert(i + 1, pivot);
            list.RemoveAt(list.Count - 1);

            var first = list.Take(i+1).ToList();
            var second = list.Skip(i+2).ToList();

            first = QuickSortWithMerge(first);
            second = QuickSortWithMerge(second);

            first.Add(pivot);

            return Merge(first, second);
        }


        static List<int> QuickSort(List<int> list)
        {
            if (list.Count <= 1) return list;
            int pivotPosition = list.Count / 2;
            int pivotValue = list[pivotPosition];
            list.RemoveAt(pivotPosition);
            List<int> smaller = new List<int>();
            List<int> greater = new List<int>();
            foreach (int item in list)
            {
                if (item < pivotValue)
                {
                    smaller.Add(item);
                }
                else
                {
                    greater.Add(item);
                }
            }
            List<int> sorted = QuickSort(smaller);
            sorted.Add(pivotValue);
            sorted.AddRange(QuickSort(greater));
            return sorted;
        }

        static List<int> GenerateRandom()
        {
            var list = new List<int>();
            var rand = new Random();

            for (int i = 0; i < 50000; i++)
            {
                var randNumber = rand.Next(0, 500000);

                if (list.Contains(randNumber)) continue;

                list.Add(randNumber);
            }

            return list;
        }

        static void Swap(int x, int y, ref List<int> list)
        {
            var temp = list[x];
            list[x] = list[y];
            list[y] = temp;
        }

        static void DisplayResult (List<int> list)
        {
            var output = "[";
            foreach (int el in list) {
                if (list.IndexOf(el) == list.Count - 1)
                {
                    output += el;
                }
                else
                {
                    output += el + ", ";
                }
            }

            output += "]";

            Console.WriteLine(output);
        }
    }
}
