using Microsoft.EntityFrameworkCore;

namespace ContenedorDB
{
    public class DBContextVivero : DbContext
    {
        public DBContextVivero(DbContextOptions<DBContextVivero> options) : base (options) 
        {
        
        }

        //referencia a clases para crear las tablas
        public  DbSet<Cliente> Clientes { get; set; }
        public  DbSet<Factura> Facturas { get; set; }
        public  DbSet<Producto> Productos { get; set; }
        public  DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se asinga los nombres en singular al mapear las tablas
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}