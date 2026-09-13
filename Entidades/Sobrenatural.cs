using System;
using System.Numerics;

namespace jogo_poo.Entidades
{
    // Pilar ABSTRAÇÃO: Classe abstrata base para criaturas do jogo[cite: 1]
    public abstract class Sobrenatural
    {
        public string Nome { get; protected set; }
        public Vector2 Posicao;
        
        // Pilar ENCAPSULAMENTO: Atributos privados alterados por métodos seguros[cite: 1]
        private int vida;
        private int vidaMaxima;
        private int recursoEspecial;

        public int Vida => vida;
        public int RecursoEspecial => recursoEspecial;

        public Sobrenatural(string nome, int vidaMaxima, int recursoInicial)
        {
            Nome = nome;
            this.vidaMaxima = vidaMaxima;
            this.vida = vidaMaxima;
            this.recursoEspecial = recursoInicial;
        }

        public void ReceberDano(int quantidade)
        {
            vida = Math.Max(0, vida - quantidade);
        }

        protected void ModificarRecurso(int delta)
        {
            recursoEspecial = Math.Clamp(recursoEspecial + delta, 0, 100);
        }

        // Pilar POLIMORFISMO: Cada criatura executa sua própria habilidade[cite: 1]
        public abstract void UsarHabilidade();
    }
}