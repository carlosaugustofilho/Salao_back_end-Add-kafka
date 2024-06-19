using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Repository.Maps
{
    public class PagamentoAgendamento
    {
        public int PagamentoId { get; set; }
        public int AgendamentoId { get; set; }
        public int FormaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
