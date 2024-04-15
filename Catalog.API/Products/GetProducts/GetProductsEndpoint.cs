namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products); // Response object
public class GetProductsEndpoint : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery()); // Send query to handler

                var response = result.Adapt<GetProductsResponse>(); // Map result to response

                return Results.Ok(response); // Return response
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products"); // Add metadata
    }
}