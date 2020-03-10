using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using Api2.Models;

namespace Api2.Controllers
{
    public class CalculaJurosController
    {

        private const string path = "https://alanricardorochaapi1.azurewebsites.net/";

        [HttpGet("calculaJuros")]
        public decimal CalculaJuros(ParamCalculoJuros paramCalculoJuros)
        {
            //Fórmula: Valor Final = Valor Inicial * (1 + juros) ^ Tempo
            var resultado = paramCalculoJuros.ValorInicial * (decimal)Math.Pow(1 + (double)RetornaTaxaJuros(), paramCalculoJuros.Meses);
            return TruncaDecimal(resultado, 2);
        }

        //Função responsável por consumir a API /taxasJuros
        private decimal RetornaTaxaJuros()
        {
            decimal taxaJuros = 0;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{path}/taxaJuros").Result;
                string conteudo = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(conteudo))
                {
                    //taxaJuros = Convert.ToDecimal(conteudo.Replace(".", ",")); //nos testes locais eu precisei substituir o ponto pela vírgula, porém na azure não é preciso essa substituição
                    taxaJuros = Convert.ToDecimal(conteudo);
                }
            }
            return taxaJuros;
        }

        private decimal TruncaDecimal(decimal valorParam, int qtdCasasDecimais)
        {
            decimal valor1 = (decimal)Math.Pow(10, qtdCasasDecimais);
            decimal valor2 = Math.Truncate(valor1 * valorParam);
            return valor2 / valor1;
        }
    }
}
