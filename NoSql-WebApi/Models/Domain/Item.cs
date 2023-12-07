using Amazon.DynamoDBv2.DataModel;

namespace NoSql_WebApi.Models.Domain
{

    [DynamoDBTable("Items")]

    public class Item
    {
        [DynamoDBHashKey("Id")]
        public int Id { get; set; }
        [DynamoDBRangeKey("Type")]
        public string Type { get; set; }
        [DynamoDBProperty("Name")]
        public string Name { get; set; }
        [DynamoDBProperty("Description")]
        public string Description { get; set; }
    }
}
