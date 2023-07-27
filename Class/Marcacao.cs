using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class
{
    public class Marcacao
    {
        public int Id { get; set; }

        public Funcionario IdFuncionario { get; set; }

        public Empresa IdEmpresa { get; set; }

        public DateTime marcacao { get; set; }

        
    }
}