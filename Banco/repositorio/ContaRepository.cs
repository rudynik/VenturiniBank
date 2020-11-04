using Banco.data;
using Banco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.repositorio
{
    public class ContaRepository : IContaRepository
    {
        private ApplicationDbContext context;

        public ContaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ContaCorrente GetConta(ContaCorrente conta)
        {
            return context.ContaCorrente
                          .Where(x => x.Agencia == conta.Agencia)
                          .Where(x => x.Numero == conta.Numero)
                          .FirstOrDefault();
        }

        public async void AddConta(ContaCorrente conta)
        {
            context.ContaCorrente.Add(conta);
            await context.SaveChangesAsync();
        }

        public async Task<ContaCorrente> BuscarConta(int id)
        {
            return await context.ContaCorrente.FindAsync(id);
        }
    }
}
