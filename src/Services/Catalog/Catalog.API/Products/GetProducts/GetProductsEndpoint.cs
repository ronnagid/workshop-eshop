
namespace Catalog.API.Products.GetProducts;
// //public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsRequest(int? Page = 1, int? PageSize = 12) : IQuery<GetProductsResult>;
public record GetProductsResponse(IEnumerable<Product> Products, int TotalData);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters]GetProductsRequest request,ISender sender) =>
        {
            // Add Entity to Database
            //throw new NotImplementedException();
            var query = request.Adapt<GetProductsQuery> ();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);

        }).WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");;
    }
}
