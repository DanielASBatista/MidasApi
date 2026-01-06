using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI.Data
{
    // Essa classe é o "coração" do EF(migração) Core:, pq conecta as Models ao banco
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Cada DbSet vira uma tabela no banco
        public DbSet<Projecao> Projecoes { get; set; } = null!;
        public DbSet<Lancamento> Lancamentos { get; set; } = null!;
        public DbSet<Emprestimo> Emprestimos {get; set; } = null!;

        // É aqui o OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define nomes de tabelas para serem alimentadas e chamadas. Também defini o relacionamentos e pk e fk
            modelBuilder.Entity<Projecao>().ToTable("Projecoes");
            modelBuilder.Entity<Projecao>().HasKey(p => p.IdProjecao);

            modelBuilder.Entity<Lancamento>().ToTable("Lancamentos");
            modelBuilder.Entity<Lancamento>().HasKey(l => l.IdLancamento);
            
            modelBuilder.Entity<Emprestimo>().ToTable("Emprestimos");
            modelBuilder.Entity<Emprestimo>().HasKey(e => e.IdSimEmprestimo);
            base.OnModelCreating(modelBuilder);
        }
    }
}
