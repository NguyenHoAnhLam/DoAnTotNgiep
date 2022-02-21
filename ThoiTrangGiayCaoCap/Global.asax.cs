using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThoiTrangGiayCaoCap
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            //Lưu thông tin đăng nhập quản lý
            Session["UserAdmin"] = "";
            //Lưu mã người quản lý
            Session["UserId"] = "1";
            Session["FullName"] = "";
            Session["Img"] = "";
            //Lưu giỏ hàng
            Session["MyCart"] = "";
            //Lưu thông tin khách hàng(người dùng)
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";

        }
    }
}
