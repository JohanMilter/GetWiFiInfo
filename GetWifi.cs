using System.Diagnostics;

namespace GetWifiPass;

public class GetWifi
{
    private static readonly Process process = new()
    {
        StartInfo = new ProcessStartInfo()
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
        }
    };
    internal string[] GetWiFis()
    {
        process.Start();
        process.StandardInput.WriteLine("netsh wlan show profile");
        process.StandardInput.Flush();
        process.StandardInput.Close();
        return process.StandardOutput.ReadToEnd().Split('\n')
            .Where(x => x.StartsWith("    All User Profile     : "))
            .Select(x => x.Replace("    All User Profile     : ", null)).ToArray();
    }
    internal string[] GetWifiInfo(string wifi)
    {
        string[] info = new string[5];
        if (wifi != null)
        {
            process.Start();
            process.StandardInput.WriteLine("netsh wlan show profile " + wifi + " key=clear");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            string[] lines = process.StandardOutput.ReadToEnd().Split('\n');
            foreach (string item in lines)
            {
                if (item.StartsWith("    Key Content            : "))
                    info[4] = item.Replace("    Key Content            : ", "Key Content (Adgangskode): ");
                else if (item.StartsWith("    Network type           : "))
                    info[3] = item.Replace("    Network type           : ", "Network type: ");
                else if (item.StartsWith("        Connection mode    : "))
                    info[2] = item.Replace("        Connection mode    : ", "Connection mode: ");
                else if (item.StartsWith("    Type                   : "))
                    info[0] = item.Replace("    Type                   : ", "Type: ");
                else if (item.StartsWith("    Name                   : "))
                    info[1] = item.Replace("    Name                   : ", "Name: ");
            }
            process.WaitForExit();
        }
        return info;
    }
}
