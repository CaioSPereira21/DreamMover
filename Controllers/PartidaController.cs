using DreamMover.DTO;
using DreamMover.Helpers;
using DreamMover.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DreamMover.Controllers
{
    public class PartidaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private lutasEntities _db;

        public PartidaController()
        {
            _db = new lutasEntities();
        }

        [HttpPost]
        public JsonResult IniciarPartida(LutadorDTO lutadorCasa, LutadorDTO lutadorVisitante)
        {
            var stages = GetStages();
            int indexRandom = new Random().Next(stages.Count);
            var stage = stages[indexRandom];

            string fileName = string.Empty;
            string arguments = string.Empty;
            string path = @"C:\Users\Caio Pereira\Documents\Jogos\PC\MUGEN\LEGACY\mugen.exe";
            fileName = path;
            arguments = "-p1 \"" + lutadorCasa.Nome + "\" -p2 \"" + lutadorVisitante.Nome + "\" -p1.ai 1 -p2.ai 1 -rounds 2 -s \"" + stage + "\"";
            ProcessStartInfo psi = new ProcessStartInfo(fileName, arguments)
            {
                WorkingDirectory = @"C:\Users\Caio Pereira\Documents\Jogos\PC\MUGEN\LEGACY\",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process process = Process.Start(psi);
            Thread.Sleep(3000);

            Process paux = new Process();
            ProcessStartInfo startInfo2 = new ProcessStartInfo();
            startInfo2.WorkingDirectory = @"C:\Users\Caio Pereira\Documents\Jogos\PC\MUGEN\LEGACY\";
            startInfo2.FileName = @"C:\Users\Caio Pereira\Documents\Jogos\PC\MUGEN\LEGACY\windowMode.bat";
            startInfo2.Arguments = "-title M.U.G.E.N -mode maximized";
            startInfo2.UseShellExecute = false;
            paux.StartInfo = startInfo2;
            paux.Start();

            MugenManager mugenManager = new MugenManager(process);
            while (process.HasExited == false) { }
            var pontosLutadorCasa = mugenManager.matchStats.WinsLeft.ToString();
            var pontosLutadorVisitante = mugenManager.matchStats.WinsRight.ToString();

            Save(lutadorCasa, lutadorVisitante, pontosLutadorCasa, pontosLutadorVisitante);
            return Json(new { sucesso = true }, JsonRequestBehavior.DenyGet);
        }

        private bool Save(LutadorDTO lutadorCasa, LutadorDTO lutadorVisitante, string pontosLutadorCasa, string pontosLutadorVisitante)
        {
            Partida partida = new Partida
            {
                IdLutadorCasa = lutadorCasa.Id,
                IdLutadorVisitante = lutadorVisitante.Id,
                DataPartida = DateTime.Now
            };

            if (int.Parse(pontosLutadorCasa) > int.Parse(pontosLutadorVisitante))
            {
                partida.IdVencedor = lutadorCasa.Id;
                partida.idPerdedor = lutadorVisitante.Id;
                partida.PontosVencedor = int.Parse(pontosLutadorCasa);
                partida.PontosPerdedor = int.Parse(pontosLutadorVisitante);
            }
            else
            {
                partida.IdVencedor = lutadorVisitante.Id;
                partida.idPerdedor = lutadorCasa.Id;
                partida.PontosVencedor = int.Parse(pontosLutadorVisitante);
                partida.PontosPerdedor = int.Parse(pontosLutadorCasa);
            }

            _db.Partida.Add(partida);
            _db.SaveChanges();

            #region Exp
            Lutador lutadorCasaDB = _db.Lutador.FirstOrDefault(lutador => lutador.Id == lutadorCasa.Id);
            Lutador lutadorVisitanteDB = _db.Lutador.FirstOrDefault(lutador => lutador.Id == lutadorVisitante.Id);

            //Lutadores de mesmo nível
            if (lutadorCasaDB.Nivel == lutadorVisitanteDB.Nivel)
            {
                lutadorCasaDB.Exp += int.Parse(pontosLutadorCasa) * 100;
                lutadorVisitanteDB.Exp += int.Parse(pontosLutadorVisitante) * 100;
            }

            //Lutador Casa Nivel inferior
            if (lutadorCasaDB.Nivel < lutadorVisitanteDB.Nivel)
            {
                int? diference = (lutadorVisitanteDB.Nivel - lutadorCasaDB.Nivel) + 1;
                lutadorCasaDB.Exp += (int.Parse(pontosLutadorCasa) * 100) * diference;
                lutadorVisitanteDB.Exp += (int.Parse(pontosLutadorVisitante) * 100) / diference;
            }

            //Figther2 Low Level
            if (lutadorCasaDB.Nivel > lutadorVisitanteDB.Nivel)
            {
                var diference = (lutadorCasaDB.Nivel - lutadorVisitanteDB.Nivel) + 1;
                lutadorCasaDB.Exp += (int.Parse(pontosLutadorCasa) * 100) / diference;
                lutadorVisitanteDB.Exp += (int.Parse(pontosLutadorVisitante) * 100) * diference;
            }

            //Level Update
            if (lutadorCasaDB.Exp >= lutadorCasaDB.Nivel * 1000)
            {
                lutadorCasaDB.Nivel++;
                lutadorCasaDB.Exp = 0;
            }
            if (lutadorVisitanteDB.Exp >= lutadorVisitanteDB.Nivel * 1000)
            {
                lutadorVisitanteDB.Nivel++;
                lutadorVisitanteDB.Exp = 0;
            }
            #endregion

            _db.SaveChanges();
            return true;
        }

        private List<string> GetStages()
        {
            var stages = new List<string>();
            string path = @"C:\Users\Caio Pereira\Documents\Jogos\PC\MUGEN\LEGACY\stages";
            string[] caminhos = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            foreach (var item in caminhos)
            {
                if (item.Contains(".def"))
                    stages.Add(Path.GetFileName(item.Replace(".def", "")));
            }
            return stages;
        }
    }
}