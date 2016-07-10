using easyUITest.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace easyUITest.Dal
{
     public class UserMsgDal
    {
         //分页查询
       public List<UserMsg> GetPageData(int pageIndex,int pageSize,out int pageCount,out int recordCount)
         {
             string sql = "usp_FenYe";
             SqlParameter[] pms = new SqlParameter[] { 
             new SqlParameter("@pageIndex",SqlDbType.Int){Value=pageIndex},
             new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize},
             new SqlParameter("@recordCount",SqlDbType.Int){Direction=ParameterDirection.Output},
             new SqlParameter("@pageCount",SqlDbType.Int){Direction=ParameterDirection.Output}
             };
             List<UserMsg> list = new List<UserMsg>();
             using (SqlDataReader reader=SqlHelper.ExecuteReader(sql,CommandType.StoredProcedure,pms))
             {
                 if (reader.HasRows)
                 {
                     while (reader.Read())
                     {
                         UserMsg msg = new UserMsg();
                         msg.UserId = reader.GetInt32(0);
                         msg.UserName = reader.GetString(1);
                         msg.UserPwd = reader.GetString(2);
                         msg.UserRemark = reader.GetString(3);
                         list.Add(msg);
                     }
                 }
             }
             recordCount =Convert.ToInt32(pms[2].Value);
             pageCount = Convert.ToInt32(pms[3].Value);
             return list;
         }
         //分页模糊查询 重载
       public List<UserMsg> GetPageData2(int selId, string selName, int pageIndex, int pageSize, out int pageCount, out int recordCount)
        {
            string sql = "usp_FenYe2";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@selId",SqlDbType.Int){Value=selId},
                new SqlParameter("@selName",SqlDbType.NVarChar){Value=selName},
                new SqlParameter("@pageIndex",SqlDbType.Int){Value=pageIndex},
                new SqlParameter("@pageSize",SqlDbType.Int){Value=pageSize},
                new SqlParameter("@recordCount",SqlDbType.Int){Direction=ParameterDirection.Output},
                new SqlParameter("@pageCount",SqlDbType.Int){Direction=ParameterDirection.Output}
           };
            List<UserMsg> list = new List<UserMsg>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.StoredProcedure, pms))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMsg msg = new UserMsg();
                        msg.UserId = reader.GetInt32(0);
                        msg.UserName = reader.GetString(1);
                        msg.UserPwd = reader.GetString(2);
                        msg.UserRemark = reader.GetString(3);
                        list.Add(msg);
                    }
                }
            }
            recordCount = Convert.ToInt32(pms[4].Value);
            pageCount = Convert.ToInt32(pms[5].Value);
            return list;

        }

         //增加
         public int Add(UserMsg msg)
        {
           string sql = "insert into UserMsg values(@name,@pwd,@remark,@isDel);";
           SqlParameter[] pms = new SqlParameter[]{
               new SqlParameter("@name",SqlDbType.NVarChar,10){Value=msg.UserName},
               new SqlParameter("@pwd",SqlDbType.VarChar,20){Value=msg.UserPwd},
               new SqlParameter("@remark",SqlDbType.NVarChar,50){Value=msg.UserRemark},
               new SqlParameter("@isDel",SqlDbType.Bit){Value=msg.IsDelete}
             };
           return SqlHelper.ExecuteNonquery(sql, CommandType.Text,pms);
         }

         //修改
         public int Edit(UserMsg msg)
         {
             string sql = "update UserMsg set UserName=@name,UserPwd=@pwd,UserRemark=@remark,IsDelete=@isDel where UserId=@id;";
             SqlParameter[] pms = new SqlParameter[]{
                 new SqlParameter("@name",SqlDbType.NVarChar,10){Value=msg.UserName},
                 new SqlParameter("@pwd",SqlDbType.VarChar,20){Value=msg.UserPwd},
                 new SqlParameter("@remark",SqlDbType.NVarChar,50){Value=msg.UserRemark},
                 new SqlParameter("@isDel",SqlDbType.Bit){Value=msg.IsDelete},
                 new SqlParameter("@id",SqlDbType.Int){Value=msg.UserId}
             };
             return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pms);
         }
         //删除
         public int Delete(int id)
         {
             string sql = "update UserMsg set IsDelete=0 where UserId=@id;";
             SqlParameter pms = new SqlParameter("@id", SqlDbType.Bit) { Value = id };
             return SqlHelper.ExecuteNonquery(sql, CommandType.Text, pms);
         }
         /// <summary>
         /// 根据id查询用户信息
         /// </summary>
         /// <param name="id">用户id</param>
         /// <returns></returns>
         public UserMsg GetOneData(int id)
         {
             UserMsg msg = new UserMsg();
             string sql = "select *from UserMsg where UserId=@id;";
             SqlParameter pms = new SqlParameter("@id", SqlDbType.Int) { Value = id };
             using (SqlDataReader reader=SqlHelper.ExecuteReader(sql,CommandType.Text,pms))
             {
                 if (reader.HasRows)
                 {
                     while (reader.Read())
                     {
                         msg.UserId = reader.GetInt32(0);
                         msg.UserName = reader.GetString(1);
                         msg.UserPwd = reader.GetString(2);
                         msg.UserRemark = reader.GetString(3);                         
                     }
                 }
             }
             return msg;
         }
    }
}
