using Amazon.DynamoDBv2.DataModel;

namespace NoSql_WebApi.Models.Domain
{

    [DynamoDBTable("items")]

    public class ItemModel
    {
        [DynamoDBHashKey("id")]
        public int Id { get; set; }
        [DynamoDBProperty("type")]
        public string Type { get; set; }
        [DynamoDBProperty("name")]
        public string Name { get; set; }
        [DynamoDBProperty("description")]
        public string Description { get; set; }
    }
}
