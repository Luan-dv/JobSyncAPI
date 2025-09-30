using JobSync.Domain.Entities; // vem daqui  expense
using Microsoft.EntityFrameworkCore;

namespace JobSync.Infrastucture.DataAcess;
internal class JobSyncDbContext : DbContext
{
    public JobSyncDbContext(DbContextOptions options) : base(options) { }//repassar para a classe base o parametro options

    public DbSet<User> Users { get; set; } //sempre nome da tabela no BD


}
