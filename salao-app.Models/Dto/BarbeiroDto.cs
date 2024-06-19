using salao_app.Repository.Maps;
using System;

namespace salao_app.Models.Dtos
{
    public class BarbeiroDto
    {
        public int BarbeiroId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Descricao { get; set; }
        public string UrlFoto { get; set; }
        public int UsuarioId { get; set; }

        public static BarbeiroDto ToDto(BarbeiroMap map)
        {
            return new BarbeiroDto
            {
                BarbeiroId = map.BarbeiroId,
                Nome = map.Nome,
                Email = map.Email,
                Descricao = map.Descricao,
                UrlFoto = map.UrlFoto,
                UsuarioId = map.UsuarioId
            };
        }

        public static List<BarbeiroDto> ToDtoList(List<BarbeiroMap> list)
        {
            return list.Select(map => new BarbeiroDto
            {
                BarbeiroId = map.BarbeiroId,
                Nome = map.Nome,
                Email = map.Email,
                Descricao = map.Descricao,
                UrlFoto = map.UrlFoto,
                UsuarioId = map.UsuarioId
            }).ToList();
        }
    }
}
