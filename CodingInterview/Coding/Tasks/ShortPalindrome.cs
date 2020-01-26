using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingInterview.Coding.Tasks
{
    [TestClass]
    public class ShortPalindromeTest
    {
        [TestMethod]
        public void Test()
        {
            var result = ShortPalindrome.shortPalindrome("abbac");
        }
    }

    //https://www.hackerrank.com/challenges/short-palindrome/problem
    public class ShortPalindrome
    {
        const int Mod = 1000000000 + 7;

        public static int shortPalindrome(string source)
        {
            var charCounts = new int[26];//keep how many times we can see the character
            var charSequenceCount = new int[26, 26];//keep the number of times we have "char after char"
            var palindromeNumbers = new int[26];//keep the number of times we have palindrome
            var palindromes = 0;
            for (int i = 0; i < source.Length; i++)
            {
                int index = source[i] - 'a';
                palindromes += palindromeNumbers[index] % Mod;
                palindromes %= Mod;

                for (int j = 0; j < 26; j++)
                {
                    //we will have the palindrome in case next iteration have the character palindromeNumbers[j]
                    palindromeNumbers[j] += charSequenceCount[j, index] % Mod;
                    palindromeNumbers[j] %= Mod;

                    //since we iterate through string we know that we have number of character before s[j] 
                    charSequenceCount[j, index] += charCounts[j];
                    charSequenceCount[j, index] %= Mod;
                }
                charCounts[index]++;
                charCounts[index] %= Mod;
            }

            return palindromes;
        }
    }
}
