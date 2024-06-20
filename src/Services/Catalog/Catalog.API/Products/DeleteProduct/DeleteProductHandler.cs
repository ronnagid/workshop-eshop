
namespace Catalog.API.Productions.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductCommandResult>;
public record DeleteProductCommandResult(bool IsSuccess);

// public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
// {
//     public DeleteProductCommandValidator()
//     {
//         RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id Is Required");
//     }
// }

public class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
    public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
      
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync();

        return new DeleteProductCommandResult(true);
    }
}
