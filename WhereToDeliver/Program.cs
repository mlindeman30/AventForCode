// See https://aka.ms/new-console-template for more information
Console.WriteLine("Where does santa end up?");

var lines = File.ReadAllLines("./input.txt").ToList();
//var lines = new List<string>() { "^v^v^v^v^v" };

var visits = new List<HouseVisit>() { new HouseVisit() { HouseId = 1, Visits =1, X = 0, Y=0 } };
int x = 0, y = 0;
int id = 2;

foreach (var line in lines)
{
    foreach (var c in line)
    {
        if(c == '>')
        {
            x++;
        }
        else if(c == '<')
        {
            x--;
        }
        else if(c == '^')
        {
            y++;
        }
        else if(c == 'v')
        {
            y--;
        }

        var visit = visits.FirstOrDefault(v=> v.X == x && v.Y == y);
        if(visit == null)
        {
            visit = new HouseVisit()
            {
                HouseId = id,
                Visits = 1,
                X = x,
                Y = y,
            };
            id++;
            visits.Add(visit);
        }
        else 
        {
            visit.Visits++;
        }
    }
}

var multicount = visits.Count(x => x.Visits > 1);
Console.WriteLine($"Houses with multiple visits: {multicount}");

Console.ReadLine();

public class HouseVisit
{
    public int HouseId { get; set; }
    public int Visits { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}