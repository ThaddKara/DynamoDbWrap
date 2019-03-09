﻿/*******************************************************************************
* Copyright 2009-2018 Amazon.com, Inc. or its affiliates. All Rights Reserved.
* 
* Licensed under the Apache License, Version 2.0 (the "License"). You may
* not use this file except in compliance with the License. A copy of the
* License is located at
* 
* http://aws.amazon.com/apache2.0/
* 
* or in the "license" file accompanying this file. This file is
* distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied. See the License for the specific
* language governing permissions and limitations under the License.
*******************************************************************************/

using System;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using TwitchBoostCredentialsDDB.Services;
using System.Threading.Tasks;

namespace AwsDynamoDBDataModelSample1
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			AmazonDynamoDBClient amazonDynamoDBClient = new AmazonDynamoDBClient();
			IPutItem putItem = new PutItem(amazonDynamoDBClient);

			putItem.AddItem("123123123123123211", "te2312313123123123st123");

			//putItem.AddTest("this is a test", "another text att");
		}

		private static async Task add(IPutItem putItem, string ApiKey, string TwitchName)
		{
			await putItem.AddItem(ApiKey, TwitchName);
		}
	}
}