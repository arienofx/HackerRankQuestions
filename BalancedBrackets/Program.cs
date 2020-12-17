using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{
    const string YES = "YES";
    const string NO = "NO";

    // Complete the isBalanced function below.
    static string isBalanced(string s)
    {
        if(s.Length % 2 != 0)
		{
            return NO;
		}

        Stack<char> bracketStack = new Stack<char>();

		foreach (char c in s)
		{
            if (c == '{' || c == '[' || c == '(')
            {
                bracketStack.Push(c);
            }
            else if (bracketStack.Count > 0)
            {
                bool match = false;

                switch (bracketStack.Pop())
                {
                    case '(': match = ')' == c; break;

                    case '[': match = ']' == c; break;

                    case '{': match = '}' == c; break;
                }

                if (!match)
                {
                    return NO;
                }
            }
            else
			{
                return NO;
			}
		}

        if (bracketStack.Count == 0)
        {
            return YES;
        }
        else
		{
            return NO;
		}
    }

    static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string s = Console.ReadLine();

            string result = isBalanced(s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
