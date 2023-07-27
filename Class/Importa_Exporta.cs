using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Class
{
    public class Importa_Exporta
    {
        public CrudEmpresa crudEmpresa;
        public CrudFuncionario crudFuncionario;
        public CrudMarcacao crudMarcacao;
        public Importa_Exporta importa_Exporta;
        public Importa_Exporta(Importa_Exporta newImporta_Exporta)
        {
            crudEmpresa = new CrudEmpresa(importa_Exporta);
            crudFuncionario = new CrudFuncionario(importa_Exporta);
            crudMarcacao = new CrudMarcacao(importa_Exporta);
            importa_Exporta = newImporta_Exporta;
        }

        public void ExportarDados()
        {
            var diretorio = Diretorio() + "\\";
            string nomeArquivo = $"Base_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")}.txt";

            var caminhoCompleto = diretorio + nomeArquivo;

            File.Create(caminhoCompleto).Close();

            using (StreamWriter escreva = new(caminhoCompleto))
            {

                if (crudEmpresa.listaEmpresa.Count != 0)
                {
                    foreach (var empresa in crudEmpresa.listaEmpresa)
                    {
                        string linha = $"100;{empresa.Id};{empresa.Nome};{empresa.Cnpj};{empresa.Senha}";
                        escreva.WriteLine(linha);
                    }
                }

                if (crudFuncionario.listaFuncionario.Count != 0)
                {
                    foreach (var Funcionario in crudFuncionario.listaFuncionario)
                    {
                        string linha = $"200;{Funcionario.Id};{Funcionario.Nome};{Funcionario.Cpf};{Funcionario.Senha};{Funcionario.IdEmpresa.Id}";
                        escreva.WriteLine(linha);
                    }
                }

                if (crudMarcacao.listaMarcacao.Count != 0)
                {
                    foreach (var marcacao in crudMarcacao.listaMarcacao)
                    {
                        string linha = $"300;{marcacao.Id};{marcacao.marcacao};{marcacao.IdFuncionario.Id};{marcacao.IdEmpresa.Id}";
                        escreva.WriteLine(linha);
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Obrigado por usar nosso sistema");
            Thread.Sleep(3000);
            Environment.Exit(0);

        }

        public void ImportarDados()
        {
            var arquivo = ObterArquivoMaisRecente();
            if (arquivo != null)
            {
                using (StreamReader ler = new(arquivo))
                {
                    while (!ler.EndOfStream)
                    {
                        string linha = ler.ReadLine();
                        if (linha != null)
                        {
                            string[] campos = linha.Split(';');

                            if (campos.Length > 2)
                            {
                                if (Convert.ToInt16(campos[0]) == 100 && campos.Length == 5)
                                {
                                    crudEmpresa.listaEmpresa.Add(new Empresa
                                    {
                                        Id = int.Parse(campos[1]),
                                        Nome = campos[2],
                                        Cnpj = campos[3],
                                        Senha = campos[4]
                                    });

                                }

                                else if (Convert.ToInt16(campos[0]) == 200 && campos.Length == 6)
                                {
                                    var empresa = crudEmpresa.BuscaEmpresaId(int.Parse(campos[5]));
                                    if (empresa != null)
                                    {
                                        crudFuncionario.listaFuncionario.Add(new Funcionario
                                        {
                                            Id = int.Parse(campos[1]),
                                            Nome = campos[2],
                                            Cpf = campos[3],
                                            Senha = campos[4],
                                            IdEmpresa = empresa
                                        });
                                    }
                                    // else { Colocar log do numero da linha que não importou}
                                }

                                else if (Convert.ToInt16(campos[0]) == 300 && campos.Length == 5)
                                {
                                    var funcionario = crudFuncionario.BuscaFuncionarioId(int.Parse(campos[3]));
                                    var empresa = crudEmpresa.BuscaEmpresaId(int.Parse(campos[4]));

                                    if (empresa != null || funcionario != null)
                                    {
                                        crudMarcacao.listaMarcacao.Add(new Marcacao
                                        {
                                            Id = int.Parse(campos[1]),
                                            marcacao = DateTime.Parse(campos[2]),
                                            IdFuncionario = funcionario,
                                            IdEmpresa = empresa
                                        });
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(3000);
                    Console.WriteLine("Dados Importados");
                }
            }            
            else 
            {
                Thread.Sleep(3000);
                Console.WriteLine("Nenhum arquivo Encontrado");
            }
        }

        private static string ObterArquivoMaisRecente()
        {
            var diretorio = Diretorio();
            DirectoryInfo pasta = new DirectoryInfo(diretorio);
            FileInfo[] arquivos = pasta.GetFiles("Base_*.txt");

            if (arquivos.Length == 0)
                return null;

            // Ordenar os arquivos pelo nome (que inclui a data e hora)
            FileInfo arquivoMaisRecente = arquivos.OrderByDescending(f => f.Name).First();

            return arquivoMaisRecente.FullName;
        }

        private static string Diretorio()
        {
            string caminhoExecutavel = Assembly.GetExecutingAssembly().Location;
            string caminhoPastaExecutavel = Path.GetDirectoryName(caminhoExecutavel);

            string caminhoPastaSolucao = Directory.GetParent(caminhoPastaExecutavel).Parent?.Parent?.Parent?.FullName;

            return caminhoPastaSolucao + "\\Dados";
        }

    }


}
