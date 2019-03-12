using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;

namespace TwitchBoostCredentialsDDB.ServicesActive
{
    public class DeleteActive : IDeleteActive
    {
        private readonly IAmazonDynamoDB amazonDynamoDB;

        public DeleteActive(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task DeleteActiveDoc(string ApiKey)
        {
            var request = BuildDoc(ApiKey);

            await DeleteActiveDocAsync(request);
        }

        private Document BuildDoc(string ApiKey)
        {
            Document doc = new Document();
            doc["Api-Key"] = ApiKey;

            return doc;
        }

        private async Task DeleteActiveDocAsync(Document doc)
        {
            Table table = Table.LoadTable(amazonDynamoDB, "Active");

            await table.DeleteItemAsync(doc);
        }
    }
}
