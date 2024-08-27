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

class Result
{

    /*
     * Complete the 'authEvents' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts 2D_STRING_ARRAY events as parameter.
     */

    public static List<int> authEvents(List<List<string>> events)
    {
        
        List<int> result = new List<int>();
        long hashvalue = 0;
        long compare = 0;
        int cnt = 0;
        long p = 131;
        long m = (long)Math.Pow(10, 9) + 7;
        HashSet<long> hashs = new HashSet<long>();
        
        for(int i = 0; i < events.Count; i++)
        {
            if(events[i][0] == "setPassword")
            {
                hashs.Clear();
                string password = events[i][1];
                hashvalue = 0;

                foreach(char c in password)
                {
                    hashvalue = (hashvalue * p + (long)c) % m;
                }
                hashs.Add(hashvalue);
                
                for (char extra = 'a'; extra <= 'z'; extra++)
                {
                    long tempHash = (hashvalue * p + (long)extra) % m;
                    hashs.Add(tempHash);
                }
                for (char extra = 'A'; extra <= 'Z'; extra++)
                {
                    long tempHash = (hashvalue * p + (long)extra) % m;
                    hashs.Add(tempHash);
                }
                for (char extra = '0'; extra <= '9'; extra++)
                {
                    long tempHash = (hashvalue * p + (long)extra) % m;
                    hashs.Add(tempHash);
                }
            }
            else if(events[i][0] == "authorize")
            {
                compare = long.Parse(events[i][1]);
                if (hashs.Contains(compare))
                {
                    result.Add(1);
                }
                else
                {
                    result.Add(0);
                }
            }    
        }
        
        return result;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int eventsRows = Convert.ToInt32(Console.ReadLine().Trim());
        int eventsColumns = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<string>> events = new List<List<string>>();

        for (int i = 0; i < eventsRows; i++)
        {
            events.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
        }

        List<int> result = Result.authEvents(events);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
