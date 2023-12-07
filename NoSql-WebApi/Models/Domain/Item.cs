using Amazon.DynamoDBv2.DataModel;

namespace NoSql_WebApi.Models.Domain
{

    [DynamoDBTable("Items")]

    public class Item
    {
        [DynamoDBHashKey("Type")]
        public string Type { get; set; }
        [DynamoDBRangeKey("Id")]
        public string Id { get; set; }
        [DynamoDBProperty("Name")]
        public string Name { get; set; }
        [DynamoDBProperty("Description")]
        public string Description { get; set; }
    }
}
