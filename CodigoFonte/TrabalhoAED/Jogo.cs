using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Jogo
    {

        static Stack<Carta> monte;
        static Stack<Carta> descarte;
        static Queue<Jogador> filaDeJogadores;
        //escolher a quantidade de cartas
        //escolher a quantidade de jogadores


        /*
        apresentar ganhadores (nome, posição e numero de cartas na mão), apresentar de forma ordenada as cartas presentes na mão do jogador
        apresentar o ranking de jogadores ordenado de acordo com a quantidade de cartas na mão
        */


        /*
        metodo para pesquisar o histórico de posições de um jogador especifico (apresentar os dados da fila presente na classe jogador)

        todas as ações da partida devem estar presente em um log, registando todas as ações (quantidade de cartas, jogadores da partida)
        apresentar o nome de jogadores
        jogador que vai iniciar a partida
        mostrar a carta que o usuário retirou do monte de cartas
        tudo isso em um arquivo e no terminal
        */

        public void ComecarJogo(int quantidadeJogadores, int quantidadeDeCartas)
        {
            if(quantidadeDeCartas > 52 || quantidadeDeCartas < 1)
            {
                throw new Exception("Quantidade de cartas errada (deve ser maior que 0 e menor que 53)");
            }

            if (quantidadeJogadores < 2)
            {
                throw new Exception("Quantidade de jogadores deve ser maior que 2");
            }

            filaDeJogadores = GerarFilaDeJogadores(quantidadeJogadores);

            monte = GerarBaralho(quantidadeDeCartas);
            descarte = new Stack<Carta>();
        }

        public Stack<Carta> GerarBaralho(int quantidadeDeCartas)
        {
            Stack<Carta> pilhaDeCartas = new Stack<Carta>();

            string[] naipes = {"Paus", "Espadas", "Copas", "Ouros" };

            string[] valores = { "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            Random rnd = new Random();

            int totalCartas = 0;

            while(totalCartas != quantidadeDeCartas)
            {
                int randomNaipe = rnd.Next(0, 4);
                int randomValor = rnd.Next(0, 13);

                Carta carta = new Carta(valores[randomValor], naipes[randomNaipe]);

                if (!pilhaDeCartas.Contains(carta))
                {
                    pilhaDeCartas.Push(carta);
                    totalCartas++;
                }
            }

            return pilhaDeCartas;
        }

        public Queue<Jogador> GerarFilaDeJogadores(int quantidade)
        {
            Queue<Jogador> fila = new Queue<Jogador>();

            Console.WriteLine(new String('-', 9) + "Definição de Jogadores" + new String('-', 9));

            for(int i = 0; i < quantidade; i++)
            {
                Console.Write($"Digite o nome do {i + 1}° jogador: ");
                string nome = Console.ReadLine();

                fila.Enqueue(new Jogador(nome));
            }

            Console.WriteLine(new String('-', 40));

            return fila;
        }
    }
}
