using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Program
    {
        static List<Jogador> lendasQueJaJogaram;

        static void Main(string[] args)
        {

            lendasQueJaJogaram = new List<Jogador>();
      
            bool sairDoJogo = false;

            while (!sairDoJogo)
            {
                string escolha = Menu();

                switch (escolha)
                {
                    case "1":
                        IniciarPartida();
                    break;

                    case "2":
                        Console.Write("Digite o nome do jogador: ");
                        string nome = Console.ReadLine();

                        ProcurarPosicaoJogador(nome);
                        break;

                    case "3":
                        sairDoJogo = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opção inválida");
                        Console.ResetColor();
                        break;
                }          
            }
        }

        //Método para inicializar a partida
        static void IniciarPartida()
        {
            Console.WriteLine(new String('-', 13) + "Inicio do jogo" + new String('-', 13));

            Console.Write("Digite a quantidade de jogadores: ");
            int quantidadeDeJogadores = int.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de baralhos que vão ser usados no jogo: ");
            int quantidadeDeBaralhos = int.Parse(Console.ReadLine());

            Console.WriteLine(new String('-', 40));

            Jogo jogo = new Jogo(quantidadeDeJogadores, quantidadeDeBaralhos, lendasQueJaJogaram);

            jogo.Jogar();
            lendasQueJaJogaram = jogo.getLendas();
        }

        //Método para mostrar o menu no console
        static string Menu()
        {
            Console.WriteLine(new String('-', 18) + "MENU" + new String('-', 18));
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Acessar histórico");
            Console.WriteLine("3 - Sair");
            Console.Write("Digite a opção desejada: ");
            string opcao = Console.ReadLine();
            Console.WriteLine(new String('-', 40));

            return opcao;
        }

        //Método para procurar e imprimir ranking da ultimas 5 partidas dos jogadores
        static void ProcurarPosicaoJogador(string nome)
        {
            if(lendasQueJaJogaram.Count == 0)
            {
                Console.WriteLine("Niguém jogou ainda");
            }
            else
            {
                bool achouOJogador = false;

                foreach(Jogador jogador in lendasQueJaJogaram)
                {
                    if(jogador.getNome().ToLower() == nome.ToLower())
                    {
                        jogador.MostrarRaking();
                        achouOJogador = true;
                    }
                }

                if (!achouOJogador)
                {
                    Console.WriteLine("O jogador nunca jogou");
                }
            }
        }

        //static void CriarCarta()
        //{
        //    Console.WriteLine(new String('*', 10));
        //    Console.WriteLine("*" + " 4" + new String(' ', 6) + "*");
        //    Console.WriteLine("*" + " " + new String('*', 6) + " " + "*");
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Console.WriteLine("*" + " " + "*" + new String(' ', 4) + "*" + " " + "*");

        //    }
        //    Console.WriteLine("*" + " " + new String('*', 6) + " " + "*");
        //    Console.WriteLine("*" + new String(' ', 6) + "4 " + "*");
        //    Console.WriteLine(new String('*', 10));
        //}
    }
}
