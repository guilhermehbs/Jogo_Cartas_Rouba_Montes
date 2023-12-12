using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Jogador
    {
        private string nome;
        private int posicao;
        private int numeroDeCartasNoMonte;

        public Stack<Carta> monteJogador;

        public Queue<int> rankingUltimas5;

        //Construtor para o jogador
        public Jogador(string nome)
        {
            this.nome = nome;
            this.rankingUltimas5 = new Queue<int>(5);
            this.monteJogador = new Stack<Carta>();
        }

        //Método para adicionar 1 cartas no seu monte
        public void adicionarCarta(Carta carta)
        {
            monteJogador.Push(carta);
        }

        //Método para adicionar 2 cartas no seu monte
        public void adicionarCartas(Carta carta1, Carta carta2)
        {
            monteJogador.Push(carta1);
            monteJogador.Push(carta2);
        }

        //Método para adicionar as cartas do monte de outro jogador no seu monte
        public void adicionarMonte(Stack<Carta> monte)
        {
            foreach (Carta carta in monte)
            {
                monteJogador.Push(carta);
            }
        }

        //Método para limpar o monte do jogador
        public void limparMonte()
        {
            monteJogador.Clear();
        }

        //Método para mostrar a carta que está no topo do monte do jogador
        public void mostrarTopo()
        {
            if (monteJogador.Count() == 0)
            {
                Console.WriteLine("Monte vazio");
            }
            else
            {
                Console.WriteLine("Carta no topo: " + monteJogador.Peek());
            }
        }

        //Método para receber a carta que está no topo do monte do jogador
        public Carta getTopo()
        {
            if (monteJogador.Count() == 0)
            {
                return null;
            }
            else
            {
                return monteJogador.Peek();
            }
        }

        //Método para mostrar o nome do jogador
        public string getNome()
        {
            return nome;
        }

        //Método para mostrar a quantidades de cartas no monte
        public int getQuantidadeDeCartasNoMonte()
        {
            return numeroDeCartasNoMonte;
        }

        //Método para setar a quantidades de cartas no monte
        public void setQuantidadeDeCartasNoMonte(int quantidadeDeCarta)
        {
            this.numeroDeCartasNoMonte = quantidadeDeCarta;
        }

        //Método para mostrar a posição do jogador na ultima partida
        public int getPosicao()
        {
            return posicao;
        }

        //Método para setar a posção do jogador na ultima partida
        public void setPosicao(int posicao)
        {
            this.posicao = posicao;
        }

        //Método para imprimir o ranking das ultimas 5 partidas no log
        public string ImprimirRanking()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Ranking:");
            foreach (int valor in rankingUltimas5)
            {
                sb.Append(valor).Append(", ");
            }
            sb.Length -= 2;
            return sb.ToString();
        }

        //Método para mostrar o ranking no console
        public void MostrarRaking()
        {
            Console.WriteLine("Ranking:");

            foreach(int valor in rankingUltimas5)
            {
                Console.WriteLine(valor);
            }
        }

        //Sobreescrevendo o ToString para retornar o nome do jogador
        public override string ToString()
        {
            return nome;
        }
    }
}
