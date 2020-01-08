using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ArrayRotationTest
    {
        [TestMethod]
        public void Test_Juggling_left()
        {
            var expected = new[] { 3, 4, 5, 6, 7, 8, 1, 2 };
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var arrayRotation = new ArrayRotation();
            arrayRotation.LeftRotate(arr, 2);

            CollectionAssert.AreEqual(expected, arr);
        }

        [TestMethod]
        public void Test_Left()
        {
            var expected = new[] { 3, 4, 5, 6, 7, 8, 1, 2 };
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var arrayRotation = new ArrayRotation();
            arrayRotation.LeftRotate2(arr, 2);

            CollectionAssert.AreEqual(expected, arr);
        }
    }

    public class ArrayRotation
    {
        //Juggling Algorithm
        public void LeftRotate(int[] arr, int number)
        {
            int gcd = Gcd(arr.Length, number);

            for (int i = 0; i < gcd; i++)
            {
                var temp = arr[i];
                int k = 0; //store the next element
                int j = i; // store the current element
                while (true)
                {
                    k = j + number;
                    if (k >= arr.Length)
                        k -= arr.Length;
                    if (k == i)
                        break;
                    arr[j] = arr[k];
                    j = k;
                }

                arr[j] = temp;
            }
        }

        private static int Gcd(int a, int b)
        {
            if (b == 0)
                return a;

            return Gcd(b, a % b);
        }


        public void RightRotate(int[] arr, int number)
        {
            int n = arr.Length;

            if (n <= 1)
                return;

            number = number % n;
            if (number == 0)
                return;// since it will be whole round

            Reverse(arr, 0, n-number-1);
            Reverse(arr, n - number, n -1);
            Reverse(arr, 0, n-1);
        }

        public void LeftRotate2(int[] arr, int number)
        {
            int n = arr.Length;

            if (n <= 1)
                return;

            number = number % n;
            if (number == 0)
                return;// since it will be whole round

            Reverse(arr, 0, n - 1);
            Reverse(arr, n - number, n - 1);
            Reverse(arr, 0, n - number - 1);
        }

        private static void Reverse(int[] arr, int i, int j)
        {
            while (i < j)
            {
                Swap(arr, i, j);
                i++;
                j--;
            }
        }

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}