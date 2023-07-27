using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class
{
    public class CrudMarcacao
    {
        public List<Marcacao> listaMarcacao { get; set; }
        public Importa_Exporta importa_exporta;
        public Empresa empresa;
        public CrudMarcacao(Importa_Exporta newImporta_exporta)
        {
            empresa = new Empresa();
            importa_exporta = newImporta_exporta;
            listaMarcacao = new List<Marcacao>();
        }


        public void AddMarcacao(Marcacao marcacao)
        {
            marcacao.Id = GeradorId();
            listaMarcacao.Add(marcacao);

            Console.WriteLine("Registro efetuado com sucesso!!");

        }

        public void EditaMarcacaoId(int id, DateTime marcacaoEditada)
        {
            var marcacao = BuscaMarcacaoID(id);
            if (marcacao == null)
            {
                throw new Exception($"Marcação com o ID {id} não encontrada.");
            }

            marcacao.marcacao = marcacaoEditada;

            Console.WriteLine("Marcação editada com sucesso.");
        }


        public void DeleteMarcacao(int idPonto, int idEmpresa)
        {
            var marcacao = BuscaMarcacaoID(idPonto);
            if (marcacao != null && marcacao.IdEmpresa.Id == idEmpresa)
            {
                listaMarcacao.Remove(marcacao);

                Console.WriteLine("Marcação excluída com sucesso.");
            }
            else
                throw new Exception($"Marcação com o ID {idPonto} não encontrado ou não vinculado a empresa atual.");
        }


        public List<Marcacao> BuscaMarcacaoFuncionario(int idFuncionario, DateTime dataInicial, DateTime dataFinal)
        {
            List<Marcacao> marcacoesFuncionario = new();

            foreach (var marcacao in listaMarcacao)
            {
                if (marcacao.IdFuncionario.Id == idFuncionario)
                {
                    if (marcacao.marcacao.Date >= dataInicial.Date && marcacao.marcacao.Date <= dataFinal.Date)
                    {
                        marcacoesFuncionario.Add(marcacao);
                    }
                }
            }

            marcacoesFuncionario = marcacoesFuncionario.OrderBy(obj => obj.marcacao).ToList();

            return marcacoesFuncionario;
        }


        public List<Marcacao> BuscaMarcacaoEmpresa(int idEmpresa, DateTime dataInicial, DateTime dataFinal)
        {
            List<Marcacao> marcacoesEmpresa = new List<Marcacao>();

            foreach (var marcacao in listaMarcacao)
            {
                if (marcacao.IdEmpresa.Id == idEmpresa)
                {
                    if (marcacao.marcacao >= dataInicial && marcacao.marcacao <= dataFinal)
                    {
                        marcacoesEmpresa.Add(marcacao);
                    }
                }
            }

            return marcacoesEmpresa;
        }

        internal void DeletarMarcacaoEmpresa(int idEmpresa)
        {
            foreach (var item in listaMarcacao)
            {
                if (item.IdEmpresa.Id == idEmpresa)
                {
                    listaMarcacao.Remove(item);
                }
            }
            Console.WriteLine("Registros de marcações Deletados com sucesso!!");
        }


        public Marcacao BuscaMarcacaoID(int id)
        {
            foreach (var marcacao in listaMarcacao)
            {
                if (marcacao.Id == id)
                {
                    return marcacao;
                }
            }
            return null;
        }


        public int GeradorId()
        {
            int id = int.MinValue;

            if (listaMarcacao.Count == 0)
                id = 1;
            else
            {
                foreach (var item in listaMarcacao)
                {
                    if (item.Id > id)
                    {
                        id = item.Id;
                    }
                }
            }
            
            return id+1;
        }

    }
}