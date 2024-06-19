using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Repository.Maps
{
    public class BarbeiroMap
    {
        public int BarbeiroId { get; set; }
        public string Nome { get; set; }
        public DateTime horaInico { get; set; }
        public DateTime horaFim { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string UrlFoto { get; set; }
        public int UsuarioId { get; set; } 

    }
}
