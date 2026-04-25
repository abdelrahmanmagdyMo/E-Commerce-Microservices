using CatalogApplication.Responses;
using CatalogCore.Entities;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogApplication.Commands
{
    public class CreateProductCommand : IRequest<ProductResponseDto>
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }
        public ProductBrand Brand { get; set; }
        public ProductType Type { get; set; }
    }
}
