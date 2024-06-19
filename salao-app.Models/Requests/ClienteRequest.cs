using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salao_app.Models.Requests
{
    public class ClienteRequest
    {
        public string? nome{ get; set; }
        public string? email { get; set; }
        public int usuarioId { get; set; } 
        public string? senha { get; set; }
        
    }
}
