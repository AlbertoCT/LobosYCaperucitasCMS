using System;
using System.Data.Entity;

namespace DataBaseModel
{
    public class Articulo
    {
        public int Id { get; set; }
        public Categoria Categoria { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public bool EnStock { get; set; }
        public int DiasProduccion { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioWeb { get; set; }
        public decimal PrecioRebajas { get; set; }
        public decimal PVP { get; set; }

        public Articulo()
        {
            Referencia = string.Format("LC{0}{1}{2}{3}", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), new Random().Next(99).ToString());
        }
    }

    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class LobosYCaperucitasContext : DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public LobosYCaperucitasContext()
            : base("LobosYCaperucitas")
        {
           
        }
    }
}
