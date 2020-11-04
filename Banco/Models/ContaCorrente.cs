using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco.Models
{
    public class ContaCorrente
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório")]
        [Display(Name = "Titular")]
        public Cliente Titular { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Agência")]
        public int Agencia { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Número")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Saldo")]
        private double _saldo = 100;

        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        public bool Sacar(double valor)
        {
            if (_saldo < valor)
            {
                return false;
            }

            _saldo -= valor;
            return true;
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }


        public bool Transferir(double valor, ContaCorrente contaDestino)
        {
            if (_saldo < valor)
            {
                return false;
            }

            _saldo -= valor;
            contaDestino.Depositar(valor);
            return true;
        }
    }
}
