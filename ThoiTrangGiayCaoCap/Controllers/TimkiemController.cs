using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using PagedList;
using PagedList.Mvc;
using MyClass.Models;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class TimkiemController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();
        [HttpPost]
        // GET: Search
        public ActionResult KetQuaTimKiem(FormCollection form, int? page)
        {
            string sTuKhoa = form["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa; 
            List <ProductInfo> listkqtk = productDAO.getByTimKiem(sTuKhoa);
            int pageNumber = (page ?? 1);
            int pageSize = 8;

            if (listkqtk.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào";
                return View(productDAO.getList().ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + listkqtk.Count + " Kết Quả!";
            return View(listkqtk.OrderBy(m=>m.Name).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        // GET: Search
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<ProductInfo> listkqtk = productDAO.getByTimKiem(sTuKhoa);
            int pageNumber = (page ?? 1);
            int pageSize = 8;

            if (listkqtk.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào";
                return View(productDAO.getList().ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + listkqtk.Count + " Kết Quả!";
            return View(listkqtk.OrderBy(m => m.Name).ToPagedList(pageNumber, pageSize));
        }
    }
}