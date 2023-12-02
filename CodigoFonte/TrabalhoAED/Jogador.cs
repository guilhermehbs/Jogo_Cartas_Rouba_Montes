﻿using System;
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
        private int numeroDeCartasNaMao;

        public Stack<Carta> monteJogador;


        Queue<int> rankingUltimas5;

        //implementar fila contendo ranking do jogador nas ultimas 5 partidas

        //Construtor para o jogador
        public Jogador(string nome) 
        {
            this.nome = nome;
            this.rankingUltimas5 = new Queue<int>(5);
            this.monteJogador = new Stack<Carta>();
        }

        public void adicionarCartaNoMonte(Carta carta)
        {
            monteJogador.Push(carta);
        }

        public void adicionarCartas(Carta carta1, Carta carta2)
        {
            monteJogador.Push(carta1);
            monteJogador.Push(carta2);
        }

        public void adicionarMonte(Stack<Carta> monte)
        {
                monteJogador.Push(monte.Pop());
        }

        public void limparMonte()
        {
            monteJogador.Clear();
        }

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

        public string getNome()
        {
            return nome;
        }

        public override string ToString()
        {
            return nome;
        }
    }
}
