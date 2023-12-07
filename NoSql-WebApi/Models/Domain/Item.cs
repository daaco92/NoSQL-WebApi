using Amazon.DynamoDBv2.DataModel;

namespace NoSql_WebApi.Models.Domain
{

    [DynamoDBTable("Items")]

    public class Items
    {
        [DynamoDBHashKey("id")]
        public int Id { get; set; }
        [DynamoDBRangeKey("type")]
        public string Type { get; set; }
        [DynamoDBProperty("name")]
        public string Name { get; set; }
        [DynamoDBProperty("description")]
        public string Description { get; set; }
    }
}
