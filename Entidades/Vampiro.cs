using System;

namespace jogo_poo.Entidades
{
    // Pilar HERANÇA: Vampiro herda de Sobrenatural[cite: 1]
    public class Vampiro : Sobrenatural
    {
        public Vampiro(string nome) : base(nome, 120, 65) { }

        // Pilar POLIMORFISMO: Implementação específica para Vampiros[cite: 1]
        public override void UsarHabilidade()
        {
            if (RecursoEspecial >= 20)
            {
                ModificarRecurso(-20);
                Console.WriteLine($"{Nome} usou Hipnose / Velocidade!");
            }
        }
    }
}