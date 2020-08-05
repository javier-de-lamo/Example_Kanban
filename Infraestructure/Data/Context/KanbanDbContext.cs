namespace Infraestructure.Data.Context
{
    using AppCore.Entities;
    using Microsoft.EntityFrameworkCore;

    public class KanbanDbContext : DbContext
    {
        public DbSet< TaskItem > TaskItems { get; set; }

        public KanbanDbContext( DbContextOptions< KanbanDbContext > options ) : base( options ) { }
    }
}