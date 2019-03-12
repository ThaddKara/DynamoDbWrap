using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace TwitchBoostCredentialsDDB.NewFolder
{
    public class GetActive
    {
        IAmazonDynamoDB AmazonDynamoDB;
        
        GetActive(IAmazonDynamoDB AmazonDynamoDB)
        {
            this.AmazonDynamoDB = AmazonDynamoDB;
        }


        public async Task GetActiveRow(int NumBots)
        {
            var request = GetQueryRequest(NumBots);
        }

        private QueryRequest GetQueryRequest(int NumBots)
        {

            Condition condition = new Condition()
            {
                ComparisonOperator = ""
            };

            return new QueryRequest()
            {

            };
        }
    }
}
