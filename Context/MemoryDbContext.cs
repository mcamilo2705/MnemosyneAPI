using Microsoft.EntityFrameworkCore;
using MnemosyneAPI.Model;

namespace MnemosyneAPI.Context
{
    //herdando funcoes da classe DbContext
    public class MemoryDbContext : DbContext
    {
        //criando um metodo construtor da classe, recebendo um valor do banco como parametro
        public MemoryDbContext(DbContextOptions<MemoryDbContext> options) : base (options)
        {

        }

        //Criando uma tabela para o banco de dados, com base na classe Memory
        public DbSet<Memory> Memories => Set<Memory>();
    }
}
