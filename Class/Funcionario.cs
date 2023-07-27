using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class
{
    public class Funcionario
    {
       

        public string Cpf { get; set; }
        public Empresa IdEmpresa { get; set; }
        public int Id { get ; set ; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        

    }
}