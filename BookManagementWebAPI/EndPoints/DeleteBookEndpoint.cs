using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementWebAPI.Models.Entities;
using BookManagementWebAPI.Data;

namespace BookManagementWebAPI.Endpoints
{
    public class DeleteBookRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteBookEndpoint : Endpoint<DeleteBookRequest>
    {
        private readonly AppDbContext _context;

        public DeleteBookEndpoint(AppDbContext context)
        {
            this._context = context;
        }

        public override void Configure()
        {
            Delete("/api/books/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
        {

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == req.Id,ct);

            if (book is null)
            {
                await SendNotFoundAsync(cancellation: ct);
                return;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(ct);

            await SendOkAsync(cancellation: ct);
        }
    }
}
