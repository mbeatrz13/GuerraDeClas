using Raylib_cs;
using System;
using System.Numerics;
using jogo_poo.Entidades;

namespace jogo_poo
{
    class Program
    {
        // Fluxo de Estados do Jogo completo
        enum EstadoJogo 
        { 
            MenuInicial, 
            SelecaoPersonagem, 
            Capitulo1_Encapsulamento, 
            Capitulo2_Heranca, 
            Capitulo3_Polimorfismo, 
            Capitulo4_Abstracao, 
            Quiz, 
            VitoriaFinal 
        }

        static void Main(string[] args)
        {
            const int larguraTela = 800;
            const int alturaTela = 600;

            Raylib.InitWindow(larguraTela, alturaTela, "Guerra de Clãs: Eclipse do Sertão");
            Raylib.SetTargetFPS(60);

            EstadoJogo estadoAtual = EstadoJogo.MenuInicial;
            Sobrenatural jogador = null;

            // Variáveis de Combate
            Vector2 posicaoInimigo = new Vector2(600, 300);
            int vidaInimigo = 80;
            bool inimigoVivo = true;
            string nomeInimigo = "";

            // Variáveis do Quiz
            int capituloAtualNum = 1;
            string perguntaQuiz = "";
            string opcao1 = "";
            string opcao2 = "";
            int respostaCorreta = 1;
            string explicacaoQuiz = "";
            string mensagemFeedback = "";
            bool quizRespondido = false;

            while (!Raylib.WindowShouldClose())
            {
                // ==========================================
                // LÓGICA DO JOGO (UPDATE)
                // ==========================================
                switch (estadoAtual)
                {
                    case EstadoJogo.MenuInicial:
                        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                        {
                            estadoAtual = EstadoJogo.SelecaoPersonagem;
                        }
                        break;

                    case EstadoJogo.SelecaoPersonagem:
                        if (Raylib.IsKeyPressed(KeyboardKey.One))
                        {
                            jogador = new Vampiro("Dante Medeiros");
                            IniciarCapitulo1(out posicaoInimigo, out vidaInimigo, out inimigoVivo, out nomeInimigo);
                            estadoAtual = EstadoJogo.Capitulo1_Encapsulamento;
                        }
                        else if (Raylib.IsKeyPressed(KeyboardKey.Two))
                        {
                            jogador = new Lobisomem("Ícaro Bezerra");
                            IniciarCapitulo1(out posicaoInimigo, out vidaInimigo, out inimigoVivo, out nomeInimigo);
                            estadoAtual = EstadoJogo.Capitulo1_Encapsulamento;
                        }
                        break;

                    case EstadoJogo.Capitulo1_Encapsulamento:
                    case EstadoJogo.Capitulo2_Heranca:
                    case EstadoJogo.Capitulo3_Polimorfismo:
                    case EstadoJogo.Capitulo4_Abstracao:
                        AtualizarMovimentacaoECombate(jogador, ref posicaoInimigo, ref vidaInimigo, ref inimigoVivo, () =>
                        {
                            ConfigurarQuiz(estadoAtual, out capituloAtualNum, out perguntaQuiz, out opcao1, out opcao2, out respostaCorreta, out explicacaoQuiz);
                            quizRespondido = false;
                            mensagemFeedback = "";
                            estadoAtual = EstadoJogo.Quiz;
                        });
                        break;

                    case EstadoJogo.Quiz:
                        if (!quizRespondido)
                        {
                            if (Raylib.IsKeyPressed(KeyboardKey.One))
                            {
                                ProcessarRespostaQuiz(1, respostaCorreta, explicacaoQuiz, out mensagemFeedback);
                                quizRespondido = true;
                            }
                            else if (Raylib.IsKeyPressed(KeyboardKey.Two))
                            {
                                ProcessarRespostaQuiz(2, respostaCorreta, explicacaoQuiz, out mensagemFeedback);
                                quizRespondido = true;
                            }
                        }
                        else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                        {
                            if (capituloAtualNum == 1)
                            {
                                IniciarCapitulo2(out posicaoInimigo, out vidaInimigo, out inimigoVivo, out nomeInimigo, jogador);
                                estadoAtual = EstadoJogo.Capitulo2_Heranca;
                            }
                            else if (capituloAtualNum == 2)
                            {
                                IniciarCapitulo3(out posicaoInimigo, out vidaInimigo, out inimigoVivo, out nomeInimigo, jogador);
                                estadoAtual = EstadoJogo.Capitulo3_Polimorfismo;
                            }
                            else if (capituloAtualNum == 3)
                            {
                                IniciarCapitulo4(out posicaoInimigo, out vidaInimigo, out inimigoVivo, out nomeInimigo, jogador);
                                estadoAtual = EstadoJogo.Capitulo4_Abstracao;
                            }
                            else if (capituloAtualNum == 4)
                            {
                                estadoAtual = EstadoJogo.VitoriaFinal;
                            }
                        }
                        break;

                    case EstadoJogo.VitoriaFinal:
                        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                        {
                            estadoAtual = EstadoJogo.MenuInicial;
                        }
                        break;
                }

                // ==========================================
                // RENDERIZAÇÃO (DRAW)
                // ==========================================
                Raylib.BeginDrawing();
                Raylib.ClearBackground(new Color(15, 20, 30, 255));

                switch (estadoAtual)
                {
                    case EstadoJogo.MenuInicial:
                        DesenharMenuInicial();
                        break;

                    case EstadoJogo.SelecaoPersonagem:
                        DesenharSelecaoPersonagem();
                        break;

                    case EstadoJogo.Capitulo1_Encapsulamento:
                        DesenharFase("CAPÍTULO I: SOMBRAS NA FORTALEZA DOS REIS MAGOS", "Terras de Guaraíras | Pilar: Encapsulamento", jogador, posicaoInimigo, vidaInimigo, inimigoVivo, nomeInimigo, Color.DarkBlue);
                        break;

                    case EstadoJogo.Capitulo2_Heranca:
                        DesenharFase("CAPÍTULO II: RASTROS NAS DUNAS", "Dunas de Genipabu | Pilar: Herança", jogador, posicaoInimigo, vidaInimigo, inimigoVivo, nomeInimigo, new Color(40, 35, 20, 255));
                        break;

                    case EstadoJogo.Capitulo3_Polimorfismo:
                        DesenharFase("CAPÍTULO III: VOZES DA JUREMA", "Domínio de Ibirapi | Pilar: Polimorfismo", jogador, posicaoInimigo, vidaInimigo, inimigoVivo, nomeInimigo, new Color(15, 35, 20, 255));
                        break;

                    case EstadoJogo.Capitulo4_Abstracao:
                        DesenharFase("CAPÍTULO IV: ECLIPSE DO SERTÃO", "Ermos do Seridó | Chefe Final: Silas Montenegro", jogador, posicaoInimigo, vidaInimigo, inimigoVivo, nomeInimigo, new Color(40, 15, 25, 255));
                        break;

                    case EstadoJogo.Quiz:
                        DesenharQuiz(capituloAtualNum, perguntaQuiz, opcao1, opcao2, quizRespondido, mensagemFeedback);
                        break;

                    case EstadoJogo.VitoriaFinal:
                        DesenharTelaVitoria();
                        break;
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================
        static void IniciarCapitulo1(out Vector2 posInimigo, out int hpInimigo, out bool vivo, out string nome)
        {
            posInimigo = new Vector2(600, 300);
            hpInimigo = 60;
            vivo = true;
            nome = "Vampiro Rival (Lucian)";
        }

        static void IniciarCapitulo2(out Vector2 posInimigo, out int hpInimigo, out bool vivo, out string nome, Sobrenatural jogador)
        {
            jogador.Posicao = new Vector2(100, 300);
            posInimigo = new Vector2(600, 300);
            hpInimigo = 90;
            vivo = true;
            nome = "Lobisomem Rival (Ares)";
        }

        static void IniciarCapitulo3(out Vector2 posInimigo, out int hpInimigo, out bool vivo, out string nome, Sobrenatural jogador)
        {
            jogador.Posicao = new Vector2(100, 300);
            posInimigo = new Vector2(600, 300);
            hpInimigo = 120;
            vivo = true;
            nome = "Infiltrado da Névoa";
        }

        static void IniciarCapitulo4(out Vector2 posInimigo, out int hpInimigo, out bool vivo, out string nome, Sobrenatural jogador)
        {
            jogador.Posicao = new Vector2(100, 300);
            posInimigo = new Vector2(600, 300);
            hpInimigo = 180;
            vivo = true;
            nome = "CHEFE: Silas Montenegro";
        }

        static void AtualizarMovimentacaoECombate(Sobrenatural jogador, ref Vector2 posicaoInimigo, ref int vidaInimigo, ref bool inimigoVivo, Action aoDerrotarInimigo)
        {
            float velocidade = 3.5f;
            if (Raylib.IsKeyDown(KeyboardKey.W)) jogador.Posicao.Y -= velocidade;
            if (Raylib.IsKeyDown(KeyboardKey.S)) jogador.Posicao.Y += velocidade;
            if (Raylib.IsKeyDown(KeyboardKey.A)) jogador.Posicao.X -= velocidade;
            if (Raylib.IsKeyDown(KeyboardKey.D)) jogador.Posicao.X += velocidade;

            if (Raylib.IsKeyPressed(KeyboardKey.E))
            {
                jogador.UsarHabilidade();
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space) && inimigoVivo)
            {
                float distancia = Vector2.Distance(jogador.Posicao, posicaoInimigo);
                if (distancia < 65)
                {
                    vidaInimigo -= 30;
                    jogador.ReceberDano(8);
                    if (vidaInimigo <= 0)
                    {
                        inimigoVivo = false;
                        aoDerrotarInimigo();
                    }
                }
            }
        }

        static void ConfigurarQuiz(EstadoJogo estado, out int capNum, out string pergunta, out string op1, out string op2, out int correta, out string explicacao)
        {
            switch (estado)
            {
                case EstadoJogo.Capitulo1_Encapsulamento:
                    capNum = 1;
                    pergunta = "Atributos privados como Sede e Furia nao podem ser alterados diretamente por outras partes do jogo.\nQual pilar de POO garante esse controle de acesso aos dados?";
                    op1 = "[1] Encapsulamento";
                    op2 = "[2] Polimorfismo";
                    correta = 1;
                    explicacao = "O Encapsulamento protege o estado interno do objeto exigindo métodos como ReceberDano().";
                    break;

                case EstadoJogo.Capitulo2_Heranca:
                    capNum = 2;
                    pergunta = "As classes Vampiro e Lobisomem derivam características comuns da classe base Sobrenatural.\nQual pilar de POO permite reaproveitar codigo atraves dessa hierarquia?";
                    op1 = "[1] Abstracao";
                    op2 = "[2] Heranca";
                    correta = 2;
                    explicacao = "A Herança estabelece uma relação 'é-um', onde subclasses herdam atributos e métodos da superclasse.";
                    break;

                case EstadoJogo.Capitulo3_Polimorfismo:
                    capNum = 3;
                    pergunta = "Vampiro e Lobisomem possuem o mesmo comando UsarHabilidade(), mas executam Hipnose e Investida.\nQual pilar permite que um mesmo metodo tenha comportamentos diferentes?";
                    op1 = "[1] Polimorfismo";
                    op2 = "[2] Encapsulamento";
                    correta = 1;
                    explicacao = "O Polimorfismo permite a sobrescrita (override) de um método em subclasses diferentes.";
                    break;

                default: // Capítulo 4
                    capNum = 4;
                    pergunta = "A classe Sobrenatural define a ideia essencial de um personagem sem poder ser instanciada diretamente.\nQual pilar representa esse modelo abstrato?";
                    op1 = "[1] Polimorfismo";
                    op2 = "[2] Abstracao";
                    correta = 2;
                    explicacao = "A Abstração foca no essencial, ocultando os detalhes complexos através de classes abstratas.";
                    break;
            }
        }

        static void ProcessarRespostaQuiz(int escolha, int correta, string explicacao, out string feedback)
        {
            if (escolha == correta)
            {
                feedback = $"CORRETO! {explicacao}";
            }
            else
            {
                feedback = $"INCORRETO! A resposta certa era a opcao [{correta}]. {explicacao}";
            }
        }

        static void DesenharMenuInicial()
        {
            Raylib.DrawText("GUERRA DE CLÃS", 230, 180, 40, Color.Gold);
            Raylib.DrawText("ECLIPSE DO SERTÃO", 225, 230, 36, Color.Red);
            Raylib.DrawText("Vampiros e Lobisomens no Brasil", 250, 280, 20, Color.LightGray);
            Raylib.DrawRectangle(220, 380, 360, 50, new Color(30, 40, 60, 255));
            Raylib.DrawText("Pressione [ENTER] para Iniciar", 240, 395, 20, Color.Yellow);
        }

        static void DesenharSelecaoPersonagem()
        {
            Raylib.DrawText("ESCOLHA SEU PERSONAGEM", 230, 80, 26, Color.Gold);

            Raylib.DrawRectangle(100, 160, 270, 320, new Color(40, 20, 30, 255));
            Raylib.DrawRectangleLines(100, 160, 270, 320, Color.Red);
            Raylib.DrawText("[1] DANTE MEDEIROS", 120, 180, 20, Color.Red);
            Raylib.DrawText("Classe: Vampiro", 120, 220, 16, Color.White);
            Raylib.DrawText("Recurso: Sede (65/100)", 120, 260, 16, Color.Magenta);

            Raylib.DrawRectangle(430, 160, 270, 320, new Color(20, 30, 50, 255));
            Raylib.DrawRectangleLines(430, 160, 270, 320, Color.Blue);
            Raylib.DrawText("[2] ÍCARO BEZERRA", 450, 180, 20, Color.SkyBlue);
            Raylib.DrawText("Classe: Lobisomem", 450, 220, 16, Color.White);
            Raylib.DrawText("Recurso: Fúria (40/100)", 450, 260, 16, Color.Magenta);

            Raylib.DrawText("Pressione [1] para Vampiro ou [2] para Lobisomem", 180, 520, 18, Color.Yellow);
        }

        static void DesenharFase(string titulo, string subtitulo, Sobrenatural jogador, Vector2 posInimigo, int hpInimigo, bool vivo, string nomeInimigo, Color corFundo)
        {
            Raylib.DrawRectangle(0, 0, 800, 65, corFundo);
            Raylib.DrawText(titulo, 20, 15, 20, Color.Gold);
            Raylib.DrawText(subtitulo, 20, 40, 14, Color.LightGray);

            Color corJogador = jogador is Vampiro ? Color.Red : Color.Blue;
            Raylib.DrawRectangleV(jogador.Posicao, new Vector2(40, 40), corJogador);
            Raylib.DrawText(jogador.Nome, (int)jogador.Posicao.X - 10, (int)jogador.Posicao.Y - 20, 14, Color.White);

            if (vivo)
            {
                Raylib.DrawRectangleV(posInimigo, new Vector2(40, 40), Color.DarkGray);
                Raylib.DrawText($"{nomeInimigo} (HP: {hpInimigo})", (int)posInimigo.X - 20, (int)posInimigo.Y - 20, 14, Color.Orange);
            }

            Raylib.DrawRectangle(10, 470, 780, 120, new Color(0, 0, 0, 220));
            Raylib.DrawText($"Vida: {jogador.Vida}", 30, 485, 18, Color.Green);
            
            string recurso = jogador is Vampiro ? "Sede" : "Fúria";
            Raylib.DrawText($"{recurso}: {jogador.RecursoEspecial}/100", 30, 510, 18, Color.Magenta);
            Raylib.DrawText("CONTROLES: [W,A,S,D] Mover | [ESPAÇO] Atacar | [E] Usar Habilidade", 30, 550, 14, Color.Yellow);
        }

        static void DesenharQuiz(int capNum, string pergunta, string op1, string op2, bool respondido, string feedback)
        {
            Raylib.DrawRectangle(50, 50, 700, 500, new Color(20, 25, 40, 245));
            Raylib.DrawRectangleLines(50, 50, 700, 500, Color.Gold);

            Raylib.DrawText($"DESAFIO DE POO - CAPÍTULO {capNum}", 80, 80, 22, Color.Gold);
            Raylib.DrawText(pergunta, 80, 130, 15, Color.White);

            Raylib.DrawText(op1, 100, 250, 18, Color.Green);
            Raylib.DrawText(op2, 100, 290, 18, Color.SkyBlue);

            if (respondido)
            {
                Raylib.DrawText(feedback, 80, 360, 15, Color.Yellow);
                Raylib.DrawText("Pressione [ENTER] para avançar ao próximo capítulo...", 80, 440, 16, Color.White);
            }
        }

        static void DesenharTelaVitoria()
        {
            Raylib.DrawRectangle(50, 50, 700, 500, new Color(15, 30, 20, 250));
            Raylib.DrawRectangleLines(50, 50, 700, 500, Color.Green);

            Raylib.DrawText("PARABÉNS! VOCÊ ZEROU O JOGO!", 180, 100, 26, Color.Gold);
            Raylib.DrawText("Silas Montenegro foi derrotado e o Pacto do Potengi foi restaurado!", 100, 150, 15, Color.White);

            Raylib.DrawText("CONCEITOS APRENDIDOS:", 100, 210, 18, Color.Yellow);
            Raylib.DrawText("[X] ENCAPSULAMENTO  - Atributos privados e métodos seguros", 120, 250, 16, Color.White);
            Raylib.DrawText("[X] HERANÇA         - Reutilização da classe Sobrenatural", 120, 280, 16, Color.White);
            Raylib.DrawText("[X] POLIMORFISMO    - Sobrescrita de Habilidades únicas", 120, 310, 16, Color.White);
            Raylib.DrawText("[X] ABSTRAÇÃO       - Modelagem da classe base genérica", 120, 340, 16, Color.White);

            Raylib.DrawText("Pressione [ENTER] para reiniciar o jogo.", 220, 440, 18, Color.Yellow);
        }
    }
}