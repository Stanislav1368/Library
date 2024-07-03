using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GenreBook> GenreBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
    }
}
