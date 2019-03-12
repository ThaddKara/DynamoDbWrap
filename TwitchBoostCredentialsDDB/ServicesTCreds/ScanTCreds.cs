using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using TwitchBoostCredentialsDDB.ServicesActive;
using TwitchBoostCredentialsDDB.Services;

namespace TwitchBoostCredentialsDDB.ServicesTCreds
{
    public class ScanTCreds : IScanTCreds
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;
		AmazonDynamoDBClient amazonDynamoDBClient = new AmazonDynamoDBClient();

        public ScanTCreds(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task ScanTCredsBots(string channelName, int numBots, int timeMin)
        {
			IPutActive putActive = new PutActive(amazonDynamoDBClient);
			IPutItem putItem = new PutItem(amazonDynamoDBClient);
			
            Table table = Table.LoadTable(amazonDynamoDB, "TwitchCredentials");
			List<Document> result = new List<Document>();

            ScanOperationConfig configrequest = ScanTCredsConfig(numBots);

            Search search = table.Scan(configrequest);
			Thread.Sleep(1000);

			//Console.WriteLine(search.Matches.IndexOf(search.m));
			//Console.WriteLine(search.Matches.ToList().GetType());
			//Console.WriteLine(search.Matches[0].GetAttributeNames());
			//Console.WriteLine(search.Matches[0]["Api-Key"].AsString());

			do
			{
				result = search.GetNextSet();

				foreach (var item in result)
				{
					Console.WriteLine(item["Api-Key"]);
					putActive.AddComplete(item["Api-Key"], channelName, numBots, timeMin);

					item["IsActive"] = "true";
					putItem.AddComplete(item["Api-Key"], item["TwitchName"], item["IsActive"]);
				}
			} while (!search.IsDone);

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
