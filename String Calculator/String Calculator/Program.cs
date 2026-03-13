using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalulator
{
    public class StringCalculator
    {
        public int Calculate(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                return 0;

            List<string> delimiters = new List<string> { ",", "\n" };

            if (arg.StartsWith("//"))
            {
                int newlineIndex = arg.IndexOf('\n');
                string delimiterPart = arg.Substring(2, newlineIndex - 2);

                if (delimiterPart.StartsWith("["))
                {
                    MatchCollection matches = Regex.Matches(delimiterPart, @"\[(.*?)\]");
                    delimiters.Clear();
                    delimiters.Add("\n");

                    foreach (Match match in matches)
                    {
                        delimiters.Add(match.Groups[1].Value);
                    }
                }
                else
                {
                    delimiters = new List<string> { delimiterPart, "\n" };
                }

                arg = arg.Substring(newlineIndex + 1);
            }

            string[] parts = arg.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;

            foreach (var part in parts)
            {
                int number = int.Parse(part);

                if (number < 0)
                    throw new ArgumentOutOfRangeException(part, "Numbers should be positive");

                if (number > 1000)
                    continue;

                sum += number;
            }

            return sum;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}