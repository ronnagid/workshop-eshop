
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? Page = 1, int? PageSize = 12) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products, int TotalData = 0);
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToPagedListAsync(request.Page ?? 1,request.PageSize ?? 12);
        var TotalData = await session.Query<Product>().CountAsync();

        return new GetProductsResult(products,TotalData);
    }
}
