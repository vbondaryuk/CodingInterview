using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ChocolateFeastTest
    {
        [TestMethod]
        public void Test()
        {
            var result = ChocolateFeast(10, 2, 4);

            Assert.AreEqual(6, result);
        }

        //https://www.hackerrank.com/challenges/chocolate-feast/problem
        static int ChocolateFeast(int n, int c, int m)
        {
            int chocoEat = n / c;
            int chocoWrappers = chocoEat;

            int chocoCount;
            do
            {
                chocoCount = chocoWrappers / m;
                chocoEat += chocoCount;
                chocoWrappers = chocoWrappers % m;
                chocoWrappers += chocoCount;
            } while (chocoCount > 0 || chocoWrappers > m);

            return chocoEat;
        }
    }
}
