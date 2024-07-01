using Microsoft.EntityFrameworkCore;
using library_manager_avalonia.Models;

namespace library_manager_avalonia.Database
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<RentalBook> RentalBooks { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title)
                .IsUnique();

            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<RentalBook>()
                .HasKey(rb => new { rb.RentalId, rb.BookId });
            modelBuilder.Entity<RentalBook>()
                .HasOne(rb => rb.Rental)
                .WithMany(r => r.RentalBooks)
                .HasForeignKey(rb => rb.RentalId);
            modelBuilder.Entity<RentalBook>()
                .HasOne(rb => rb.Book)
                .WithMany(b => b.RentalBooks)
                .HasForeignKey(rb => rb.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
