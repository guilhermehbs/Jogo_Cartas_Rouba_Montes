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

        //Construtor das demais cartas
        public Carta(string valor, string naipe)
        {
            this.valor = valor;
            this.naipe = naipe;
        }

        //Construtor para o coringa
        public Carta(string valor)
        {
            this.valor = valor;
            naipe = null;
        }

        //metodo para sobreescrever o Equals padrão, usado para verificar a igualdade entre cartas
        public override bool Equals(object obj)
        {
            Carta outraCarta = (Carta)obj;

            return (valor == outraCarta.valor) && (naipe == outraCarta.naipe);
        }

        //sobreescrevendo o ToString para imprimir uma carta
        public override string ToString()
        {
            return $"Valor: {valor} || Naipe: {naipe}";
        }
    }
}
