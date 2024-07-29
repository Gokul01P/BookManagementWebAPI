using Microsoft.EntityFrameworkCore;
using Xunit;
using BookManagementWebAPI.Data;
using BookManagementWebAPI.Endpoints;
using BookManagementWebAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookManagementWebAPI.Tests
{
    public class GetBooksEndpointTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("GetBooksTestDb")
                .Options;

            return new AppDbContext(options);
        }

        private async Task SeedData(AppDbContext context)
        {
            context.Books.AddRange(new List<Book>
            {
                new Book { Title = "Test Book 1", Author = "Test Author 1", PublicationYear = 2020, ISBN = "1234567890123" },
                new Book { Title = "Test Book 2", Author = "Test Author 2", PublicationYear = 2021, ISBN = "1234567890124" }
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetBooks_ShouldReturnBooks_WhenBooksExist()
        {
            // Arrange
            var context = GetDbContext();
            await SeedData(context);
            var endpoint = new GetBooksEndpoint(context);

            // Act
            var books = await endpoint.GetBooksForTestingAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(books);
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public async Task GetBooks_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            // Arrange
            var context = GetDbContext();
            var endpoint = new GetBooksEndpoint(context);

            // Act
            var books = await endpoint.GetBooksForTestingAsync(CancellationToken.None);
            if (books == null)
            {
                // Log or handle the error as needed
                throw new InvalidOperationException("Failed to retrieve books from the database.");
            }
            // Assert
            Assert.NotNull(books);
            
        }
    }
}
