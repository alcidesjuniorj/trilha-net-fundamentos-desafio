using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            string placa = string.Empty;
            while(placa.Trim().Length == 0)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                placa = Console.ReadLine();
                if(ValidarPlaca(placa))
                {
                    if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
                    {
                        Console.WriteLine($"Placa: {placa} já foi inclusa no sistema!");
                    }
                    else
                    {
                        veiculos.Add(placa.ToUpper());
                    }
                }
                else
                {
                    Console.WriteLine($"Placa: {placa} inválida!");
                    placa = string.Empty;
                }           
            };
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = "";
            placa = Console.ReadLine();

            if (ValidarPlaca(placa))
            {
                if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    int horas = -1;
                    while (horas < 0)
                    {
                        string horaInformada = Console.ReadLine();
                        int.TryParse(horaInformada, out horas);
                        
                        if(horas < 0)
                        {
                            Console.WriteLine($"Valor informado inválido! \n Informe um valor numérico positivo.");
                        }
                    }

                    decimal valorTotal = 0;
                    valorTotal = precoInicial + (precoPorHora * horas); 

                    veiculos.Remove(placa.ToUpper());

                    Console.WriteLine($"O veículo {placa.ToUpper()} foi removido e o preço total foi de: R$ {valorTotal.ToString("0.00")}");
                }
                else
                {
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                }
            }
            else
            {
                Console.WriteLine($"Placa: {placa} inválida!");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var item in veiculos)
                {
                    Console.WriteLine($"{item}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa)) 
            { 
                return false; 
            }

            if (placa.Length <= 4 || placa.Length > 8) 
            { 
                return false; 
            }

            placa = placa.Replace("-", "").Trim();
            
            if (char.IsLetter(placa, 4))
            {
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            }
            else
            {
                var padraoNacional = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNacional.IsMatch(placa);
            }
        }
    
    }
}
