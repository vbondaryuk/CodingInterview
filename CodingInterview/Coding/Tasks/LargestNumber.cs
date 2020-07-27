using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class LargestNumberTest
    {
        [TestMethod]
        public void Test()
        {
            var input = new List<int>{ 472, 663, 964, 722, 485, 852, 635, 4, 368, 676, 319, 412 };
            var actual = LargestNumber(input);
            Assert.AreEqual("9648527226766636354854724412368319", actual);
        }

        public static string LargestNumber(List<int> A)
        {
            if (A == null)
                return null;

            A.Sort((i, i1) =>
            {
                return int.Parse(i + "" + i1) < int.Parse(i1 + "" + i) ? 1 : -1;
            });

            if (A[0] == 0)
                return "0";

            var builder = new StringBuilder();
            foreach (var value in A)
            {
                builder.Append(value);
            }

            return builder.ToString();
        }
    }
}
