using DreamMover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}