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
        static Queue<Jogador> filaDeJogadores;
        static List<Carta> cartasDaMesa;

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

            cartasDaMesa = new List<Carta>();


            for(int i = 0; i < 4; i++)
            {
                cartasDaMesa.Add(monte.Pop());
            }

            Console.WriteLine("Cartas na mesa: ");

            ImprimirCartas(cartasDaMesa);

            bool jogoAcabou = false;


            List<Jogador> listaDeJogadores = new List<Jogador>();

            for (int i = 0; i < quantidadeJogadores; i++)
            {

                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                listaDeJogadores.Add(jogadorAtual);

                filaDeJogadores.Enqueue(jogadorAtual);
            }

            while(!jogoAcabou){
                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                Console.WriteLine($"\nJogador a jogar: {jogadorAtual}");

                bool vezDoJogador = true;

                while (vezDoJogador)
                { 
                    foreach (Jogador jogador in listaDeJogadores)
                    {
                        Console.WriteLine("\nTopo do jogador: " + jogador + "\n");
                        jogador.mostrarTopo();
                    }

                    int opcao = 0;
                    Carta cartaAtual = monte.Pop();

                    bool existe1 = false;
                    bool existe2 = false;

                    Console.WriteLine("\nCarta atual: " + cartaAtual + "\n");

                    foreach(Carta carta in cartasDaMesa)
                    {
                        if(carta == cartaAtual)
                        {
                            existe1 = true;
                            break;
                        }

                    }

                    for(int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        if(cartaAtual == listaDeJogadores[i].getTopo())
                        {
                            existe2 = true;
                            break;
                        }
                    }

                    if (existe1 && existe2)
                    {
                        Console.WriteLine("Menu:");
                        Console.WriteLine("1 - Formar monte com carta na mesa");
                        Console.WriteLine("2 - Roubar monte de um jogador");
                        Console.WriteLine("3 - Adicionar carta no topo do próprio monte");
                        Console.Write("Digite a opção desejada: ");
                        opcao = int.Parse(Console.ReadLine());

                        switch (opcao)
                        {
                            case 1:
                                EscolherOpcao(cartasDaMesa);

                                Console.Write("Digite a carta desejada: ");
                                int opcaoCarta = int.Parse(Console.ReadLine());

                                Carta cartaDesejada = cartasDaMesa[opcaoCarta - 1];

                                if (cartaDesejada.getValor() == cartaAtual.getValor())
                                {
                                    jogadorAtual.adicionarCartas(cartaDesejada, cartaAtual);
                                }
                                else
                                {
                                    Console.WriteLine("A carta selecionada é diferente da carta que está na sua mão");
                                }
                                break;

                            case 2:
                                break;

                            case 3:
                                if (cartaAtual.getValor() == jogadorAtual.getTopo().getValor())
                                {
                                    jogadorAtual.adicionarCartaNoMonte(cartaAtual);
                                }
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opção inválida");
                                Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        vezDoJogador = false;
                        cartasDaMesa.Add(cartaAtual);
                    }
                }

                filaDeJogadores.Dequeue();
                filaDeJogadores.Enqueue(jogadorAtual);

                if(monte.Count == 0)
                {
                    jogoAcabou = true;
                }
            }
        }

        //metodo que gera um baralho já embaralhado para inicio do jogo
        private Stack<Carta> GerarBaralho(int quantidadeDeBaralhos)
        {
            //int quantidadeDeCartas = quantidadeDeBaralhos * 53;

            Stack<Carta> pilhaDeCartas = new Stack<Carta>();

            string[] naipes = { "Paus", "Espadas", "Copas", "Ouros" };

            string[] valores = { "Coringa", "Coringa", "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            Random rnd = new Random();
            for (int i = 0; i < quantidadeDeBaralhos; i++)
            {
                int totalCartas = 0;
                Stack<Carta> pilhaTemp = new Stack<Carta>();
                while (totalCartas != 54)
                {
                    int randomNaipe = rnd.Next(0, 4);
                    int randomValor = rnd.Next(0, 15);

                    Carta carta;

                    if (randomValor != 0 || randomValor != 1)
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

        private Queue<Jogador> GerarFilaDeJogadores(int quantidade)
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

        private void ImprimirCartas(List<Carta> lista)
        {
            foreach (Carta carta in lista)
            {
                Console.WriteLine(carta);
            }
        }

        private int EscolherOpcao(List<Carta> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"Opção {i + 1}: {lista[i]}");
            }

            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine());

            while(opcao < 1 || opcao > lista.Count)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
            }

            return opcao - 1;
        }

        //private void DescartarCarta(Jogador jogador)
        //{
        //    Console.WriteLine("Digite uma carta para descartar: ");

        //    int opcao = EscolherOpcao(jogador.cartasNaMao);

        //    cartasDaMesa.Add(jogador.cartasNaMao[opcao]);
        //    jogador.removerCarta(jogador.cartasNaMao[opcao]);
        //}
    }
}