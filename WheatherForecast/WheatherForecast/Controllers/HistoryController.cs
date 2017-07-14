using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IUnitOfWork _uow;

        public HistoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: History
        public ActionResult Index()
        {
            var history = _uow.Repository<ForecastEntity>().GetAll();
            return View(history.Reverse());
        }

        public ActionResult Search(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = _uow.Repository<ForecastEntity>().GetAll().Where(c => c.City == city);
            ViewBag.InfoMessage = $"Results for city '{city}'";
            return View(view, history.Reverse());
        }

        [HttpPost]
        public ActionResult Search(DateTime? date)
        {
            if (!date.HasValue)
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = _uow.Repository<ForecastEntity>().GetAll()
                .Where(c => c.Date.ToShortDateString().Equals(date.Value.ToShortDateString()));
            ViewBag.InfoMessage = $"Results for {date.Value.ToShortDateString()}";
            return View(view, history.Reverse());
        }

    }
}