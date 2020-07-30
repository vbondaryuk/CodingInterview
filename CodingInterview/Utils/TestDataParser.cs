using System;
using System.Collections.Generic;
using System.IO;

namespace CodingInterview.Utils
{
    public class TestDataParser
    {
        public static T Parse<T>(Action<Options<T>> optionBuilder)
        {
            var option = new Options<T>();
            optionBuilder(option);
            var filePath = Path.Combine(Environment.CurrentDirectory, option.FileName);
            if (option.ParseByLine)
            {
                var instance = option.Initialize();
                int lineNumber = 0;
                foreach (var line in File.ReadLines(filePath))
                {
                    if (option.SkipEmptyRow && string.IsNullOrWhiteSpace(line))
                        continue;

                    option.ByLineParse(instance, line, lineNumber);
                    lineNumber++;
                }
                
                return instance;
            }

            return option.Parse(File.ReadAllText(filePath));
        }

        public static IEnumerable<T> MultiCaseParse<T>(Action<MultiCaseOptions<T>> optionBuilder)
        {
            var option = new MultiCaseOptions<T>();
            optionBuilder(option);
            var filePath = Path.Combine(Environment.CurrentDirectory, option.FileName);
            var cases = File.ReadAllText(filePath).Split(option.CaseSeparator);
            foreach (var testCase in cases)
            {
                if (option.ParseByLine)
                {
                    var instance = option.Initialize();
                    int lineNumber = 0;
                    foreach (var line in testCase.Split(new []{"\r", "\n", "\r\n"}, StringSplitOptions.None))
                    {
                        if (option.SkipEmptyRow && string.IsNullOrWhiteSpace(line)) 
                            continue;

                        option.ByLineParse(instance, line, lineNumber);
                        lineNumber++;
                    }

                    yield return instance;
                }
                else
                {
                    yield return option.Parse(testCase);
                }
            }
        }

        public class Options<T>
        {
            public string FileName { get; set; }
            public bool ParseByLine { get; set; }
            public Func<T> Initialize { get; set; }
            public Action<T, string, int> ByLineParse { get; set; }
            public Func<string, T> Parse { get; set; }
            public bool SkipEmptyRow = true;
        }

        public class MultiCaseOptions<T> : Options<T>
        {
            public string CaseSeparator { get; set; }
        }
    }

    public class TestData<TInput, TOutput>
    {
        public TOutput ExpectedResult { get; set; }
        public TInput Input { get; set; }
    }
}
