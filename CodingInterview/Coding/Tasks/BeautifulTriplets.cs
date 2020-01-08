using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class BeautifulTripletsTest
    {
        [TestMethod]
        [DataRow(3, new int[]{ 1,2,4,5,7,8,10 }, 3)]
        [DataRow(3, new int[]{ 1,6,7,7,8,10,12,13,14,19 }, 2)]
        public void Test(int d, int[] arr, int expected)
        {
            var result = BeautifulTriplets.beautifulTriplets(d, arr);

            Assert.AreEqual(expected, result);

        }
    }

    //https://www.hackerrank.com/challenges/beautiful-triplets/problem
    public class BeautifulTriplets
    {
        // a[j]-a[i]=a[k]-a[j]=d

        //a[j]=a[i]+d
        //a[k]-a[j]=d => a[k] = (a[i] + d) + d => a[k] = a[i] + 2 * d
        public static int beautifulTriplets(int d, int[] arr)
        {
            var hash = new HashSet<int>(arr);
            int number = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                int aj = arr[i] + d;
                int ak = aj + d;
                //check if ak and aj contains in hash
                if (hash.Contains(aj) && hash.Contains(ak))
                {
                    // check uniquiness that the previous not equals to current
                    // if (i > 0 && arr[i] == arr[i - 1])
                    //     continue;

                    number++;
                }
            }

            return number;
        }
    }
}