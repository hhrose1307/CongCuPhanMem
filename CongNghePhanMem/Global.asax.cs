using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace CongNghePhanMem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application.Add("dem", 0);
        }
        protected void Application_End()
        {
            Application.Remove("dem");
        }
        protected void Session_Start()
        {
            int so = int.Parse(Application.Get("dem").ToString());
            so++;
            Application.Set("dem", so);
            //Đã truy cập
            int count_visit = 0;
            if (System.IO.File.Exists(Server.MapPath("~/Soluot/count_visit.txt")) == false)
            {
                count_visit = 1;
            }
            else
            {
                // Đọc dử liều từ file count_visit.txt
                System.IO.StreamReader read = new System.IO.StreamReader(Server.MapPath("~/Soluot/count_visit.txt"));
                count_visit = int.Parse(read.ReadLine());
                read.Close();
                // Tăng biến count_visit thêm 1
                count_visit++;
            }
            // khóa website
            Application.Lock();

            // gán biến Application count_visit
            Application["count_visit"] = count_visit;

            // Mở khóa website
            Application.UnLock();

            // Lưu dử liệu vào file  count_visit.txt
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("~/Soluot/count_visit.txt"));
            writer.WriteLine(count_visit);
            writer.Close();
        }
        protected void Session_End()
        {
            int so = int.Parse(Application.Get("dem").ToString());
            so--;
            Application.Set("dem", so);
        }
    }

}

