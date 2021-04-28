using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting_Algo
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = new List<int> { 5, 3, 1, 4, 2, 9, 6, 8, 7 };

            DisplayResult(SelectionSort(new List<int>(list)));
            DisplayResult(InsertionSort(new List<int>(list)));
            DisplayResult(MergeSort(new List<int>(list)));
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
