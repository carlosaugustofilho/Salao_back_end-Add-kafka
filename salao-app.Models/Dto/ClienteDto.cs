using salao_app.Repository.Maps;
using System;
using System.Collections.Generic;

namespace salao_app.Models.DTOs
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        
        public DateTime? DataRegistro { get; set; }

        public List<ClienteDto> ToDtoList(List<ClienteMap> list)
        {
            List<ClienteDto> dto = new();

            foreach (var map in list)
            {
                dto.Add(new ClienteDto
                {
                    ClienteId = map.clienteId,

                    Nome = map.nome,
                    Email = map.email,
                   
                });
            }

            return dto;
        }

        public ClienteDto ToDto(ClienteMap map)
        {
            if (map == null)
            {
                
                throw new ArgumentNullException(nameof(map), "ClienteMap object cannot be null");
            }
            return new ClienteDto
            {
               ClienteId= map.clienteId,
               Email= map.email,
               Nome = map.nome,
              
               DataRegistro = DataRegistro,

            };
        }
    }
}
