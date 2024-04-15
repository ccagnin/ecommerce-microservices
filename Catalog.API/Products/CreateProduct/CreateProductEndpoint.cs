// Purpose: Implementation of the Create Product
// endpoint in the Catalog.API microservice.
// This endpoint is responsible for creating a new product in the catalog.
// The endpoint receives a CreateProductRequest object,
// which is mapped to a CreateProductCommand object.
// The command is then sent to the CreateProductCommandHandler,
// which creates a new Product object and saves it to the database.
// The handler returns a CreateProductResult object with the ID of the newly created product.
// The result is then mapped to a CreateProductResponse object and returned to the client
// with a 201 Created status code.

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