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

        public void AddConta(ContaCorrente conta)
        {
            context.ContaCorrente.Add(conta);
            context.SaveChanges();
        }

        public ContaCorrente BuscarConta(int id)
        {
            return context.ContaCorrente.Where(x => x.Id == id).FirstOrDefault();
        }

        public void AtualizarConta(ContaCorrente conta)
        {
            context.ContaCorrente.Update(conta);
            context.SaveChanges();
        }

        public string GetCliente(int id)
        {
            return context.Cliente.Where(x => x.Id == id)
                                  .Select(x => x.Nome)
                                  .FirstOrDefault();
        }
    }
}
