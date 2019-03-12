using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace TwitchBoostCredentialsDDB.ServicesActive
{
    public class ScanActive : IScanActive
    {
        private readonly AmazonDynamoDBClient amazonDynamoDBClient = new AmazonDynamoDBClient();
        private readonly IAmazonDynamoDB amazonDynamoDB;

        public ScanActive(IAmazonDynamoDB amazonDynamoDB)
        {
            this.amazonDynamoDB = amazonDynamoDB;
        }

        public async Task ScanActiveDoc(bool IsActive)
        {
            var request = RequestBuilder(IsActive);

            await ScanActiveDocAsync(request);
        }

        //results for given input bool
        private ScanOperationConfig RequestBuilder(bool IsActive)
        {
            ScanFilter filter = new ScanFilter();
            filter.AddCondition("IsActive", ScanOperator.Equal, IsActive.ToString());

            return new ScanOperationConfig()
            {
                Filter = filter
            };
        }

        //delete matching ApiKey if active is false
        private async Task ScanActiveDocAsync(ScanOperationConfig scanOperationConfig)
        {
            Table table = Table.LoadTable(amazonDynamoDB, "Active");
            List<Document> results = new List<Document>();
            IDeleteActive deleteActive = new DeleteActive(amazonDynamoDBClient);

            Search search = table.Scan(scanOperationConfig);

            do
            {
                results = search.GetNextSet();

                foreach (Document doc in results)
                {
                    await deleteActive.DeleteActiveDoc(doc["Api-Key"]);
                }
            } while (!search.IsDone);
        }
    }
}
