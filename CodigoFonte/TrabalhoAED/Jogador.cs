using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Jogador
    {
        private string nome { get; set; }
        private int posicao { get; set; }  
        private int numeroDeCartasNaMao{ get; set; }

        public Stack<Carta> monteJogador { get; set; }

        public List<Carta> cartasNaMao;

        Queue<int> rankingUltimas5;

        //implementar fila contendo ranking do jogador nas ultimas 5 partidas

        //Construtor para o jogador
        public Jogador(string nome) 
        {
            this.nome = nome;
            this.rankingUltimas5 = new Queue<int>(5);
            this.cartasNaMao = new List<Carta>();
            this.monteJogador = new Stack<Carta>();
        }

        public void adicionarCarta(Carta carta)
        {
            cartasNaMao.Add(carta);
            numeroDeCartasNaMao++;
        }

        public void removerCarta(Carta carta)
        {
            cartasNaMao.Remove(carta);
            numeroDeCartasNaMao--;
        }

        public void mostrarLista()
        {
            foreach(Carta carta in cartasNaMao)
            {
                Console.WriteLine(carta);
            }
        }

        public void mostrarTopo()
        {
            if (monteJogador.Count() == 0)
            {
                Console.WriteLine("Não há cartas no monte do jogador");
            }
            else
            {
                    foreach(var carta in monteJogador){
                        Console.WriteLine("Carta do topo: " + carta);
                        break;
                    }
            }
        }

        public override string ToString()
        {
            return $" {nome} ";
        }
    }
}
