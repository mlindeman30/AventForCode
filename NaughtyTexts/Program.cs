// See https://aka.ms/new-console-template for more information
Console.WriteLine("How may strings are nice and not naughty");

var lines = File.ReadAllLines("./input.txt").ToList();
//var lines = new List<string>() { "^v^v^v^v^v" };

var niceCount = 0;
var naughtyCount = 0;

foreach (var line in lines)
{
    int vowel_count = 0;
    int count_double = 0;

    if (line.Contains("ab") || line.Contains("cd") || line.Contains("pq") || line.Contains("xy"))
    {
        naughtyCount++;
        continue;
    }

    for (int i =0; i < line.Length; i++)
    {
        if (line[i] == 'a' || line[i] == 'e' || line[i] == 'i' || line[i] == 'o' || line[i] == 'u' || line[i] == 'A' || line[i] == 'E' || line[i] == 'I' || line[i] == 'O' || line[i] == 'U')
            vowel_count++;

        if (line[i] == line[i+1])
            count_double++;
    }

    if(vowel_count>3 && count_double >1)
        niceCount++;
    else
        naughtyCount++;

}


Console.WriteLine($"Nice lines: {niceCount}");
Console.WriteLine($"Naughty lines: {naughtyCount}");

Console.ReadLine();
