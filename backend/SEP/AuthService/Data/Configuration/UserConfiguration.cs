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
            builder.Property(x=> x.Type).IsRequired();

            builder.HasData(new User()
            {
                Id = 1,
                Email = "zdravko@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("zdravko"),
                Type = Enums.EUserType.AGENCY_REGISTRATION_EMPLOYEE
            },
            new User()
            {
                Id = 2,
                Email = "kurda@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("kurda"),
                Type = Enums.EUserType.AGENCY_CODIFICATION_LAYER
            },
            new User()
            {
                Id = 3,
                Email = "malina@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("malina"),
                Type = Enums.EUserType.GOVERNMENT_EMPLOYEE
            });
        }       
    }
}
