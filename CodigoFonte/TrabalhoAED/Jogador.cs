using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoAED
{
    class Jogador
    {
        private string nome { get; set; }
        private int posicao { get; set; }  
        private int numeroDeCartas{ get; set; }

        Queue<int> rankingUltimas5 = new Queue<int>(5);

        //implementar fila contendo ranking do jogador nas ultimas 5 partidas

        public Jogador(string nome) 
        {
            this.nome = nome;
        }

    }
}
