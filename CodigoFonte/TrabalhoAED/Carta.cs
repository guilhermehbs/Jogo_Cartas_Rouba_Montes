using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Carta
    {
        private string valor { get; set; }
        private string naipe { get; set; }

        public Carta(string valor, string naipe)
        {
            this.valor = valor;
            this.naipe = naipe;
        }

        public override bool Equals(object obj)
        {

            Carta outraCarta = (Carta)obj;

            return (valor == outraCarta.valor) && (naipe == outraCarta.naipe);
        }

        public override string ToString()
        {
            return $"Valor: {valor} || Naipe: {naipe}";
        }
    }
}
