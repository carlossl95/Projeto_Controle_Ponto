using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Class;

namespace console
{
    class Program
    {
        public static Importa_Exporta importa_Exporta = new Importa_Exporta(importa_Exporta);

        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao sistema de gerenciamneto de ponto");
            Console.WriteLine("Aguarde enquanto carregamos os dados!!!!!\n");
            importa_Exporta.ImportarDados();
            Thread.Sleep(3000);
            Menu();
        }

        public static void Menu()
        {
            Console.Clear();
            bool menu = true;
            while (menu == true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Funcionario");
                Console.WriteLine("2 - Empresa");
                Console.WriteLine("3 - ADM");
                Console.WriteLine("4 - Sair");

                Console.Write("Digite a opção desejada: ");
                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuFuncionario();
                        break;

                    case "2":
                        MenuEmpresa();
                        break;

                    case "3":
                        LoginADM();
                        break;

                    case "4":
                        SalvarDados();
                        break;

                    default:
                        Console.WriteLine("Opção Invalida");
                        Console.Clear();
                        Menu();
                        break;
                }
            }
        }



        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        // Funções do ADM

        private static void LoginADM()
        {
            Console.Clear();


            //Console.Write("User: ");
            //string userAdm = Console.ReadLine();
            //Console.Write("Senha: ");
            //var senha = Console.ReadLine();

            //if (userAdm != "admin" || senha != "P@ssw0rd")
            //{
            //    Console.WriteLine("Usuario ou senha errado");
            //    Thread.Sleep(3000);
            //    Menu();
            //}
            //else
                MenuAdm();
        }
        private static void MenuAdm()
        {
            Console.Clear();
            bool menu = true;
            while (menu == true)
            {
                Console.WriteLine("------------ ADM ------------");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Adicionar Empresa");
                Console.WriteLine("2 - Editar Empresa");
                Console.WriteLine("3 - Deletar Empresa");
                Console.WriteLine("4 - Voltar");
                Console.WriteLine("7 - Sair");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarEmpresa();
                        break;

                    case "2":
                        EditarEmpresa();
                        break;

                    case "3":
                        DeletarEmpresa();
                        break;

                    case "4":
                        Menu();
                        break;

                    case "7":
                        SalvarDados();
                        break;

                    default:
                        Console.WriteLine("Opção Invalida");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                }
            }
        }
        private static void AdicionarEmpresa()
        {
            Console.Clear();

            Empresa newEmpresa = new();

            Console.WriteLine("--------------- Adicionar Empresa ---------------\n");

            Console.Write("Digite o Nome: ");
            newEmpresa.Nome = Console.ReadLine();

            Console.Write("Digite o CNPJ: ");
            newEmpresa.Cnpj = Console.ReadLine();

            Console.WriteLine("A senha deve conter de 8 a 16 caracteres");
            Console.Write("Digite a Senha: ");
            newEmpresa.Senha = Console.ReadLine();

            importa_Exporta.crudEmpresa.AddEmpresa(newEmpresa);

            Thread.Sleep(3000);
            MenuAdm();
        }

        private static void EditarEmpresa()
        {
            Console.Clear();

            ListarEmpresa();

            Empresa empresaEditada = new();

            Console.WriteLine("--------------- Editar Empresa ---------------\n");

            Console.Write("Digite o ID da empresa a ser editada: ");
            empresaEditada.Id = Convert.ToInt16(Console.ReadLine());

            Console.Write("Digite o Nome: ");
            empresaEditada.Nome = Console.ReadLine();

            Console.Write("Digite o CPF: ");
            empresaEditada.Cnpj = Console.ReadLine();

            Console.WriteLine("A senha deve conter de 8 a 16 caracteres");
            Console.Write("Digite a Senha: ");
            empresaEditada.Senha = Console.ReadLine();

            importa_Exporta.crudEmpresa.EditaEmpresa(empresaEditada);

            Thread.Sleep(3000);
            MenuAdm();
        }       
        private static void DeletarEmpresa()    
        {
            ListarEmpresa();
            Console.WriteLine("--------------- Excluir Empresa ---------------\n");
            Console.Write("Digite A Matricula da empresa para excluir: ");
            int idEmpresa = Convert.ToInt16(Console.ReadLine());

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!!!!!! Atenção Todos os dados vinculados a empresa serão excluidos !!!!!!!!!\n");
            Console.WriteLine("!!!!!!! Atenção Todos os dados vinculados a empresa serão excluidos !!!!!!!!!\n");
            Console.WriteLine("!!!!!!! Atenção Todos os dados vinculados a empresa serão excluidos !!!!!!!!!\n");

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Deseja EXCLUIR TODOS OS DADOS  vinculados a empresa?  S/N: ");
            var excluir = Console.ReadLine();
            Console.ResetColor(); 
            
            if(excluir == "S" || excluir == "s")
            {
                importa_Exporta.crudEmpresa.DeleteEmpresa(idEmpresa);
                Thread.Sleep(3000);
                Console.Clear();
            }
            else if (excluir == "N" || excluir == "n")
            {
                Console.WriteLine("Opção Cancelada");
                Thread.Sleep(3000);
                Console.Clear();
                MenuAdm();
            }
            else
            {
                Console.WriteLine("Opção Invalida");
                Thread.Sleep(3000);
                Console.Clear();
                MenuAdm();
            }
        }        
        private static void ListarEmpresa()
        {
            var listaEmpresa = importa_Exporta.crudEmpresa.listaEmpresa;

            if (listaEmpresa.Count == 0)
            {
                Console.WriteLine("Nenhuma empresa Cadastrada!!");
                Thread.Sleep(3000);
                MenuAdm();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("--------------- Lista Empresas ---------------\n");

                Console.WriteLine("Matricula      CNPJ              Nome");
                foreach (var item in listaEmpresa)
                {
                    Console.WriteLine($"{item.Id}           {item.Cnpj}  -  {item.Nome}");
                }
            }
        }




        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        // Funções da Empresa
        private static void MenuEmpresa()
        {
            Console.Clear();
            bool menu = true;
            while (menu == true)
            {
                Console.WriteLine("------------ Empresa ------------");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Editar Ponto");
                Console.WriteLine("2 - Deletar Ponto");
                Console.WriteLine("3 - Relatório Ponto");
                Console.WriteLine("4 - Adicionar funcionario");
                Console.WriteLine("5 - Editar funcionario");
                Console.WriteLine("6 - Voltar");
                Console.WriteLine("7 - Sair");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        EditarPonto();
                        break;

                    case "2":
                        DeletarPonto();
                        break;

                    case "3":
                        RelatorioPontoEmpresa();
                        break;

                    case "4":
                        AdicionarFuncionario();
                        break;

                    case "5":
                        EditarFuncionario();
                        break;

                    case "6":
                        Menu();
                        break;

                    case "7":
                        SalvarDados();
                        break;

                    default:
                        Console.WriteLine("Opção Invalida");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                }
            }
        }
        private static void EditarFuncionario()
        {
            Console.Clear();

            Empresa empresa = LoginEmpresa();
            ListarFuncionario(empresa.Id);

            Funcionario funcEditado = new();

            Console.WriteLine("--------------- Editra Funcionário ---------------\n");

            Console.Write("Digite o ID do funcionario a ser editado: ");
            funcEditado.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o Nome: ");
            funcEditado.Nome = Console.ReadLine();

            Console.Write("Digite o CPF: ");
            funcEditado.Cpf = Console.ReadLine();

            Console.WriteLine("A senha deve conter de 8 a 16 caracteres");
            Console.Write("Digite a Senha: ");
            funcEditado.Senha = Console.ReadLine();

            importa_Exporta.crudFuncionario.EditaFuncionario(empresa.Id, funcEditado);

            Thread.Sleep(3000);
            MenuEmpresa();
        }
        private static void AdicionarFuncionario()
        {
            Console.Clear();

            Empresa empresa = LoginEmpresa();
            Funcionario func = new();

            func.IdEmpresa = empresa;

            Console.WriteLine("--------------- Adicionar Funcionário ---------------\n");


            Console.Write("Digite o Nome: ");
            func.Nome = Console.ReadLine();

            Console.Write("Digite o CPF: ");
            func.Cpf = Console.ReadLine();

            Console.WriteLine("A senha deve conter de 8 a 16 caracteres");
            Console.Write("Digite a Senha: ");
            func.Senha = Console.ReadLine();

            importa_Exporta.crudFuncionario.AddFuncionario(func);

            Thread.Sleep(3000);
            MenuEmpresa();
        }
        private static void EditarPonto()
        {
            Console.Clear();

            Empresa empresa = LoginEmpresa();

            ListarFuncionario(empresa.Id);

            Console.WriteLine("--------------- Editar Ponto ---------------\n");

            Console.Write("Digite a matricula do funcionário: ");
            int idFuncionario = Convert.ToInt32(Console.ReadLine());

            RelatorioMarcacao(idFuncionario);

            Console.Write("Digite o ID da Marcação: ");
            int idMarcacao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a data e hora da marcação (formato: dd/MM/yyyy HH:mm:ss): ");
            DateTime marcacaoEditada = Convert.ToDateTime(Console.ReadLine());

            importa_Exporta.crudMarcacao.EditaMarcacaoId(idMarcacao, marcacaoEditada);

            Thread.Sleep(3000);
            MenuEmpresa();
        }
        private static void DeletarPonto()
        {
            Console.Clear();

            Empresa empresa = LoginEmpresa();

            ListarFuncionario(empresa.Id);

            Console.WriteLine("--------------- Deletar Ponto ---------------\n");

            Console.Write("Digite a matricula do funcionário: ");
            int idFuncionario = Convert.ToInt32(Console.ReadLine());

            RelatorioMarcacao(idFuncionario);

            Console.Write("Digite o ID da Marcação: ");
            int idMarcacao = Convert.ToInt32(Console.ReadLine());

            importa_Exporta.crudMarcacao.DeleteMarcacao(idMarcacao, empresa.Id);

            Thread.Sleep(3000);
            MenuEmpresa();
        }
        private static void RelatorioPontoEmpresa()
        {
            Console.Clear();

            Empresa empresa = LoginEmpresa();

            ListarFuncionario(empresa.Id);

            Console.Write("Digite a matricula do funcionário: ");
            int id = Convert.ToInt32(Console.ReadLine());

            RelatorioMarcacao(id);

            Console.Write("Aperte qualquer tecla para Retornar");
            Console.ReadKey();
            MenuEmpresa();
        }
        private static Empresa LoginEmpresa()
        {
            Console.Clear();

            Empresa login = new();

            Console.WriteLine("----------Login Empresa----------");

            Console.Write("Digite a Matricula: ");
            login.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite a senha: ");
            login.Senha = Console.ReadLine();

            Empresa empresa = importa_Exporta.crudEmpresa.Login(login);

            if (empresa == null)
            {
                Console.Clear();
                Console.WriteLine("Matricula ou Senha Errado");
                Thread.Sleep(3000);
                MenuEmpresa();
            }

            Console.Clear();
            return empresa;
        }




        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        // Funções dos funcionarios
        private static void MenuFuncionario()
        {
            Console.Clear();

            while (true)
            {
                Console.WriteLine("------------ Funcionário ------------");
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1 - Marcação");
                Console.WriteLine("2 - Relatório");
                Console.WriteLine("3 - Voltar");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MarcacaoPonto();
                        break;

                    case "2":
                        RelatorioPontoFuncionario();
                        break;

                    case "3":
                        Menu();
                        break;

                    default:
                        Console.WriteLine("Opção Invalida");
                        Thread.Sleep(3000);
                        Console.Clear();
                        break;
                }
            }
        }
        private static void RelatorioPontoFuncionario()
        {
            Funcionario funcionario = LoginFuncionario();

            RelatorioMarcacao(funcionario.Id);

            Console.Write("\n\n\nAperte qualquer tecla para Retornar");
            Console.ReadKey();
            MenuFuncionario();
        }
        private static void MarcacaoPonto()
        {
            Funcionario funcionario = LoginFuncionario();

            Console.Write("Digite a data e hora da marcação (formato: dd/MM/yyyy HH:mm:ss): ");
            DateTime dataHoraMarcacao = Convert.ToDateTime(Console.ReadLine());

            Marcacao ponto = new()
            {
                marcacao = dataHoraMarcacao,
                IdFuncionario = funcionario,
                IdEmpresa = funcionario.IdEmpresa
            };

            importa_Exporta.crudMarcacao.AddMarcacao(ponto);

            Console.WriteLine("Marcação realizada com Sucesso!!");

            Thread.Sleep(3000);

            MenuFuncionario();
        }
        private static Funcionario LoginFuncionario()
        {
            Console.Clear();

            Funcionario login = new();

            Console.WriteLine("----------Login----------");

            Console.Write("Digite a Matricula: ");
            login.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite a senha: ");
            login.Senha = Console.ReadLine();

            Funcionario funcionario = importa_Exporta.crudFuncionario.Login(login);

            if (funcionario == null)
            {
                Console.Clear();

                Console.WriteLine("Matricula ou Senha Errado");

                Thread.Sleep(3000);

                MenuFuncionario();
            }

            return funcionario;
        }




        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        // Funções comuns
        private static void RelatorioMarcacao(int idFuncionario)
        {

            Console.Write("Digite a data Inicial (formato: dd/MM/yyyy): ");
            DateTime dataInicial = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data Final (formato: dd/MM/yyyy): ");
            DateTime dataFinal = Convert.ToDateTime(Console.ReadLine());

            List<Marcacao> relatorio = importa_Exporta.crudMarcacao.BuscaMarcacaoFuncionario(idFuncionario, dataInicial, dataFinal);

            if (relatorio.Count == 0)
            {
                Console.WriteLine("Nenhuma marcação encontrada no intervalo de data!!");
                Thread.Sleep(3000);
                Menu();
            }
            else
            {
                Console.Clear();

                DateTime controle = new();

                foreach (var item in relatorio)
                {
                    if (controle == DateTime.MinValue)
                    {
                        controle = item.marcacao.Date;
                        Console.WriteLine($"Data: {item.marcacao.Day}/{item.marcacao.Month}/{item.marcacao.Year}");
                        Console.WriteLine($"ID    Marcacao");
                        Console.WriteLine("-------------------------------------------------");
                    }
                    else if (item.marcacao.Date != controle.Date)
                    {
                         controle = item.marcacao.Date;
                         Console.WriteLine("-------------------------------------------------\n");
                         Console.WriteLine($"Data: {item.marcacao.Day}/{item.marcacao.Month}/{item.marcacao.Year}");
                         Console.WriteLine($"ID    Marcacao");
                         Console.WriteLine("-------------------------------------------------");
                    }
                    Console.WriteLine($"{item.Id}  -  {item.marcacao.Hour}:{item.marcacao.Minute}:{item.marcacao.Second}");
                }
            }
        }

        private static void ListarFuncionario(int idEmpresa)
        {
            List<Funcionario> listaFuncionario = importa_Exporta.crudFuncionario.BuscaFuncionariosIdEmpresa(idEmpresa);

            if (listaFuncionario.Count == 0)
            {
                Console.WriteLine("Empresa sem funcionario cadastrado!!");
                Thread.Sleep(3000);
                Menu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("--------------- Lista Funcionário ---------------\n");

                Console.WriteLine("Matricula        Nome");
                foreach (var item in listaFuncionario)
                {
                    Console.WriteLine($"{item.Id}             {item.Nome}");
                }
            }
        }
        private static void SalvarDados()
        {
            importa_Exporta.ExportarDados();
        }

    }
}


