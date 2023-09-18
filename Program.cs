using GetWifiPass;
using System.Diagnostics;

GetWifi getWifi = new();

Stopwatch sw = Stopwatch.StartNew();
string[] wifis = getWifi.GetWiFis();

foreach (var item in wifis)
    Console.WriteLine(item);

string[] wifiInfo = getWifi.GetWifiInfo(wifis[^2]);
Console.WriteLine();

foreach (var item in wifiInfo)
    Console.WriteLine(item);

sw.Stop();
Console.WriteLine();
Console.WriteLine(sw.ElapsedMilliseconds);
Console.ReadKey();