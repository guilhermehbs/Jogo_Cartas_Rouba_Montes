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

        static Stack<Carta> baralho;
        static Queue<Jogador> filaDeJogadores;

        static int quantidadeDeJogadores;
        static int quantidadeDeBaralhos;

        // Construtor usado para gerar um novo jogo, que vai gerar as cartas e a fila de jogadores
        public Jogo(int quantidadeDeJogadores, int quantidadeDeBaralhos)
        {
            // Verificação para que a quantidade de baralhos seja maior que 1
            if (quantidadeDeBaralhos < 1)
            {
                throw new Exception("Quantidade de baralhos errada (deve ser maior que 0)");
            }

            // Verificação para que a quantidade de jogadores seja maior que 2
            if (quantidadeDeJogadores < 2)
            {
                throw new Exception("Quantidade de jogadores deve ser maior que 2");
            }

            quantidadeDeJogadores = quantidadeDeJogadores;
            quantidadeDeBaralhos = quantidadeDeBaralhos;

            // Gerando a fila de jogadores
            filaDeJogadores = GerarFilaDeJogadores(quantidadeDeJogadores);

            // Gerando o baralho
            baralho = GerarBaralho(quantidadeDeBaralhos);
        }

        public void Jogar()
        {
            StreamWriter log = new StreamWriter("log.txt");

            log.WriteLine($"O jogo foi criado com {quantidadeDeBaralhos * 54} cartas");
            log.WriteLine($"Total de jogadores do jogo: {quantidadeDeJogadores}");

            //Gerando e adicionando as cartas na mesa, e imprimindo
            List<Carta> cartasDaMesa = new List<Carta>();

            for (int i = 0; i < 4; i++)
            {
                cartasDaMesa.Add(baralho.Pop());
            }

            log.WriteLine("Cartas inseridas na mesa: ");

            foreach(Carta carta in cartasDaMesa)
            {
                log.WriteLine(carta);
            }

            Console.WriteLine("Cartas na mesa:");

            ImprimirListaDeCartas(cartasDaMesa);

            //Gerando e preenchendo uma lista de jogadores
            List<Jogador> listaDeJogadores = new List<Jogador>();

            for (int i = 0; i < quantidadeDeJogadores; i++)
            {

                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                listaDeJogadores.Add(jogadorAtual);

                filaDeJogadores.Enqueue(jogadorAtual);
            }

            log.WriteLine("Jogadores da partida: ");

            foreach(Jogador jogador in listaDeJogadores)
            {
                log.WriteLine(jogador);
            }

            //Colocando a carta no monte do jogador inicial
            Jogador jogadorInicial = filaDeJogadores.Dequeue();
            Carta cartaInicial = baralho.Pop();
            jogadorInicial.adicionarCarta(cartaInicial);
            filaDeJogadores.Enqueue(jogadorInicial);

            log.WriteLine($"Carta: {cartaInicial} foi adicionada no monte do {jogadorInicial}");


            bool jogoAcabou = false;

            //Repetição até acabar as cartas do baralho
            while (!jogoAcabou)
            {

                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                Console.WriteLine($"\nJogador a jogar: {jogadorAtual}");
                log.WriteLine($"Jogador a jogar: {jogadorAtual}");

                bool vezDoJogador = true;

                while (vezDoJogador)
                {

                    Console.WriteLine("Cartas na mesa: ");

                    ImprimirListaDeCartas(cartasDaMesa);

                    foreach (Jogador jogador in listaDeJogadores)
                    {
                        Console.WriteLine("\nTopo do jogador: " + jogador + "\n");
                        jogador.mostrarTopo();
                    }


                    Carta cartaAtual;

                    try
                    {
                        cartaAtual = baralho.Pop();
                    }
                    catch
                    {
                        jogoAcabou = true;
                        break;
                    }

                    log.WriteLine($"Carta da rodada: {cartaAtual}");

                    bool cartaCoringa = false;

                    if(cartaAtual.getValor() == "Coringa")
                    {
                        cartaCoringa = true;
                    }

                    bool existe1 = false;
                    bool existe2 = false;
                    bool existe3 = false;

                    Console.WriteLine("\nCarta atual: " + cartaAtual + "\n");

                    //Método para verificar se existe alguma jogada possível com a carta atual
                    foreach (Carta carta in cartasDaMesa)
                    {
                        if (carta.getValor() == cartaAtual.getValor())
                        {
                            existe1 = true;
                            break;
                        }

                    }

                    //Método para verificar se existe alguma jogada possível com a carta atual
                    for (int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        if (listaDeJogadores[i].getTopo() != null && cartaAtual.getValor() == listaDeJogadores[i].getTopo().getValor())
                        {
                            existe2 = true;
                            break;
                        }
                    }

                    if(cartaAtual == jogadorAtual.getTopo())
                    {
                        existe3 = true;
                        break;
                    }

                    int opcao = 0;

                    if (existe1 || existe2 || existe3 || cartaCoringa)
                    {
                        bool jogadaOk = false;

                        while (!jogadaOk)
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
                                    int opcaoCarta = EscolherOpcao(cartasDaMesa);

                                    Carta cartaDesejada = cartasDaMesa[opcaoCarta];

                                    if (cartaDesejada.getValor() == cartaAtual.getValor() || cartaCoringa)
                                    {
                                        jogadorAtual.adicionarCartas(cartaDesejada, cartaAtual);
                                        cartasDaMesa.Remove(cartaDesejada);
                                        jogadaOk = true;
                                        log.WriteLine($"Jogador: {jogadorAtual} adicionou as cartas {cartaAtual}, {cartaDesejada} em seu monte");
                                    }
                                    else
                                    {
                                        Console.WriteLine("A carta selecionada é diferente da carta que está na sua mão");
                                    }
                                    break;

                                case 2:
                                    Console.Write("Digite o nome do jogador para roubar o monte: ");
                                    string nomeJogadorRoubo = Console.ReadLine();
                                    Jogador jogadorRoubo = listaDeJogadores.Find(j => j.getNome() == nomeJogadorRoubo);

                                    if (cartaAtual.getValor() == jogadorRoubo.getTopo().getValor() || cartaCoringa)
                                    {
                                        jogadorAtual.adicionarMonte(jogadorRoubo.monteJogador);
                                        jogadorAtual.adicionarCarta(cartaAtual);
                                        jogadorRoubo.limparMonte();
                                        jogadaOk = true;
                                        log.WriteLine($"Jogador: {jogadorAtual} roubou o monte do jogador: {jogadorRoubo}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("A carta não é igual o monte do jogador selecionado");
                                    }

                                    break;

                                case 3:
                                    if (cartaAtual.getValor() == jogadorAtual.getTopo().getValor() || cartaCoringa)
                                    {
                                        jogadorAtual.adicionarCarta(cartaAtual);
                                        jogadaOk = true;
                                        log.WriteLine($"Jogador: {jogadorAtual} adicionou a carta: {cartaAtual} em seu monte");
                                    }
                                    break;

                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Opção inválida");
                                    Console.ResetColor();
                                    break;
                            }
                        }
                    }

                    //Se a carta atual não estiver no topo do monte de nenhum jogador, nem nas cartas da mesa, ele passa para o proximo jogador da fila e descarta a carta
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Com essa carta atual não é possível fazer nenhuma jogada, vez do próximo jogador!");
                        Console.ResetColor();
                        vezDoJogador = false;
                        cartasDaMesa.Add(cartaAtual);
                        log.WriteLine($"Jogador: {jogadorAtual} perdeu sua vez, pois com a carta: {cartaAtual} não foi possível fazer nenhuma jogada");
                    }
                }

                //Colocando o jogador atual no final da fila de jogadores
                filaDeJogadores.Enqueue(jogadorAtual);

                //Verificação se o baralho acabou, se sim, ele deve finalizar o jogo
                if (baralho.Count == 0)
                {
                    log.WriteLine("Jogo acabou");
                    log.WriteLine("Estatísticas da partida: ");

                    jogoAcabou = true;
                    Jogador jogadorCampeao = listaDeJogadores[0];
                    listaDeJogadores[0].setQuantidadeDeCartasNoMonte(listaDeJogadores[0].monteJogador.Count);
                    Console.WriteLine($"Jogador: {listaDeJogadores[0].getNome()}" + listaDeJogadores[0].monteJogador.Count);

                    for (int i = 1; i < listaDeJogadores.Count; i++)
                    {
                        Console.WriteLine($"Jogador: {listaDeJogadores[i].getNome()}" + listaDeJogadores[i].monteJogador.Count);

                        listaDeJogadores[i].setQuantidadeDeCartasNoMonte(listaDeJogadores[i].monteJogador.Count);

                        if (listaDeJogadores[i].monteJogador.Count > jogadorCampeao.monteJogador.Count)
                        {
                            jogadorCampeao = listaDeJogadores[i];
                        }
                    }

                    for (int i = 0; i < listaDeJogadores.Count - 1; i++)
                    {
                        for (int j = 0; j < listaDeJogadores.Count - i - 1; j++)
                        {
                            if (listaDeJogadores[j].monteJogador.Count < listaDeJogadores[j + 1].monteJogador.Count)
                            {
                                Jogador temp = listaDeJogadores[j];
                                listaDeJogadores[j] = listaDeJogadores[j + 1];
                                listaDeJogadores[j + 1] = temp;
                            }
                        }
                    }

                    StreamWriter jogadores = new StreamWriter("jogadores.txt");

                    for (int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        listaDeJogadores[i].setPosicao(i + 1);
                        listaDeJogadores[i].rankingUltimas5.Enqueue(i + 1);
                        jogadores.WriteLine(new string('=', 30));
                        jogadores.WriteLine($"Jogador: {listaDeJogadores[i].getNome()}");
                        jogadores.WriteLine($"Posição na última partida: {i + 1}");
                        jogadores.WriteLine($"Total de cartas na mão na ultima partida: {listaDeJogadores[i].getQuantidadeDeCartasNoMonte()}");
                        log.WriteLine(new string('=', 30));
                        log.WriteLine($"Jogador: {listaDeJogadores[i].getNome()}");
                        log.WriteLine($"Posição na última partida: {i + 1}");
                        log.WriteLine($"Total de cartas na mão na ultima partida: {listaDeJogadores[i].getQuantidadeDeCartasNoMonte()}");
                    }

                    Console.WriteLine("Jogador Campeão: " + jogadorCampeao);

                    jogadores.Close();
                }
            }
        }

        //Método para imprimir as cartas de uma determinada lista
        private void ImprimirListaDeCartas(List<Carta> lista)
        {
            foreach (Carta carta in lista)
            {
                Console.WriteLine(carta);
            }
        }

        //Método para escolher a carta desejada (usado no switch case)
        private int EscolherOpcao(List<Carta> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"Opção {i + 1}: {lista[i]}");
            }

            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine());

            while (opcao < 1 || opcao > lista.Count)
            {
                Console.WriteLine("Opção inválida");
                Console.Write("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
            }

            return opcao - 1;
        }

        //Método para gerar o baralho já embaralhado 
        private Stack<Carta> GerarBaralho(int quantidadeDeBaralhos)
        {

            Stack<Carta> pilhaDeCartas = new Stack<Carta>();

            string[] naipes = { "Paus", "Espadas", "Copas", "Ouros" };

            string[] valores = { "Coringa", "Coringa", "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            Random rnd = new Random();
            for (int i = 0; i < quantidadeDeBaralhos; i++)
            {
                int totalCartas = 0;

                Stack<Carta> pilhaTemp = new Stack<Carta>();

                while (totalCartas != 8)
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

        //Método para gerar a fila de jogadores
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
    }
}