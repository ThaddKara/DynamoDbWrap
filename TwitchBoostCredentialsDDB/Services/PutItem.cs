using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace TwitchBoostCredentialsDDB.Services
{
	class PutItem : IPutItem
	{
		private readonly IAmazonDynamoDB amazonDynamoDB;

		public PutItem(AmazonDynamoDBClient amazonDynamoDB)
		{
			this.amazonDynamoDB = amazonDynamoDB;
		}

		public async Task AddItem(string ApiKey, string TwitchName)
		{

			var request = RequestBuilder(ApiKey, TwitchName);

			await PutAsync(request);
		}

		public async Task AddTest(string ApiKey, string Att)
		{
			Table table = Table.LoadTable(amazonDynamoDB, "TwitchCredentials");

			await TestAsync(table, ApiKey, Att);
		}

		private async Task TestAsync(Table table, string ApiKey, string Att)
		{
			var doc = new Document();
			doc["Api-Key"] = ApiKey;
			doc["TwitchName"] = Att;

			await table.PutItemAsync(doc);
		}

		private PutItemRequest RequestBuilder(string ApiKey, string TwitchName)
		{
			var attributes = new Dictionary<string, AttributeValue>
			{
				{"Api-Key", new AttributeValue{ S = ApiKey } },
				{"TwitchName", new AttributeValue{ S = TwitchName } }
			};
			return new PutItemRequest
			{
				TableName = "TwitchCredentials",
				Item = attributes
			};
		}

		private async Task PutAsync(PutItemRequest request)
		{
			await amazonDynamoDB.PutItemAsync(request);
		}
	}
}
