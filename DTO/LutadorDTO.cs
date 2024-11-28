using System.Collections.Generic;

namespace DreamMover.DTO
{
    public class LutadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public string Universo { get; set; }
        public double? Exp { get; set; }
        public int? Nivel { get; set; }
        public string Portrait { get; set; }
        public string Stand { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Partidas { get; set; }
        public List<RivalDTO> Rivais { get; set; }
    }
}