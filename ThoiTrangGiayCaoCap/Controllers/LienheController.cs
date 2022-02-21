using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class LienheController : Controller
    {
        ContactDAO contactDAO = new ContactDAO();
        // GET: Contact
        public ActionResult Index()
        {
            return View(contactDAO.getList("Index"));
        }
    }
}