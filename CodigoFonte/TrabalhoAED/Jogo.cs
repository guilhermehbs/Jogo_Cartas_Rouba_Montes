using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        List<Jogador> lendasQueJaJogaram = new List<Jogador>();

        int quantidadeDeJogadores;
        int quantidadeDeBaralhos;
        StreamWriter log = new StreamWriter("log.txt");

        // Construtor usado para gerar um novo jogo, que vai gerar as cartas e a fila de jogadores
        public Jogo(int quantidadeDeJogadores, int quantidadeDeBaralhos, List<Jogador> lendasQueJaJogaram)
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

            this.quantidadeDeJogadores = quantidadeDeJogadores;
            this.quantidadeDeBaralhos = quantidadeDeBaralhos;
            this.lendasQueJaJogaram = lendasQueJaJogaram;

            // Gerando a fila de jogadores
            filaDeJogadores = GerarFilaDeJogadores(quantidadeDeJogadores, lendasQueJaJogaram);

            // Gerando o baralho
            baralho = GerarBaralho(quantidadeDeBaralhos);
        }

        public void Jogar()
        {
            //Salvando no log as informações iniciais da partida
            log.WriteLine($"O jogo foi criado com {quantidadeDeBaralhos * 54} cartas\n");
            log.WriteLine($"Total de jogadores do jogo: {quantidadeDeJogadores}\n");

            //Gerando e adicionando as cartas na mesa, e imprimindo
            List<Carta> cartasDaMesa = new List<Carta>();

            for (int i = 0; i < 4; i++)
            {
                cartasDaMesa.Add(baralho.Pop());
            }

            //Salvando no log as cartas na mesa
            log.WriteLine("Cartas inseridas na mesa:");

            foreach (Carta carta in cartasDaMesa)
            {
                log.WriteLine(carta);
            }

            //Gerando e preenchendo uma lista de jogadores
            List<Jogador> listaDeJogadores = new List<Jogador>();

            for (int i = 0; i < quantidadeDeJogadores; i++)
            {

                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                listaDeJogadores.Add(jogadorAtual);

                filaDeJogadores.Enqueue(jogadorAtual);
            }

            //Salvando no log os jogadores da partida
            log.WriteLine("Jogadores da partida:");

            foreach (Jogador jogador in listaDeJogadores)
            {
                log.WriteLine(jogador);
            }

            //Colocando a carta no monte do jogador inicial
            Jogador jogadorInicial = filaDeJogadores.Dequeue();
            Carta cartaInicial = baralho.Pop();
            jogadorInicial.adicionarCarta(cartaInicial);
            filaDeJogadores.Enqueue(jogadorInicial);

            log.WriteLine($"\nCarta: {cartaInicial} foi adicionada no monte do {jogadorInicial}\n");

            bool jogoAcabou = false;

            //Repetição até acabar as cartas do baralho
            while (!jogoAcabou)
            {

                //Atribuindo e mostrando o jogador atual
                Jogador jogadorAtual = filaDeJogadores.Dequeue();
                Console.WriteLine(new String('-', 40));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nJogador a jogar: {jogadorAtual}");
                Console.ResetColor();
                log.WriteLine($"Jogador a jogar: {jogadorAtual}\n");

                bool vezDoJogador = true;

                while (vezDoJogador)
                {
                    //Mostrar carta na mesa
                    Console.WriteLine("\nCartas na mesa: ");
                    log.WriteLine("\nCartas na mesa:");

                    ImprimirListaDeCartas(cartasDaMesa);

                    //Imprimit o topo de todos os jogadores
                    foreach (Jogador jogador in listaDeJogadores)
                    {
                        Console.WriteLine(new String('-', 40));
                        log.WriteLine(new String('-', 40));
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nTopo do jogador: " + jogador + "\n");
                        log.WriteLine("\nTopo do jogador: " + jogador + "\n");
                        Console.ResetColor();
                        jogador.mostrarTopo();
                    }


                    Carta cartaAtual;

                    //Verificar se o baralho está vazio
                    if(baralho.Count != 0)
                    {
                        cartaAtual = baralho.Pop();
                    }
                    else
                    {
                        jogoAcabou = true;
                        break;
                    }

                    //Mostrar a carta atual
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nCarta atual: " + cartaAtual + "\n");
                    Console.ResetColor();
                    log.WriteLine($"Carta da rodada: {cartaAtual}\n");

                    bool existeACartaNaMesa = false;
                    bool existeACartaNoTopoDosJogadores = false;
                    bool existeACartaNoMonteDoJogador = false;

                    //Método para verificar se a carta atual está na mesa
                    foreach (Carta carta in cartasDaMesa)
                    {
                        if (carta.getValor() == cartaAtual.getValor())
                        {
                            existeACartaNaMesa = true;
                            break;
                        }

                    }

                    //Método para verificar se a carta atual está no topo do monte de algum jogador
                    for (int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        if (listaDeJogadores[i].getTopo() != null && cartaAtual.getValor() == listaDeJogadores[i].getTopo().getValor())
                        {
                            existeACartaNoTopoDosJogadores = true;
                            break;
                        }
                    }

                    //Método para verificar se a carta atual está no topo do monte do proprio jogador
                    if (cartaAtual == jogadorAtual.getTopo())
                    {
                        existeACartaNoMonteDoJogador = true;
                        break;
                    }

                    int opcao = 0;

                    if (existeACartaNaMesa || existeACartaNoTopoDosJogadores || existeACartaNoMonteDoJogador)
                    {
                        bool jogadaOk = false;

                        //Repetição até a jogada ser correta
                        while (!jogadaOk)
                        {
                            Console.WriteLine(new String('-', 40));
                            Console.WriteLine("Menu:");
                            Console.WriteLine("1 - Formar monte com carta na mesa");
                            Console.WriteLine("2 - Roubar monte de um jogador");
                            Console.WriteLine("3 - Adicionar carta no topo do próprio monte");
                            Console.Write("Digite a opção desejada: ");
                            opcao = int.Parse(Console.ReadLine());
                            log.WriteLine(new String('-', 40));
                            log.WriteLine("\nMenu:");
                            log.WriteLine("1 - Formar monte com carta na mesa");
                            log.WriteLine("2 - Roubar monte de um jogador");
                            log.WriteLine("3 - Adicionar carta no topo do próprio monte");
                            log.WriteLine($"Opção escolhida: {opcao}\n");

                            switch (opcao)
                            {
                                case 1:
                                    int opcaoCarta = EscolherOpcao(cartasDaMesa);

                                    Carta cartaDesejada = cartasDaMesa[opcaoCarta];

                                    if (cartaDesejada.getValor() == cartaAtual.getValor())
                                    {
                                        jogadorAtual.adicionarCartas(cartaDesejada, cartaAtual);
                                        cartasDaMesa.Remove(cartaDesejada);
                                        jogadaOk = true;
                                        log.WriteLine($"Jogador: {jogadorAtual} adicionou as cartas {cartaAtual}, {cartaDesejada} em seu monte\n");
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("A carta selecionada é diferente da carta atual");
                                        log.WriteLine("A carta selecionada é diferente da carta atual\n");
                                        Console.ResetColor();
                                    }
                                    break;

                                case 2:
                                    bool achouJogador = false;

                                    //Repetição até achar o jogador
                                    while (!achouJogador)
                                    {
                                        Console.Write("Digite o nome do jogador para roubar o monte: ");

                                        string nomeJogadorRoubo = Console.ReadLine();
                                        log.WriteLine($"Jogador escolhido: {nomeJogadorRoubo}\n");

                                        if (nomeJogadorRoubo.ToLower() != jogadorAtual.getNome().ToLower())
                                        {
                                            Jogador jogadorRoubo = listaDeJogadores.Find(j => j.getNome().ToLower() == nomeJogadorRoubo.ToLower());

                                            if (listaDeJogadores.Contains(jogadorRoubo))
                                            {
                                                achouJogador = true;
                                                if (jogadorRoubo.getTopo() != null)
                                                {
                                                    if (cartaAtual.getValor() == jogadorRoubo.getTopo().getValor())
                                                    {
                                                        jogadorAtual.adicionarMonte(jogadorRoubo.monteJogador);
                                                        jogadorAtual.adicionarCarta(cartaAtual);
                                                        jogadorRoubo.limparMonte();
                                                        jogadaOk = true;
                                                        log.WriteLine($"Jogador: {jogadorAtual} roubou o monte do jogador: {jogadorRoubo}\n");
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("A carta não é igual o topo do monte do jogador selecionado");
                                                        log.WriteLine("A carta não é igual o topo do monte do jogador selecionado\n");
                                                        Console.ResetColor();
                                                    }
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine($"O topo do jogador {jogadorRoubo} está vazio");
                                                    log.WriteLine($"O topo do jogador {jogadorRoubo} está vazio\n");
                                                    Console.ResetColor();

                                                }
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Jogador não encontrado, digite novamente");
                                                log.WriteLine("Jogador não encontrado, digite novamente\n");
                                                Console.ResetColor();
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Não é possível pegar o próprio monte");
                                            log.WriteLine("Não é possível pegar o próprio monte\n");
                                            Console.ResetColor();
                                        }
                                    }
                                    break;

                                case 3:
                                    if (jogadorAtual.getTopo() != null) 
                                    {
                                        if (cartaAtual.getValor() == jogadorAtual.getTopo().getValor())
                                        {
                                            jogadorAtual.adicionarCarta(cartaAtual);
                                            jogadaOk = true;
                                            log.WriteLine($"Jogador: {jogadorAtual} adicionou a carta: {cartaAtual} em seu monte\n");
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nA carta do topo é diferente da atual\n");
                                            log.WriteLine("\nA carta do topo é diferente da atual\n");
                                            Console.ResetColor();

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nO topo do monte está vazio\n");
                                        log.WriteLine("\nO topo do monte está vazio\n");
                                    }
                                    break;

                                //Caso a opção digitada não exista, ira entrar no default
                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Opção inválida");
                                    log.WriteLine("Opção inválida\n");
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
                        log.WriteLine($"\nJogador: {jogadorAtual} perdeu sua vez, pois com a carta: {cartaAtual} não foi possível fazer nenhuma jogada\n");
                    }
                }

                //Colocando o jogador atual no final da fila de jogadores
                filaDeJogadores.Enqueue(jogadorAtual);

                //Verificação se o baralho acabou, se sim, ele deve finalizar o jogo
                if (baralho.Count == 0)
                {
                    jogoAcabou = true;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nJogo acabou\n");
                    Console.ResetColor();

                    log.WriteLine("\nJogo acabou\n");
                    log.WriteLine("\nEstatísticas da partida:\n");

                    //Ordenando a lista de jogadores pela quantidade de cartas no monte
                    BubbleSort(listaDeJogadores);

                    Jogador jogadorCampeao = listaDeJogadores[0];

                    //Imprimindo as informações de cada jogador e verificando quem foi o campeão
                    for (int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        Console.WriteLine($"Jogador: {listaDeJogadores[i].getNome()} com {listaDeJogadores[i].monteJogador.Count} cartas no monte");

                        bool jaEsta = false;
                        foreach (Jogador lenda in lendasQueJaJogaram)
                        {
                            if (listaDeJogadores[i] == lenda)
                            {
                                jaEsta = true;
                            }
                        }

                        if (!jaEsta)
                        {
                            lendasQueJaJogaram.Add(listaDeJogadores[i]);
                        }

                        listaDeJogadores[i].setQuantidadeDeCartasNoMonte(listaDeJogadores[i].monteJogador.Count);
                    }

                    StreamWriter jogadores = new StreamWriter("jogadores.txt");

                    //Percorrendo a lista de jogadores e salvando nos arquivos as informações e setando posição e ranking nas ultimas 5 partidas
                    for (int i = 0; i < listaDeJogadores.Count; i++)
                    {
                        listaDeJogadores[i].setPosicao(i + 1);
                        listaDeJogadores[i].rankingUltimas5.Enqueue(i + 1);

                        jogadores.WriteLine(new string('=', 30));
                        jogadores.WriteLine($"Jogador: {listaDeJogadores[i].getNome()}");
                        jogadores.WriteLine($"Posição na última partida: {i + 1}");
                        jogadores.WriteLine($"Total de cartas na mão na ultima partida: {listaDeJogadores[i].getQuantidadeDeCartasNoMonte()}");

                        log.WriteLine(new string('=', 30));
                        log.WriteLine($"Jogador: {listaDeJogadores[i].getNome()}\n");
                        log.WriteLine($"Posição na última partida: {i + 1}\n");
                        log.WriteLine($"Total de cartas na mão na ultima partida: {listaDeJogadores[i].getQuantidadeDeCartasNoMonte()}\n");
                        log.WriteLine($"Ranking das últimas cinco partidas: {listaDeJogadores[i].ImprimirRanking()}\n");
                    }

                    //Mostrando no console as informações do jogador campeão
                    Console.WriteLine($"\nJogador Campeão: {jogadorCampeao}");
                    Console.WriteLine($"\nPosição: {jogadorCampeao.getPosicao()}");
                    Console.WriteLine($"\nQuantidade de cartas na mão: {jogadorCampeao.monteJogador.Count}");
                    Console.WriteLine("\nCartas no monte ordenadas:");
                    List<Carta> monteOrdenado = InsertionSort(jogadorCampeao.monteJogador);
                    foreach(Carta carta in monteOrdenado)
                    {
                        Console.WriteLine(carta);
                    }

                    //Salvando no log as informações do jogador campeão
                    log.WriteLine($"\nJogador Campeão: {jogadorCampeao}\n");
                    log.WriteLine($"\nPosição: {jogadorCampeao.getPosicao()}\n");
                    log.WriteLine($"\nQuantidade de cartas na mão: {jogadorCampeao.monteJogador.Count}\n");
                    log.WriteLine("\nCartas no monte ordenada:");
                    foreach (Carta carta in monteOrdenado)
                    {
                        log.WriteLine(carta);
                    }

                    //Fechando arquivos
                    jogadores.Close();
                    log.Close();
                }
            }
        }

        //Método para imprimir as cartas de uma determinada lista
        private void ImprimirListaDeCartas(List<Carta> lista)
        {
            foreach (Carta carta in lista)
            {
                Console.WriteLine(carta);
                log.WriteLine(carta);
            }
        }

        //Método para escolher a carta desejada (usado no switch case)
        private int EscolherOpcao(List<Carta> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"Opção {i + 1}: {lista[i]}");
                log.WriteLine($"Opção {i + 1}: {lista[i]}\n");
            }

            Console.Write("Digite a opção desejada: ");
            int opcao = int.Parse(Console.ReadLine());
            log.WriteLine($"Opção escolhida: {opcao}\n");

            while (opcao < 1 || opcao > lista.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção inválida");
                log.WriteLine("Opção inválida\n");
                Console.ResetColor();
                Console.Write("Digite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
                log.WriteLine($"Opção escolhida: {opcao}\n");
            }

            return opcao - 1;
        }

        //Método para gerar o baralho já embaralhado 
        private Stack<Carta> GerarBaralho(int quantidadeDeBaralhos)
        {

            Stack<Carta> pilhaDeCartas = new Stack<Carta>();

            string[] naipes = { "Paus", "Espadas", "Copas", "Ouros" };

            string[] valores = { "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            Random rnd = new Random();

            for (int i = 0; i < quantidadeDeBaralhos; i++)
            {
                int totalCartas = 0;

                Stack<Carta> pilhaTemp = new Stack<Carta>();

                while (totalCartas != 52)
                {
                    int randomNaipe = rnd.Next(0, 4);
                    int randomValor = rnd.Next(0, 13);


                    Carta carta = new Carta(valores[randomValor], naipes[randomNaipe]);

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
        private Queue<Jogador> GerarFilaDeJogadores(int quantidade, List<Jogador> lendasQueJaJogaram)
        {
            Queue<Jogador> fila = new Queue<Jogador>();
            bool jaJogou = false;

            Console.WriteLine(new String('-', 9) + "Definição de Jogadores" + new String('-', 9));

            for (int i = 0; i < quantidade; i++)
            {
                Console.Write($"Digite o nome do {i + 1}° jogador: ");
                string nome = Console.ReadLine();

                if(lendasQueJaJogaram.Count > 0)
                {
                    foreach(Jogador jogador in lendasQueJaJogaram)
                    {
                        if(jogador.getNome().ToLower() == nome.ToLower())
                        {
                            fila.Enqueue(jogador);
                            jaJogou = true;
                        }
                    }
                }
                if (!jaJogou)
                {
                    fila.Enqueue(new Jogador(nome));
                }
            }

            Console.WriteLine(new String('-', 40));

            return fila;
        }

        //Método para ordenar uma lista de jogadores de acordo com a quantidade de cartas no monte
        private void BubbleSort(List<Jogador> listaDeJogadores)
        {
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
        }

        //Método para ordenar as cartas
        private List<Carta> InsertionSort(Stack<Carta> pilha)
        {

            List<Carta> array = pilha.ToList();
            List<string> valores = new List<string> { "Às", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove", "Dez", "Dama", "Valete", "Rei" };

            int n = array.Count;
            for (int i = 1; i < n; ++i)
            {
                Carta key = array[i];
                int j = i - 1;
                while (j >= 0 && valores.IndexOf(array[j].getValor()) > valores.IndexOf(key.getValor()))
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }

            return array;
        }

        //Método para mostrar os jogadores que já jogaram
        public List<Jogador> getLendas()
        {
            return lendasQueJaJogaram;
        }
    }
}