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
        public void AddActiveDoc(string ApiKey,
    }
}
