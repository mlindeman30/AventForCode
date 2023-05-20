// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("What is de hex for adventcoind?");

var baseString = "ckczppom";
var doSix = true;
var found = false;
var id = 0;

using (MD5 md5Hash = MD5.Create())
{
    do
    {
        var byteResult = md5Hash.ComputeHash(Encoding.UTF8.GetBytes($"{baseString}{id}"));
        StringBuilder hex = new StringBuilder(byteResult.Length * 2);

        foreach (byte bi in byteResult)
            hex.AppendFormat("{0:x2}", bi);

        var stringResult = hex.ToString();

        if ((!doSix && stringResult.StartsWith("00000")) || (doSix && stringResult.StartsWith("000000")))
            found = true;
        else
            id++;
    }
    while (!found);
}

Console.WriteLine($"The smallest number is: {id}");
Console.ReadLine();
