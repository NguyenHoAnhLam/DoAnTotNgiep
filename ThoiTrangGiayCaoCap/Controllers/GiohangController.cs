using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using Common;

namespace ThoiTrangGiayCaoCap.Controllers
{
    public class GiohangController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();
        private UserDAO userDAO = new UserDAO();
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        XCart xcart = new XCart();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index", listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.Price, 1);
            //Thêm vào giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem);
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listquantity = form["quantity"];
                var listarr = listquantity.Split(',');
                xcart.UpdateCart(listarr);
            }
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDelAll()
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult ThanhToan()
        {

            List<CartItem> listcart = xcart.getCart();
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/dang-nhap");
            }
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("ThanhToan", listcart);
        }
        public ActionResult DatMua(FormCollection field)
        {
            //Luu thông tin vào order và order detail
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            String note = field["Note"];
            Order order = new Order();
            order.UserId = userid;
            order.Note = note;
            order.Status = 1;
            if (orderDAO.Insert(order) == 1)
            {
                decimal total = 0;
                List<CartItem> listcart = xcart.getCart();
                foreach(CartItem cartItem in listcart)
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.Id;
                    orderdetail.ProductId = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.Quantity = cartItem.Quantity;
                    orderdetail.Amounts = cartItem.Amount;
                    orderdetailDAO.Insert(orderdetail);
                    total += cartItem.Amount;
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Public/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", user.Fullname);
                content = content.Replace("{{Phone}}", user.Phone);
                content = content.Replace("{{Email}}", user.Email);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(user.Email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            Session["MyCart"] = "";
            return RedirectToAction("DatMuaThanhCong", "GioHang");
        }
        public ActionResult DatMuaThanhCong()
        {
            return View();
        }
    }
}