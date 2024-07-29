using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementWebAPI.Models.Entities;
using BookManagementWebAPI.Data;
using Microsoft.Extensions.Logging;

namespace BookManagementWebAPI.Endpoints
{
    public class GetBookByIdRequest
    {
        public Guid Id { get; set; }
    } 

    public class GetBookByIdResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
    }

    public class GetBookByIdEndpoint : Endpoint<GetBookByIdRequest, GetBookByIdResponse>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GetBookByIdEndpoint> _logger;

        public GetBookByIdEndpoint(AppDbContext context, ILogger<GetBookByIdEndpoint> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override void Configure()
        {
            Verbs(Http.GET);
            Get("/api/books/{id:guid}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
        {
            //var id = Route<Guid>("id");

            _logger.LogInformation("Attempting to retrieve book with ID: {Id}", req.Id);

            var book = await _context.Books.FindAsync(new object[] { req.Id }, ct);

            if (book is null)
            {
                _logger.LogWarning("Book with ID: {Id} not found", req.Id);
                await SendNotFoundAsync(cancellation: ct);
                return;
            }

            var response = new GetBookByIdResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                ISBN = book.ISBN
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}
