using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace TwitchBoostCredentialsDDB.ServicesTCreds
{
    public interface IScanTCreds
    {
        Task ScanTCredsBots(string channelName, int numBots, int timeMin);
    }
}
