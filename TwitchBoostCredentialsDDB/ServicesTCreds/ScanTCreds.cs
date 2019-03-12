using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace TwitchBoostCredentialsDDB.ServicesTCreds
{
    public class ScanTCreds : IScanTCreds
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;

        public ScanTCreds(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public Search ScanTCredsBots(int numBots)
        {
            Table table = Table.LoadTable(amazonDynamoDB, "TwitchCredentials");

            ScanOperationConfig configrequest = ScanTCredsConfig(numBots);

            Search search = table.Scan(configrequest);
            Console.WriteLine(search.Count);
            return search;
        }

        private ScanOperationConfig ScanTCredsConfig(int numBots)
        {
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("IsActive", ScanOperator.Equal, "false");

            return new ScanOperationConfig()
            {
                Limit = numBots,
                Filter = scanFilter
            };
        }
    }
}
