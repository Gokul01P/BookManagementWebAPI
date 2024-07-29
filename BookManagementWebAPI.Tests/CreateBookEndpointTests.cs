using Microsoft.EntityFrameworkCore;
using Xunit;
using BookManagementWebAPI.Data;
using BookManagementAPI.Endpoints;
using BookManagementWebAPI.Models.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using FastEndpoints;

namespace BookManagementWebAPI.Tests
{
    public class CreateBookEndpointTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("CreateBookTestDb")
                .Options;

            return new AppDbContext(options);
        }

        private HttpContext CreateHttpContext()
        {
            var context = new DefaultHttpContext();
            return context;
        }

        private void SetHttpContext(object endpoint, HttpContext httpContext)
        {
            var httpContextField = typeof(BaseEndpoint).GetProperty("HttpContext", BindingFlags.Instance | BindingFlags.NonPublic);
            if (httpContextField != null)
            {
                httpContextField.SetValue(endpoint, httpContext);
            }
        }

        [Fact]
        public async Task CreateBook_ShouldReturnCreatedBook_WhenRequestIsValid()
        {
            // Arrange
            var context = GetDbContext();
            var endpoint = new CreateBookEndpoint(context);
            SetHttpContext(endpoint, CreateHttpContext());

            var createBookRequest = new CreateBookRequest
            {
                Title = "New Book",
                Author = "New Author",
                PublicationYear = 2023,
                ISBN = "1234567890123"
            };

            // Act
            await endpoint.HandleAsync(createBookRequest, CancellationToken.None);

            // Assert
            var createdBook = await context.Books
                .FirstOrDefaultAsync(b => b.ISBN == createBookRequest.ISBN);

            Assert.NotNull(createdBook);
            Assert.Equal(createBookRequest.Title, createdBook.Title);
            Assert.Equal(createBookRequest.Author, createdBook.Author);
            Assert.Equal(createBookRequest.PublicationYear, createdBook.PublicationYear);
            Assert.Equal(createBookRequest.ISBN, createdBook.ISBN);
        }

        [Fact]
        public async Task CreateBook_ShouldHandleInvalidRequest()
        {
            // Arrange
            var context = GetDbContext();
            var endpoint = new CreateBookEndpoint(context);
            SetHttpContext(endpoint, CreateHttpContext());

            var invalidCreateBookRequest = new CreateBookRequest
            {
                // Missing Title, Author, and ISBN to simulate invalid request
                Title = string.Empty,
                Author = string.Empty,
                PublicationYear = 0,
                ISBN = string.Empty
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationFailureException>(() =>
                endpoint.HandleAsync(invalidCreateBookRequest, CancellationToken.None));

            Assert.Contains("Invalid request: Title, Author, and ISBN are required fields.", exception.Message);
        }
    }
}
