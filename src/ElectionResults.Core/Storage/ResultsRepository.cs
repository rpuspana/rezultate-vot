using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using ElectionResults.Core.Models;
using Microsoft.Extensions.Options;

namespace ElectionResults.Core.Storage
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly IAmazonDynamoDB _dynamoDb;
        private AppConfig _config;

        public ResultsRepository(IOptions<AppConfig> config, IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
            _config = config.Value;
        }

        public async Task InsertResults(ElectionStatistics electionStatistics)
        {
            try
            {
                var tableExists = await TableIsReady();
                if (!tableExists)
                    await CreateTable();

                Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>();
                item["id"] = new AttributeValue { S = electionStatistics.Id };
                item["csvType"] = new AttributeValue { S = electionStatistics.Type };
                item["csvLocation"] = new AttributeValue { S = electionStatistics.Location };
                item["statisticsJson"] = new AttributeValue { S = electionStatistics.StatisticsJson };
                item["csvTimestamp"] = new AttributeValue { N = electionStatistics.FileTimestamp.ToString() };
                List<WriteRequest> items = new List<WriteRequest>();
                items.Add(new WriteRequest
                {
                    PutRequest = new PutRequest { Item = item }
                });
                Dictionary<string, List<WriteRequest>> requestItems = new Dictionary<string, List<WriteRequest>>();
                requestItems[_config.TableName] = items;

                await _dynamoDb.BatchWriteItemAsync(requestItems);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task<bool> TableIsReady()
        {
            try
            {
                var res = await _dynamoDb.DescribeTableAsync(new DescribeTableRequest
                {
                    TableName = _config.TableName
                });
                return res.Table.TableStatus == "ACTIVE";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<ElectionStatistics> GetLatestResults(string location, string type)
        {
            var queryRequest = new QueryRequest(_config.TableName);
            queryRequest.ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {":csvType", new AttributeValue(type)},
                {":csvLocation", new AttributeValue(location) }
            };
            queryRequest.IndexName = "latest-result";
            queryRequest.KeyConditionExpression = "csvType = :csvType and csvLocation = :csvLocation";
            var queryResponse = await _dynamoDb.QueryAsync(queryRequest);

            var results = GetResults(queryResponse.Items);
            var latest = results.OrderByDescending(r => r.FileTimestamp).FirstOrDefault();
            Console.WriteLine($"Latest for {type} and {location} is {latest.FileTimestamp}");
            return latest;
        }

        private List<ElectionStatistics> GetResults(List<Dictionary<string, AttributeValue>> foundItems)
        {
            return foundItems.Select(item => new ElectionStatistics
            {
                Id = item["id"].S,
                FileTimestamp = Convert.ToInt64(item["csvTimestamp"].N),
                StatisticsJson = item["statisticsJson"].S,
                Location = item["csvLocation"].S,
                Type = item["csvType"].S

            }).ToList();
        }

        private async Task CreateTable()
        {
            Console.WriteLine("Creating Table");

            try
            {
                var request = new CreateTableRequest
                {
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "id",
                            AttributeType = "S"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "csvType",
                            AttributeType = "S"
                        },
                        new AttributeDefinition
                        {
                            AttributeName = "csvLocation",
                            AttributeType = "S"
                        }
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "id",
                            KeyType = KeyType.HASH
                        },
                        new KeySchemaElement
                        {
                            AttributeName = "csvType",
                            KeyType = KeyType.RANGE
                        }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 5,
                        WriteCapacityUnits = 5
                    },
                    TableName = _config.TableName,
                    GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
                    {
                        new GlobalSecondaryIndex
                        {
                            IndexName = "latest-result",
                            KeySchema = new List<KeySchemaElement>
                            {
                                new KeySchemaElement("csvLocation", KeyType.HASH),
                                new KeySchemaElement("csvType", KeyType.RANGE)
                            },
                            Projection = new Projection{ProjectionType = ProjectionType.ALL},
                            ProvisionedThroughput = new ProvisionedThroughput(5, 5)
                        }
                    }
                };

                var response = await _dynamoDb.CreateTableAsync(request);
                await WaitUntilTableReady(_config.TableName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task WaitUntilTableReady(string tableName)
        {
            string status = null;

            do
            {
                await Task.Delay(2000);
                try
                {
                    var res = _dynamoDb.DescribeTableAsync(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    status = res.Result.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {

                }

            } while (status != "ACTIVE");
        }
    }
}