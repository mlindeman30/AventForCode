// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("How may strings are nice and not naughty");

var lines = File.ReadAllLines("./input.txt").ToList();
//var lines = new List<string>() { "ugknbfddgicrmopn" };

var niceCount = 0;
var naughtyCount = 0;
var old = false;

foreach (var line in lines)
{
    int vowel_count = 0;
    int count_double = 0;

    if (old)
    {
        if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
        {
            naughtyCount++;
            continue;
        }

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == 'a' || line[i] == 'e' || line[i] == 'i' || line[i] == 'o' || line[i] == 'u' || line[i] == 'A' || line[i] == 'E' || line[i] == 'I' || line[i] == 'O' || line[i] == 'U')
                vowel_count++;

            if (line.Length > i + 1 && line[i] == line[i + 1])
                count_double++;
        }

        if (vowel_count > 2 && count_double > 0)
            niceCount++;
        else
            naughtyCount++; 
    }
    else
    {
        var matchesCountRepeats = 0;
        var matchesCountInTheMiddle = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (i == line.Length - 1)
                continue;
            
            var matchString = $"({line[i]}{line[i + 1]})";//match multiple occurences of repeats
            var matches = Regex.Matches(line, matchString);

            if (matches.Count >= 2)
            matchesCountRepeats++;
            matchString = $"({line[i]}.{line[i]})";
            var match = Regex.Match(line, matchString,RegexOptions.NonBacktracking);
            if (match.Captures.Count >= 1)
                matchesCountInTheMiddle++;
        }

        if (matchesCountRepeats >= 1 && matchesCountInTheMiddle >= 1)
            niceCount++;
        else
            naughtyCount++;
    }

}


Console.WriteLine($"Nice lines: {niceCount}");
Console.WriteLine($"Naughty lines: {naughtyCount}");

Console.ReadLine();
