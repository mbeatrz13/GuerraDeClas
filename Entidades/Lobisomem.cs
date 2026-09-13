using System;

namespace jogo_poo.Entidades
{
    // Pilar HERANÇA: Lobisomem herda de Sobrenatural[cite: 1]
    public class Lobisomem : Sobrenatural
    {
        public Lobisomem(string nome) : base(nome, 140, 40) { }

        // Pilar POLIMORFISMO: Implementação específica para Lobisomens[cite: 1]
        public override void UsarHabilidade()
        {
            if (RecursoEspecial >= 15)
            {
                ModificarRecurso(-15);
                Console.WriteLine($"{Nome} usou Investida!");
            }
        }
    }
}