    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Projecto
    {
        public class corpoCeleste
        {
                           // Modificador protected permite acesso pelas subclasses
                           protected string n;
                           protected double m;
                           protected double r;
                            //Construtor padrão
                            public corpoCeleste() { }
                            //Para mudar o valor da variavel usa-se o metodo set
                            public virtual /*Virtual indica que estamos a lidar com polimorfismo*/ void SetDados(string nome, double massa, double raio)
                            {
                                this.n = nome;
                                this.m = massa;
                                this.r = raio;
                            }
                            public virtual void exibir_informações()
                            {
                                Console.WriteLine($"   -Corpo Celeste: {n}");
                                Console.WriteLine($"   - Massa: {m} kg");
                                Console.WriteLine($"   - Raio: {r} m");
                            }
                            // Método para obter a massa
                            public double getmassa()
                            {
                                return m;
                            }
                            // Método para obter o nome
                            public string getnome() 
                            {
                                return n;
                            }
        }
        public class planeta : corpoCeleste
        {
            //Construtor Padrão
            public planeta() { }
            public override void SetDados(string nome, double massa, double raio)
            {
                base.SetDados(nome, massa, raio);
            }
            //Calculando a gravidade
            public double GetGravidade()
            {
                double G = 6.674e-11;
                return (G * m) / (Math.Pow(r, 2) / 1000);
            }
            public override void exibir_informações()
            {
                base.exibir_informações();
                //F2 indica que conciderará apenas 2 casas decimal
                Console.WriteLine($"   - Gravidade: {GetGravidade():F2} m/s²");
            }

        }
        public class asteroide : corpoCeleste
        {
            //Construtor padrão
            public asteroide() { }
            private double semiEixoMaior;
            private double excentricidade;
            private planeta orbitaEmTornoDe;
        public asteroide(string nome, double massa, double raio, double semiEixoMaior, double excentricidade)
        {
            // Chama o método da classe base para definir os dados básicos do asteroide
            SetDados(nome, massa, raio);
            this.semiEixoMaior = semiEixoMaior;
            this.excentricidade = excentricidade;
        }
        public double GetPeriodoOrbital()
            {
                if (orbitaEmTornoDe == null)
                {
                //o "null" indica o vazio. Significa que se aquela variavel receber nada ele imprimira o texto a baixo
                //O comando throw em C# é usado para lançar exceções. Isso significa que o programa para a execução normal e sinaliza que ocorreu um erro.
                throw new InvalidOperationException("O asteroide não está orbitando nenhum planeta!");
                }

                double G = 6.674e-11;
                double M = orbitaEmTornoDe.getmassa();
                return 2 * Math.PI * Math.Sqrt(Math.Pow(semiEixoMaior, 3) / (G * M));
            }
            public override void exibir_informações()
            {
                base.exibir_informações();
                Console.WriteLine($"   - Orbita ao redor de: {(orbitaEmTornoDe != null ? orbitaEmTornoDe.getnome() : "Desconhecido")}");
                Console.WriteLine($"   - Semi-eixo maior: {semiEixoMaior:F0} m");
                Console.WriteLine($"   - Excentricidade: {excentricidade:F2}");

                if (orbitaEmTornoDe != null)
                {
                //TEmpo orbital em segundos!

                    Console.WriteLine($"   - Período orbital: {GetPeriodoOrbital() / (60 * 60 * 24):F2} dias");
                }


            }
            // Método para definir a órbita do asteroide
            public void SetOrbita(planeta Planeta)
            {
                this.orbitaEmTornoDe = Planeta;
            }
         
     }
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Qual é o nome do planeta?");
                string np = Console.ReadLine();

                Console.WriteLine("Qual a massa do planeta?[kg]");
                double mp = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Qual o raio do planeta?[m]");
                double rp = Convert.ToDouble(Console.ReadLine()) * 1000;

                //Instancia da classe planeta
                planeta planeta = new planeta();
                //Temos que colocar o nome da terra, massa e raio. O "e" representa uma notação cientifica"10^numero depois do e"
                planeta.SetDados(np, mp, rp);
                //Exibir as informações:
                Console.WriteLine("Informações do planeta:");
                planeta.exibir_informações();

                Console.WriteLine("Qual é o nome do asteroide");
                string na = Console.ReadLine(); 

                Console.WriteLine("Qual é a massa da estrela[kg]?");
                double ma = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Qual é o raio do asteroide?");
                double ra = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Digite o semi-eixo maior do asteroide.");
                double sem = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Digite a excentricidade do asteroide.");
                double exc = Convert.ToDouble(Console.ReadLine());

                //Criar instabcia da classe asteroide
                asteroide asteroides = new asteroide();
                asteroide ceres = new asteroide(na, ma, ra, sem, exc);
                //Definir em quanl planeta o asteroide orbita:
                ceres.SetOrbita(planeta);
                //Exibindo as informações do asteroide 
                Console.WriteLine("Informações do asteroide");
                ceres.exibir_informações();
                Console.ReadKey();

            }
        }
    }
