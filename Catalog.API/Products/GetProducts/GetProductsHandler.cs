using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>; // Query object

public record GetProductsResult(IEnumerable<Product> Products); // Result object

internal class GetProductsQueryHandler // Query handler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult> // Handler interface
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    { // Handler implementation

        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize?? 10, cancellationToken); // Query products)
        return new GetProductsResult(products);
    }
}