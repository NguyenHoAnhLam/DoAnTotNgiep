using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentID("mainmenu",0);
            return View("MainMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentID("mainmenu",id);
            if(list.Count==0)
            {
                return View("MainMenuSub1", menu);//Không có cấp con
            }
            else
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);//có cấp con
            }
        }
        //Slide Show
        public ActionResult Slideshow()
        {
            List<Slider> list = sliderDAO.getListByPosition("Slideshow");
            return View("Slideshow",list);
        }

        //ListCategory
        public ActionResult ListCategory()
        {
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("ListCategory",list);
        }
        public ActionResult PostLastNews()
        {
            return View("PostLastNews");
        }
        public ActionResult MenuFooter()
        {
            List<Menu> list = menuDAO.getListByParentID("footermenu", 0);
            return View("MenuFooter", list);
        }
    }
}