using Banco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.repositorio
{
    public interface IContaRepository
    {
        ContaCorrente GetConta(ContaCorrente conta);
        void AddConta(ContaCorrente conta);

        Task<ContaCorrente> BuscarConta(int id);
    }
}
