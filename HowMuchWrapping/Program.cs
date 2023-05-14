// See https://aka.ms/new-console-template for more information
Console.WriteLine("How much wrapping do we need?");

var lines = File.ReadAllLines("./input.txt").Select(x => x.ToLower().Split("x").Select(y => int.Parse(y)).ToList()).ToList();
var totalWrap = 0;

//2*l*w + 2*w*h + 2*h*l
//+smallest side for slack

foreach (var line in lines)
{
    var totalgift = 0;
    var l = line[0];
    var w = line[1];
    var h = line[2];
    var lw = (2 * l * w);
    var wh = (2 * w * h);
    var hl = (2 * h * l);
    totalgift = lw + wh + hl;
    var slack = lw < wh ? (lw < hl ? lw : hl) : (wh < hl ? wh : hl);
    totalgift += slack;
    totalWrap += totalgift;
}

Console.WriteLine($"Totalwrapping = {totalWrap}");
Console.ReadLine();