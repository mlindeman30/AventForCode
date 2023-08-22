// See https://aka.ms/new-console-template for more information

Console.WriteLine("How many lights are on?");

var lines = File.ReadAllLines("./input.txt").ToList();
//lines = new List<string>() { "turn on 0,0 through 999,999", "toggle 0,0 through 999,0", "turn off 499,499 through 500,500" };
var lightMatrix = new List<List<int>>();
var old = false;

for (int i = 0; i < 1000; i++)
{
    var matrix = new List<int>();
    for (int j = 0; j < 1000; j++)
    {
        matrix.Add(0);
    }
    lightMatrix.Add(matrix);
}

foreach (var line in lines)
{
    var split = line.Split(' ');
    var action = 0;//turn on
    if (line.Contains("off"))
        action = 1;
    if (line.Contains("toggle"))
        action = 2;

    var startInd = action == 2 ? 1 : 2;

    var startsplit = split[startInd].Split(',');
    var endsplit = split[startInd + 2].Split(',');

    int startx = int.Parse(startsplit[0]);
    int starty = int.Parse(startsplit[1]);
    int endx = int.Parse(endsplit[0]);
    int endy = int.Parse(endsplit[1]);

    for (int i = startx; i <= endx; i++)
    {
        for (int j = starty; j <= endy; j++)
        {
            if (old)
            {
                switch (action)
                {
                    case 0:
                        lightMatrix[i][j] = 1;
                        break;
                    case 1:
                        lightMatrix[i][j] = 0;
                        break;
                    case 2:
                        lightMatrix[i][j] = lightMatrix[i][j] == 0 ? 1 : 0;
                        break;
                }
            }
            else
            {
                switch (action)
                {
                    case 0:
                        lightMatrix[i][j] += 1;
                        break;
                    case 1:
                        lightMatrix[i][j] -= 1;

                        if (lightMatrix[i][j] < 0)
                            lightMatrix[i][j] = 0;
                        break;
                    case 2:
                        lightMatrix[i][j] += 2;
                        break;
                }
            }
        }
    }
}

var lightsOn = 0;
var lightsOff = 0;
var lumination = 0;
foreach (var light in lightMatrix)
{
    lightsOn += light.Where(x => x >= 1).Count();
    lightsOff += light.Where(x => x < 1).Count();
}
lumination = lightMatrix.Sum(x => x.Sum(y => y));

Console.WriteLine($"Lights On: {lightsOn}");
Console.WriteLine($"Lights Off: {lightsOff}");
Console.WriteLine($"Lumination: {lumination}");

Console.ReadLine();