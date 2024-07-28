using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementWebAPI.Data;

namespace BookManagementWebAPI.Endpoints
{
    public class UpdateBookRequest
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int PublicationYear { get; set; }
        public required string ISBN { get; set; }
    }
    public class UpdateBookEndpoint : Endpoint<UpdateBookRequest>
    {
        private readonly AppDbContext _context;

        public UpdateBookEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Put("/api/books/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateBookRequest req, CancellationToken ct)
        {
            var book = await _context.Books.FindAsync(req.Id);

            if (book == null)
            {
                await SendNotFoundAsync(cancellation: ct);
                return;
            }

            book.Title = req.Title;
            book.Author = req.Author;
            book.PublicationYear = req.PublicationYear;
            book.ISBN = req.ISBN;

            await _context.SaveChangesAsync(ct);

            await SendOkAsync(cancellation: ct);
        }
    }

}
