using easyUITest.Dal;
using easyUITest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace easyUITest.BLL
{
    public class UserMsgBLL
    {
        UserMsgDal dal = new UserMsgDal();
        //分页查询
       public List<UserMsg> GetPageData(int pageIndex,int pageSize,out int pageCount,out int recordCount)
        {
           return dal.GetPageData(pageIndex, pageSize, out pageCount, out recordCount);
        }
         //分页模糊查询 重载
       public List<UserMsg> GetPageData2(int selId, string selName, int pageIndex, int pageSize, out int pageCount, out int recordCount)
       {
           return dal.GetPageData2(selId, selName, pageIndex, pageSize,out pageCount, out recordCount);
       }

           //增加
         public bool Add(UserMsg msg)
       {
           return dal.Add(msg)>0;
       }
           //修改
         public bool Edit(UserMsg msg)
         {
             return dal.Edit(msg)>0;
         }
             //删除
         public bool Delete(int id)
         {
            return dal.Delete(id)>0;
         }
        public UserMsg GetOneData(int id)
         {
             return dal.GetOneData(id);
         }
    }
}
