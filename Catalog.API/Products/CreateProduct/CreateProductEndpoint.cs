using Carter;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(
    string Name, 
    List<string> Category, 
    string Description, 
    string ImageFile, 
    decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>(); // Map request to command

                var result = await sender.Send(command); // Send command to handler

                var response = result.Adapt<CreateProductResponse>(); // Map result to response

                return Results.Created($"/products/{response.Id}", response); // Return response
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product"); // Add metadata
    }
}