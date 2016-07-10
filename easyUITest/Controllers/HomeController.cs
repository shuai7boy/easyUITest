using easyUITest.BLL;
using easyUITest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        //无条件查询所有分页数据
        UserMsgBLL bll = new BLL.UserMsgBLL();
        public ActionResult GetPageList()
        {
            int pageIndex = 1;
            int pageSize = 10;
            int pageCount=0,recordCount=0;
            int.TryParse(Request["rows"], out pageSize);
            int.TryParse(Request["page"], out pageIndex);           
            List<UserMsg> list= bll.GetPageData(pageIndex, pageSize, out pageCount, out recordCount);
            return Json(new { total = recordCount, rows = list }, JsonRequestBehavior.AllowGet);

        }
        //带条件查询分页数据      
        public ActionResult GetPageList2()
        {
            int strId=0;
            int pageIndex = 1;
            int pageSize = 10;
            int pageCount = 0, recordCount = 0;
            int.TryParse(Request["sId"],out strId);
            string strName = Request["sName"];
            if (string.IsNullOrEmpty(strName))
            {
                strName = "";
            }
            int.TryParse(Request["rows"], out pageSize);
            int.TryParse(Request["page"], out pageIndex);
            List<UserMsg> list = bll.GetPageData2(strId, strName, pageIndex, pageSize, out pageCount, out recordCount);
            return Json(new { total = recordCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add()
        {
            string result = "ok";
            string userName = Request["UserName"];
            string userPwd = Request["UserPwd"];
            string userRemark = Request["UserRemark"];
            if (string.IsNullOrEmpty(userName))
            {
                result = "请输入用户名...";
            }else if(string.IsNullOrEmpty(userPwd))
            {
                result = "请输入密码...";
            }else if(string.IsNullOrEmpty(userRemark))
            {
                result = "请输入备注信息...";
            }
            else
            {
                UserMsg msg = new UserMsg();
                msg.UserName = userName;
                msg.UserPwd = Md5Helper.GetMd5Str(userPwd);
                msg.UserRemark = userRemark;
                msg.IsDelete = 1;
                UserMsgBLL bll = new UserMsgBLL();
                if(bll.Add(msg)==false)
                {
                    result = "添加失败!";
                }               
                
            }           
        
            return Content(result);
        }
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            UserMsg msg = bll.GetOneData(id);
            return View(msg);
        }
        [HttpPost]
        public ActionResult Edit(UserMsg msg)
        {
            //处理修改的信息
            var result = "ok";
            msg.IsDelete = 1;
            if ((msg.UserPwd).Trim() != "")
            {
                msg.UserPwd = Md5Helper.GetMd5Str(msg.UserPwd);
            }
            else
            {
                msg.UserPwd = Request["UserPwdOld"];
            }
           //判断输入的数据是否为空
            if (string.IsNullOrEmpty(msg.UserName))
            {
                result = "请填写用户名...";
            }
            else if(string.IsNullOrEmpty(msg.UserRemark)){
                result = "请填写备注...";
            }
            else
            {
                if (bll.Edit(msg))
                {

                }
                else
                {
                    result = "保存时出现故障，请稍后重试...";
                }
            }

            return Content(result);
        }

    }
}
