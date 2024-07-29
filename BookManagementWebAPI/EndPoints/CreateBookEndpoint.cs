using BookManagementWebAPI.Data;
using BookManagementWebAPI.Models.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Endpoints
{
    public class CreateBookRequest
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int PublicationYear { get; set; }
        public required string ISBN { get; set; }
    }

    public class CreateBookEndpoint : Endpoint<CreateBookRequest>
    {
        private readonly AppDbContext _context;

        public CreateBookEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/api/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Title) || string.IsNullOrWhiteSpace(req.Author) || string.IsNullOrWhiteSpace(req.ISBN))
            {
                ThrowError("Invalid request: Title, Author, and ISBN are required fields.");
            }

            var book = new Book
            {
                Title = req.Title,
                Author = req.Author,
                PublicationYear = req.PublicationYear,
                ISBN = req.ISBN
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);

            await SendAsync(book, cancellation: ct);
        }
    }
}
