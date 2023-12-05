using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new String('-', 13) + "Inicio do jogo" + new String('-', 13));

            Console.Write("Digite a quantidade de jogadores: ");
            int quantidadeDeJogadores = int.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de baralhos que vão ser usados no jogo: ");
            int quantidadeDeBaralhos = int.Parse(Console.ReadLine());

            Console.WriteLine(new String('-', 40));

            Jogo jogo = new Jogo(quantidadeDeJogadores, quantidadeDeBaralhos);

            jogo.Jogar();
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
