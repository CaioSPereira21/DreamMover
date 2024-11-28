using DreamMover.DTO;
using DreamMover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamMover.Controllers
{
    public class ConsLutadorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private lutasEntities _db;

        public ConsLutadorController()
        {
            _db = new lutasEntities();
        }

        [HttpPost]
        public JsonResult GetLutadores()
        {
            var lutadores = _db.Lutador.ToList().Select(x => GetLutadorDTO(x.Id)).OrderBy(x => x.Nome).ToList();

            return Json(new { sucesso = true, lutadores }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetDetalhesLutador(LutadorDTO lutador)
        {
            var partidas = _db.Partida.Where(x => x.IdLutadorCasa == lutador.Id || x.IdLutadorVisitante == lutador.Id).ToList();

            lutador.Vitorias = partidas.Where(x => x.IdVencedor == lutador.Id).Count();
            lutador.Derrotas = partidas.Where(x => x.idPerdedor == lutador.Id).Count();
            lutador.Partidas = lutador.Vitorias + lutador.Derrotas;
            lutador.Rivais = GetRivais(lutador.Id);

            return Json(new { sucesso = true, lutador }, JsonRequestBehavior.DenyGet);
        }

        private LutadorDTO GetLutadorDTO(int idLutador)
        {
            var lutador = _db.Lutador.Select(x => new LutadorDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                Autor = x.Autor,
                Universo = "/Imagens/" + x.Universo + ".png",
                Exp = x.Exp,
                Nivel = x.Nivel,
                Portrait = "/Imagens/" + x.Portrait,
                Stand = "/Imagens/" + x.Stand,
            }).FirstOrDefault(x => x.Id == idLutador);

            return lutador;
        }

        private List<RivalDTO> GetRivais(int idLutador)
        {
            var rivais = _db.Partida
                .Where(partida => partida.IdLutadorCasa == idLutador || partida.IdLutadorVisitante == idLutador)
                .GroupBy(partida => partida.IdLutadorCasa == idLutador ? partida.IdLutadorVisitante : partida.IdLutadorCasa)
                .OrderByDescending(g => g.Count())
                .ToList()
                .Select(g => new RivalDTO
                {
                    Id = _db.Lutador.FirstOrDefault(x => x.Id == g.Key).Id,
                    Nome = _db.Lutador.FirstOrDefault(x => x.Id == g.Key)?.Nome,
                    Nivel = _db.Lutador.FirstOrDefault(x => x.Id == g.Key)?.Nivel,
                    Portrait = "/Imagens/" + _db.Lutador.FirstOrDefault(x => x.Id == g.Key)?.Portrait,
                }).ToList();


            foreach (var rival in rivais)
            {
                rival.Partidas = _db.Partida
                .Where(partida => (partida.IdLutadorCasa == rival.Id && partida.IdLutadorVisitante == idLutador) ||
                (partida.IdLutadorCasa == idLutador && partida.IdLutadorVisitante == rival.Id))
                .ToList().Select(partida => new PartidaDTO
                {
                    Id = partida.Id,
                    IdVencedor = partida.IdVencedor,
                    IdPerdedor = partida.idPerdedor,
                }).Take(5).ToList();
            }

            return rivais;
        }

    }
}