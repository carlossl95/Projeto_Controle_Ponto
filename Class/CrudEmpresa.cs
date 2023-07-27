using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Class
{

    public class CrudEmpresa
    {
        public List<Empresa> listaEmpresa { get; set; }

        public Importa_Exporta importa_exporta;
        public Empresa empresa;
        public CrudEmpresa(Importa_Exporta newImporta_exporta)
        {
            empresa = new Empresa();
            importa_exporta = newImporta_exporta;
            listaEmpresa = new List<Empresa>();
        }       


        public void AddEmpresa(Empresa empresa)
        {
            empresa.Id = GeradorId();

            if (ValidarSenha(empresa.Senha) != false)
            {                 
                if (BuscaEmpresaCnpj(empresa.Cnpj) == null)
                {
                    listaEmpresa.Add(empresa);
                    Console.Clear();
                    Console.WriteLine("Empresa cadastrada com sucesso!!");
                    Console.WriteLine($"Matricula para acesso: {empresa.Id}");
                }
                else
                    Console.WriteLine($"CNPJ: {empresa.Cnpj} já cadastrado no Banco de dados");
            }
            else
                Console.WriteLine($"A senha deve conter de 8 a 16 caracteres");

        }
        public void EditaEmpresa(Empresa empresaEditada)
        {
            if (BuscaEmpresaId(empresaEditada.Id)!= null)
            {
                if (ValidarSenha(empresaEditada.Senha) != false)
                {
                    var validaCnpj = BuscaEmpresaCnpj(empresaEditada.Cnpj);
                    if (validaCnpj == null || validaCnpj.Id == empresaEditada.Id)
                    {
                        foreach (var item in listaEmpresa)
                        {
                            if (item.Id == empresaEditada.Id)
                            {
                                item.Nome = empresaEditada.Nome;
                                item.Cnpj = empresaEditada.Cnpj;
                                item.Senha = empresaEditada.Senha;
                                break;
                            }
                        }
                        Console.Clear();
                        Console.WriteLine($"Empresa Editada com Sucesso!!");
                    }
                    else
                        Console.WriteLine($"CNPJ: {empresaEditada.Cnpj} já cadastrado no Banco de dados");
                }
            }
            else
                Console.WriteLine($"Empresa não encontrada!!");

        }
        public void DeleteEmpresa(int idEmpresa)
        {
            var empresa = BuscaEmpresaId(idEmpresa);
            if (empresa != null)
            {
                importa_exporta.crudMarcacao.DeletarMarcacaoEmpresa(empresa.Id);
                importa_exporta.crudFuncionario.DeletarFuncionarioEmpresa(empresa.Id);

                listaEmpresa.Remove(empresa);

                Console.WriteLine("Todos dados da empresa foram excluidos");
            }
            else
                Console.WriteLine($"Empresa não encontrada.");            
        }
        public Empresa BuscaEmpresaCnpj(string cnpj)
        {
            foreach (var item in listaEmpresa)
            {
                if (item.Cnpj == cnpj)
                    return item;
            }
            return null;
        }
        public int GeradorId()
        {
            Random random = new();
            int numeroAleatorio;

            while (true)
            {
                numeroAleatorio = random.Next(100, 999);

                bool numeroExistente = false;

                if (listaEmpresa.Count != 0)
                {
                    foreach (var item in listaEmpresa)
                    {
                        if (item.Id == numeroAleatorio)
                        {
                            numeroExistente = true;
                            break;
                        }
                    }
                    if (!numeroExistente)
                        break;
                }
                break;
               
            }
            return numeroAleatorio;
        }
        public static bool ValidarSenha(string senha)
        {
            if (senha.Length < 8 || senha.Length > 16)
            {
                Console.WriteLine("A senha deve conter de 8 a 16 caracteres.");
                return false;
            }

            bool temmaiuscula = false;
            bool temminuscula = false;
            bool temnumero = false;

            foreach (char c in senha)
            {
                if (char.IsUpper(c))
                {
                    temmaiuscula = true;
                }
                else if (char.IsLower(c))
                {
                    temminuscula = true;
                }
                else if (char.IsDigit(c))
                {
                    temnumero = true;
                }
            }

            if (!temmaiuscula || !temminuscula || !temnumero)
            {
                Console.WriteLine("a senha deve conter letras maiúsculas, minúsculas e números.");
                return false;
            }

            return true;
        }
        public Empresa BuscaEmpresaId(int id)
        {
            foreach (var item in listaEmpresa)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }
        public Empresa Login(Empresa login)
        {
            var empresa = BuscaEmpresaId(login.Id);

            if (empresa is not null)
            {
                if (empresa.Senha != login.Senha)
                    return null;
            }
            else
                return null;

            return empresa;
        }

        public List<Empresa> listarEmpresa()
        {
            return listaEmpresa;
        }
    }
}