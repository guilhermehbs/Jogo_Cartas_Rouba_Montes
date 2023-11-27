using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Jogo
    {

        static Stack<Carta> monte;
        static List<Carta> descarte;
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

            descarte = new List<Carta>();

            bool jogoAcabou = false;


            List<Jogador> listaDeJogadores = new List<Jogador>();
            for (int i = 0; i < quantidadeJogadores; i++)
            {

                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                listaDeJogadores.Add(jogadorAtual);

                for (int j = 0; j < 4; j++)
                {
                    jogadorAtual.adicionarCarta(monte.Pop());
                }
                filaDeJogadores.Enqueue(jogadorAtual);
            }

            

            int k = 0;
            while(!jogoAcabou){
                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                Console.WriteLine($"Jogador a jogar: {jogadorAtual}");

                //mostrar carta na mão
                Console.WriteLine($"\nCartas na sua mão:\n");
                jogadorAtual.mostrarLista();
               
                foreach(Jogador jogador in listaDeJogadores)
                {
                    Console.WriteLine("Topo do jogador: " + jogador);
                    jogador.mostrarTopo();
                }

                //mostrar descarte
                if(descarte.Count() == 0)
                {
                    Console.WriteLine("Área de Descarte vazia");
                }
                else
                {
                    Console.WriteLine("Área de Descarte:\n");
                    foreach (Carta carta in descarte)
                    {
                        Console.WriteLine(carta);
                    }
                }

                int opcao = 0;
                Carta cartaAtual = monte.Pop();

                Console.WriteLine("Carta atual: " + cartaAtual);

                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Adicionar carta na área de descarte");
                Console.WriteLine("2 - Roubar monte de um jogador");
                Console.WriteLine("3 - Formar monte com carta da área de descarte");
                Console.WriteLine("4 - Formar monte com carta na sua mão");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        descarte.Add(cartaAtual);
                        break;

                    case 2:
                        break;
                }

                //Console.WriteLine("\nMontão:\n\n" + monte.Peek());


                Console.WriteLine("\nMonte dos outros jogadores:\n");
                
                filaDeJogadores.Enqueue(jogadorAtual);

                k++;
                if(k == 2)
                {
                    jogoAcabou = true;
                }

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