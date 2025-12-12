using System;
using System.Linq;

namespace Lab6WinForms.Core
{
    /// <summary>Класс для работы с одномерным массивом.</summary>
    public class ArrayWorker
    {
        private int _size;
        private int[] _array;
        private Random _rnd = new Random();

        public int[] Array => _array;
        public int Size => _size;

        public ArrayWorker() : this(10) { }

        public ArrayWorker(int size)
        {
            if (size <= 0) size = 10;
            _size = size;
            _array = new int[_size];
            FillRandom();
        }

        public void FillRandom()
        {
            for (int i = 0; i < _size; i++) _array[i] = _rnd.Next(0, 501);
        }

        public void SetArray(int[] arr)
        {
            if (arr == null) throw new ArgumentNullException(nameof(arr));
            _size = arr.Length;
            _array = new int[_size];
            for (int i = 0; i < _size; i++) _array[i] = arr[i];
        }

        public int[] ArrayCopy()
        {
            int[] copy = new int[_array.Length];
            for (int i = 0; i < _array.Length; i++) copy[i] = _array[i];
            return copy;
        }

        public int[] ShellSort()
        {
            int n = _array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = _array[i];
                    int j;
                    for (j = i; j >= gap && _array[j - gap] > temp; j -= gap)
                        _array[j] = _array[j - gap];
                    _array[j] = temp;
                }
            }
            return _array;
        }

        public int[] SelectionSort()
        {
            int n = _array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                    if (_array[j] < _array[minIdx]) minIdx = j;
                int tmp = _array[i];
                _array[i] = _array[minIdx];
                _array[minIdx] = tmp;
            }
            return _array;
        }

        public int Min() => _array.Min();
        public int Max() => _array.Max();
        public double Mean() => _array.Average();
    }
}
