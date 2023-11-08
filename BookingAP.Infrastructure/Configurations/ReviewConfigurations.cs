using BookingAP.Domain.Appartments;
using BookingAP.Domain.Bookings;
using BookingAP.Domain.Reviews;
using BookingAP.Domain.Reviews.ValueObjects;
using BookingAP.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingAP.Infrastructure.Configurations
{
    internal sealed class ReviewConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(nameof(Review));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasConversion(id => id.Value, value => new ReviewId(value));

            builder.Property(p => p.Comment)
                   .HasConversion(c => c.Value, value => new Comment(value));

            builder.Property(p => p.Rating)
                   .HasConversion(c => c.Value, value => Rating.Create(value).Value);

            builder.Property(p => p.CreatedOnUTC);

            builder.HasOne<Appartment>()
                   .WithMany()
                   .HasForeignKey(f => f.AppartmentId);
            
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(f => f.UserId);

            builder.HasOne<Booking>()
                   .WithMany()
                   .HasForeignKey(f => f.BookingId);
        }
    }
}
