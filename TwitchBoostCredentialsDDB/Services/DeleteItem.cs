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
	public class DeleteItem : IDeleteItem
	{
		private readonly IAmazonDynamoDB AmazonDynamoDB;
		private readonly Table table;

		public DeleteItem(IAmazonDynamoDB AmazonDynamoDB)
		{
			this.AmazonDynamoDB = AmazonDynamoDB;
			try
			{
				this.table = Table.LoadTable(AmazonDynamoDB, "TwitchCredentials");
				Console.WriteLine("Succesful load");
			}
			catch
			{
				Console.WriteLine("Could not load");
			}
		}

		// delete by doc primary key
		public async Task DeleteDoc(string ApiKey)
		{
			try
			{
				var request = BuildDoc(ApiKey);
				await DeleteDocAsync(request);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

		private Document BuildDoc(string ApiKey)
		{
			var doc = new Document();
			doc["Api-Key"] = ApiKey;
			return doc;
		}

		private async Task DeleteDocAsync(Document doc)
		{
			await table.DeleteItemAsync(doc);
		}

		// delete by string primary key
		public async Task Delete(string ApiKey, string Name)
		{
			try
			{
				var request = RequestBuilder(ApiKey, Name);
				await DeleteAsync(request);
			}
			catch
			{
				throw new NotImplementedException();
			}
		}

		private DeleteItemRequest RequestBuilder(string ApiKey, string Name)
		{
			var item = new Dictionary<string, AttributeValue>
			{
				{"Api-Key", new AttributeValue{ S = ApiKey }}
				//{"TwitchName", new AttributeValue{ S = Name } }
			};

			return new DeleteItemRequest
			{
				TableName = "TwitchCredentials",
				Key = item
			};
		}

		private async Task DeleteAsync(DeleteItemRequest deleteRequest)
		{
			try
			{
				DeleteItemResponse response;
				response = await AmazonDynamoDB.DeleteItemAsync(deleteRequest);

				Console.WriteLine(response.ResponseMetadata.Metadata.ToString());
			}
			catch
			{
				throw new NotImplementedException();
			}
		}
	}
}
