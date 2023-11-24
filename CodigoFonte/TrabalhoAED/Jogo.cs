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

        //metodo que inicia o jogo, gerando um baralho de acordo com a quantidade de cartas e gerando a fila de jogadores
        public void ComecarJogo(int quantidadeJogadores, int quantidadeDeBaralhos)
        {
            if (quantidadeDeBaralhos < 1)
            {
                throw new Exception("Quantidade de baralhos errada (deve ser maior que 0)");
            }

            if (quantidadeJogadores < 2)
            {
                throw new Exception("Quantidade de jogadores deve ser maior que 2");
            }

            filaDeJogadores = GerarFilaDeJogadores(quantidadeJogadores);

            monte = GerarBaralho(quantidadeDeBaralhos);

            // foreach (Carta carta in monte)
            // {
            //     Console.WriteLine(carta);
            // }

            descarte = new Stack<Carta>();

            bool jogoAcabou = false;
            while(!jogoAcabou){
                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                Console.WriteLine($"Jogador a jogar: {jogadorAtual}");
                
                filaDeJogadores.Enqueue(jogadorAtual);
            } // so vai sair daqui quando o jogo acabar, dito isso , se vc rodar  isso aqui AGORA vai crashar kkkkkkk

        }


        //metodo que gera um baralho já embaralhado para inicio do jogo
        public Stack<Carta> GerarBaralho(int quantidadeDeBaralhos)
        {
            //int quantidadeDeCartas = quantidadeDeBaralhos * 53;

            Stack<Carta> pilhaDeCartas = new Stack<Carta>();

            string[] naipes = { "Paus", "Espadas", "Copas", "Ouros" };

            string[] valores = { "Coringa", "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            Random rnd = new Random();
            for (int i = 0; i < quantidadeDeBaralhos; i++)
            {
                int totalCartas = 0;
                Stack<Carta> pilhaTemp = new Stack<Carta>();
                while (totalCartas != 53)
                {
                    int randomNaipe = rnd.Next(0, 4);
                    int randomValor = rnd.Next(0, 14);

                    Carta carta;

                    if (randomValor != 0)
                    {
                        carta = new Carta(valores[randomValor], naipes[randomNaipe]);
                    }
                    else
                    {
                        carta = new Carta(valores[randomValor]);
                    }



                    if (!pilhaTemp.Contains(carta))
                    {
                        pilhaTemp.Push(carta);
                        totalCartas++;
                    }

                }
                foreach (Carta cartaTemp in pilhaTemp)
                {
                    pilhaDeCartas.Push(cartaTemp);
                }
            }
            return pilhaDeCartas;

        }




        public Queue<Jogador> GerarFilaDeJogadores(int quantidade)
        {
            Queue<Jogador> fila = new Queue<Jogador>();

            Console.WriteLine(new String('-', 9) + "Definição de Jogadores" + new String('-', 9));

            for (int i = 0; i < quantidade; i++)
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