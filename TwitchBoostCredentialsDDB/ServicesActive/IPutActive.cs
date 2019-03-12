using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBoostCredentialsDDB.ServicesActive
{
    public interface IPutActive
    {
		Task AddComplete(string ApiKey, string ChannelName, int NumBots, int TimeAlive);
    }
}
