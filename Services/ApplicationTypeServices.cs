using System.Net.NetworkInformation;
using yatracub.Services.Interface;

namespace yatracub.Services
{
    public class ApplicationTypeServices : IApplicationTypeServices
    {
        public List<string> GetSystemIp()
        {

            List<string> ip = new List<string>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ips in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ips.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        var ipsf = ips.Address.ToString().Trim();
                        if (ips.DuplicateAddressDetectionState.ToString() == "Preferred" && ips.IsDnsEligible == true)
                        {
                            ip.Add(ips.Address.ToString());

                    }
                }
                }
            }
            return ip;
        }
    }
}
