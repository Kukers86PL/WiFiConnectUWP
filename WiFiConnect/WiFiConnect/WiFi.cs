using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Security.Credentials;

public class WiFi
{
    static public async void Connect(String SSID, String Pass)
    {
        var access = await WiFiAdapter.RequestAccessAsync();
        if (access == WiFiAccessStatus.Allowed)
        {
            var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            if (result.Count >= 1)
            {
                WiFiAdapter firstAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);

                foreach (var availableNetwork in await WiFi.GetConfiguredNetworks())
                {
                    if (availableNetwork.Ssid == SSID)
                    {
                        await firstAdapter.ConnectAsync(availableNetwork, WiFiReconnectionKind.Automatic, new PasswordCredential("", "", Pass));
                        break;
                    }
                }                
            }
        }
    }

    static public async Task<IReadOnlyList<WiFiAvailableNetwork>> GetConfiguredNetworks()
    {
        var access = await WiFiAdapter.RequestAccessAsync();
        if (access == WiFiAccessStatus.Allowed)
        {
            var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            if (result.Count >= 1)
            {
                WiFiAdapter firstAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                await firstAdapter.ScanAsync();
                return firstAdapter.NetworkReport.AvailableNetworks;
            }
        }
        return new List<WiFiAvailableNetwork>();
    }
}