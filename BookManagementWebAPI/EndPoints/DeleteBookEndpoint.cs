using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementWebAPI.Models.Entities;
using BookManagementWebAPI.Data;
using Microsoft.Extensions.Logging;

namespace BookManagementWebAPI.Endpoints
{
    public class DeleteBookRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteBookEndpoint : Endpoint<DeleteBookRequest>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DeleteBookEndpoint> _logger;

        public DeleteBookEndpoint(AppDbContext context, ILogger<DeleteBookEndpoint> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public override void Configure()
        {
            Delete("/api/books/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
        {
            _logger.LogInformation("Attempting to delete book with ID: {Id}", req.Id);

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == req.Id,ct);

            if (book is null)
            {
                _logger.LogWarning("Book with ID: {Id} not found", req.Id);
                await SendNotFoundAsync(cancellation: ct);
                return;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(ct);

            _logger.LogInformation("Book with ID: {Id} deleted successfully", req.Id);
            await SendOkAsync(cancellation: ct);
        }
    }
}
