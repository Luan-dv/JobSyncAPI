using JobSync.Domain.Entities; // vem daqui  expense
using JobSync.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace JobSync.Infrastucture.DataAcess;
internal class JobSyncDbContext : DbContext
{
    public JobSyncDbContext(DbContextOptions options) : base(options) { }//repassar para a classe base o parametro options

    public DbSet<User> Users { get; set; } //sempre nome da tabela no BD

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Converter Gender enum -> string
        var genderConverter = new EnumToStringConverter<GenderChoice>();

        modelBuilder.Entity<User>()
            .Property(u => u.Gender)
            .HasConversion(genderConverter);
            

        base.OnModelCreating(modelBuilder);
    }

}
