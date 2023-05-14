// See https://aka.ms/new-console-template for more information
Console.WriteLine("How much wrapping do we need?");

var lines = File.ReadAllLines("./input.txt").Select(x => x.ToLower().Split("x").Select(y => int.Parse(y)).ToList()).ToList();
//var lines = new List<List<int>>() { new List<int>() { 2, 3, 4 }, new List<int>() { 1, 1, 10 } };
var totalWrap = 0;
var totalRibbon = 0;

//2*l*w + 2*w*h + 2*h*l
//+smallest side for slack

foreach (var line in lines)
{
    var ribbonGift = 0;
    var totalgift = 0;

    var l = line[0];
    var w = line[1];
    var h = line[2];
    var lw = (l * w);
    var wh = (w * h);
    var hl = (h * l);
    totalgift = 2 * lw + 2 * wh + 2 * hl;
    var slack = lw < wh ? (lw < hl ? lw : hl) : (wh < hl ? wh : hl);
    totalgift += slack;
    totalWrap += totalgift;

    var smallestSide = line.Min(x => x);
    var mediumSide = 0;
    if (line.Where(x => x == smallestSide).Count() < 2)
        mediumSide = line.Where(x => x != smallestSide).Min(x => x);
    else
        mediumSide = smallestSide;
    var cube = line.Aggregate((x, y) => x * y);
    ribbonGift = (smallestSide * 2) + (mediumSide * 2) + cube;
    totalRibbon += ribbonGift;
    }

Console.WriteLine($"Totalwrapping = {totalWrap}");
Console.WriteLine($"Totalribbon = {totalRibbon}");
Console.ReadLine();