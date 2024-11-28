using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamMover.DTO
{
    public class PartidaDTO
    {
        public int Id { get; set; }

        public int? IdVencedor { get; set; }

        public int? IdPerdedor { get; set; }
    }
}