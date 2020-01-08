using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class KaprekarTest
    {
        [TestMethod]
        public void Test()
        {
            var kaprekar = new Kaprekar();
            kaprekar.KaprekarNumbers(77778, 77778);

        }

        public class Kaprekar
        {
            public void KaprekarNumbers(int p, int q)
            {
                bool foundKaprekar = false;
                for (long i = p; i <= q; i++)
                {
                    if (IsKaprekar(i))
                    {
                        foundKaprekar = true;
                        Console.Write(i + " ");
                    }
                }
                if(!foundKaprekar)
                    Console.Write("INVALID RANGE");
            }

            private static bool IsKaprekar(long i)
            {
                long square = i * i;
                long pos = (long)Math.Log10(i) + 1;
                long div = (long)Math.Pow(10, pos);

                long left = square / div;
                long right = square % div;

                return right + left == i;
            }
        }
    }
}