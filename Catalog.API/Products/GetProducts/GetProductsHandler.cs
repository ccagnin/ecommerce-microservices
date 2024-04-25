namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>; // Query object

public record GetProductsResult(IEnumerable<Product> Products); // Result object

internal class GetProductsQueryHandler // Query handler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult> // Handler interface
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    { // Handler implementation

        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}