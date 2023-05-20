// See https://aka.ms/new-console-template for more information
Console.WriteLine("Where does santa end up?");

var lines = File.ReadAllLines("./input.txt").ToList();
//var lines = new List<string>() { "^v^v^v^v^v" };

var visits = new List<HouseVisit>() { new HouseVisit() { HouseId = 1, Visits =2, X = 0, Y=0 } };
int x1 = 0, y1 = 0;
int x2 = 0, y2 = 0;
int id = 2;
var logging = new List<string>();

foreach (var line in lines)
{
    int index = 0;
    foreach (var c in line)
    {
        if(c == '>')
        {
            if (index % 2 == 0)
                x1++;
            else
                x2++;
        }
        else if(c == '<')
        {
            if (index % 2 == 0)
                x1--;
            else
                x2--;
        }
        else if(c == '^')
        {
            if (index % 2 == 0)
                y1++;
            else
                y2++;
        }
        else if(c == 'v')
        {
            if (index % 2 == 0)
                y1--;
            else
                y2--;
        }
        var s = $"Cord {c}: {x1}:{y1}";
        logging.Add(s);
        var visit = visits.FirstOrDefault(v=> (index % 2 == 0 && v.X == x1 && v.Y == y1) || (index % 2 != 0 && v.X == x2 && v.Y == y2));
        if(visit == null)
        {
            s = "Nieuw huis";
            logging.Add(s);
            visit = new HouseVisit()
            {
                HouseId = id,
                Visits = 1,
                X = index % 2 == 0 ? x1 : x2,
                Y = index % 2 == 0 ? y1 : y2,
            };
            id++;
            visits.Add(visit);
        }
        else
        {
            s = $"Bezocht huis: {x1}:{y1}";
            logging.Add(s);
            visit.Visits++;
        }
        index++;
    }
}

var multicount = visits.Count(x => x.Visits >= 1);
Console.WriteLine($"Houses with multiple visits: {multicount}");

Console.ReadLine();

public class HouseVisit
{
    public int HouseId { get; set; }
    public int Visits { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}