using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuthService.Models;

namespace AuthService.Data.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Password).IsRequired();

            builder.HasData(new User()
            {
                Id = 1,
                Email = "zdravko@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("zdravko")
            },
            new User()
            {
                Id = 2,
                Email = "kurda@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("kurda")
            },
            new User()
            {
                Id = 3,
                Email = "malina@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("malina")
            });
        }       
    }
}
