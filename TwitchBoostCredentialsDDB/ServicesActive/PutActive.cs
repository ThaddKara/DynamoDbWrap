using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace TwitchBoostCredentialsDDB.ServicesActive
{
    public class PutActive : IPutActive
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;

        public PutActive(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task AddComplete(string ApiKey, string ChannelName, int NumBots, int TimeAlive)
		{
			var doc = CompleteRequest(ApiKey, ChannelName, NumBots, TimeAlive);

			await AddCompleteAsync(doc);
		}

		private Document CompleteRequest(string ApiKey, string ChannelName, int NumBots, int TimeAlive)
		{
			Document doc = new Document();
			doc["Api-Key"] = ApiKey;
			doc["ChannelName"] = ChannelName;
			doc["NumBots"] = NumBots.ToString();
			doc["TimeAlive"] = TimeAlive.ToString();

			return doc;
		}

		private async Task AddCompleteAsync(Document doc)
		{
			Table table = Table.LoadTable(amazonDynamoDB, "Active");

			await table.PutItemAsync(doc);
		}
    }
}
