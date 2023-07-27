using Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class
{
    public class CrudFuncionario
    {
        public List<Funcionario> listaFuncionario { get; set; }

        public Importa_Exporta importa_exporta;
        public Funcionario funcionario;
        public CrudFuncionario(Importa_Exporta newImporta_exporta)
        {
            funcionario = new Funcionario();
            importa_exporta = newImporta_exporta;
            listaFuncionario = new List<Funcionario>();
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            funcionario.Id = GeradorId();

            var controleCpf = BuscaFuncionarioCpf(funcionario.Cpf);
            if (controleCpf == null ||controleCpf.IdEmpresa == funcionario.IdEmpresa)
            {
                var controleSenha = ValidarSenha(funcionario.Senha);
                if (controleSenha != false)
                {
                    listaFuncionario.Add(funcionario);
                    Console.WriteLine("Funcionário adicionado com sucesso!!");
                    Console.WriteLine($"Matricula de acesso: {funcionario.Id}");
                }               
            }
            else
                Console.WriteLine($"CPF: {funcionario.Cpf} já cadastrado no Banco de dados");
           
        }

        public void EditaFuncionario(int idEmpresa, Funcionario funcionarioEditado)
        {
            var funcionario = BuscaFuncionarioId(funcionarioEditado.Id);
            if (funcionario != null)
            {
                var controleCpf = BuscaFuncionarioCpf(funcionario.Cpf);
                if (controleCpf.Id != funcionarioEditado.Id && controleCpf.IdEmpresa.Id == idEmpresa)
                {
                    if (funcionario.IdEmpresa.Id == idEmpresa)
                    {
                        var controleSenha = ValidarSenha(funcionario.Senha);
                        if (controleSenha != false)
                        {
                            foreach (var item in listaFuncionario)
                            {
                                if (item.Id == funcionarioEditado.Id)
                                {
                                    item.Nome = funcionarioEditado.Nome;
                                    item.Cpf = funcionarioEditado.Cpf;
                                    item.Senha = funcionarioEditado.Senha;
                                }
                            }
                            Console.WriteLine("Funcionário Editado com sucesso!!");
                        }
                        Console.WriteLine($"A senha deve conter de 8 a 16 caracteres");                        
                    }
                    Console.WriteLine($"Funcionario não pertence a empresa");                    
                }
                Console.WriteLine($"CPF: {funcionario.Cpf} já cadastrado no Banco de dados");
            }
            Console.WriteLine($"Funcionario não encontrado");

            
        }

        public Funcionario BuscaFuncionarioCpf(string cpf)
        {
            foreach (var item in listaFuncionario)
            {
                if (item.Cpf == cpf)
                    return item;
            }
            return null;
        }

        public Funcionario BuscaFuncionarioId(int id)
        {
            foreach (var item in listaFuncionario)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }


        public List<Funcionario> BuscaFuncionariosIdEmpresa(int id)
        {
            List<Funcionario> listaFuncionarios = new();

            foreach (var item in listaFuncionario)
            {
                if (item.IdEmpresa.Id == id)
                    listaFuncionarios.Add(item);
            }

            return listaFuncionarios;
        }


        public Funcionario Login(Funcionario login)
        {
            var funcionario = BuscaFuncionarioId(login.Id);

            if (funcionario is not null)
            {
                if (funcionario.Senha != login.Senha)
                    return null;
            }
            else
                return null;            

            return funcionario;
        }

        public int GeradorId()
        {
            Random random = new();
            int numeroAleatorio;

            while (true)
            {
                numeroAleatorio = random.Next(10000, 99999);

                bool numeroExistente = false;

                if (listaFuncionario.Count != 0)
                {
                    foreach (var item in listaFuncionario)
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

        internal void DeletarFuncionarioEmpresa(int idEmpresa)
        {
            foreach (var item in listaFuncionario)
            {
                if (item.IdEmpresa.Id == idEmpresa)
                {
                    listaFuncionario.Remove(item);
                }
            }

            Console.WriteLine("Registros de Funcionários Deletados com sucesso!!");
        }        
    }
}