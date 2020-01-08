using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ToSmallestTest
    {
        [TestMethod]
        public void Test()
        {
            long number = 614778823971181216;
            var toSmallest=new ToSmallest();
            var q = toSmallest.Smallest(number);
        }
    }

    public class ToSmallest
    {
        public long[] Smallest(long n)
        {
            long[] arr = GetDigits(n);

            var firstIndex = 0;
            var lowestIndex = 1;
            bool checkFirst = true;
            for (int i = 1; i < arr.Length; i++)
            {
                if (checkFirst)
                {
                    if (i + 1 < arr.Length && arr[firstIndex] != arr[i + 1])
                        checkFirst = false;
                    else
                        firstIndex = i;
                }

                if (arr[lowestIndex] == arr[i] && lowestIndex == i - 1)
                    continue;

                if (arr[lowestIndex] >= arr[i])
                    lowestIndex = i;
            }

            if (lowestIndex != firstIndex)
            {
                if (arr[lowestIndex] == 0 && lowestIndex == firstIndex + 1)
                {
                    Move(arr, firstIndex, lowestIndex);
                    var temp = firstIndex;
                    firstIndex = lowestIndex;
                    lowestIndex = temp;
                }
                else if (arr[firstIndex] >= arr[lowestIndex])
                {
                    if (arr[firstIndex] > arr[firstIndex + 1] && arr[lowestIndex] >= arr[firstIndex + 1])
                    {
                        Move(arr, firstIndex, lowestIndex);
                        var temp = firstIndex;
                        firstIndex = lowestIndex;
                        lowestIndex = temp;
                    }
                    else
                    {
                        Move(arr, lowestIndex, 0);
                        firstIndex = 0;
                    }
                }
                else
                {
                    while (++firstIndex < arr.Length && arr[firstIndex] < arr[lowestIndex])
                    {}
                    Move(arr, lowestIndex, firstIndex);
                }
            }

            return new []{ToNumber(arr), lowestIndex, firstIndex };
        }

        private static long[] GetDigits(long n)
        {
            var length = (int)Math.Log10(n) + 1;
            var arr = new long[length];
            for (int i = length - 1; i >= 0; i--)
            {
                arr[i] = (int)(n % 10);
                n /= 10;
            }

            return arr;
        }

        private static long ToNumber(long[] arr)
        {
            long number = 0;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                number += arr[i] * (long)Math.Pow(10, (arr.Length - i - 1));
            }

            return number;
        }

        private static void Move<T>(T[] arr, int i, int before)
        {
            do
            {
                var increment = i > before ? i - 1 : i + 1;
                Swap(arr, i, increment);
                i = increment;
            } while (i != before);
        }

        private static void Swap<T>(T[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

    }
}