using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementWebAPI.Data;
using BookManagementWebAPI.Models.Entities;

namespace BookManagementWebAPI.Endpoints
{
    public class GetBooksEndpoint : EndpointWithoutRequest<List<Book>>
    {
        private readonly AppDbContext _context;

        public GetBooksEndpoint(AppDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Routes("/api/books");
            //Get("/api/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var books = await _context.Books.ToListAsync(ct);
            await SendAsync(books, cancellation: ct);
        }
        public async Task<List<Book>> GetBooksForTestingAsync(CancellationToken ct)
        {
            return await _context.Books.ToListAsync(ct);
        }
    }
}
