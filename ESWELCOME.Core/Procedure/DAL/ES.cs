using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ESWELCOME.DataBase.Procedure.BOL.ES;

namespace ESWELCOME.DataBase.Procedure.DAL
{
    internal sealed class ES : ESNfx.DataBase.MSSQL.DataServiceBase
    {
        public ES() : base() { }
        public ES(IDbTransaction txn) : base(txn) { }

        /// <summary>
        /// 웹 or 응용프로그램 설정파일의 connectionStrings의 요소의 이름이 DB인 노드의 connectionString 값을 가져온다.
        /// </summary>
        public string GetBaseConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }

        ///<summary>
		///작성일 : 2023-12-08 오전 11:49:20
		///수정일 : 2023-12-08 오후 1:34:28
		///</summary>
		public ESNfx.GenericReturn<ES_sr_MemLoginUser> ES_sr_MemLoginUser(string loginid, string password)
        {
            List<BOL.ES.ES_sr_MemLoginUser> list =
                base.GetListOfType<ES_sr_MemLoginUser>("dbo.ES_sr_MemLoginUser"             //input parameter 시작
                , CreateParameter("@LoginID", SqlDbType.VarChar, loginid)
                , CreateParameter("@PassWord", SqlDbType.VarChar, password)

            );
            return ESNfx.Generic.GetTopOne<ES_sr_MemLoginUser>(list);
        }



        ///<summary>
        ///작성일 : 2023-11-26 오후 5:46:24
        ///수정일 : 2023-11-26 오후 5:50:27
        ///</summary>
        public ESNfx.ReturnValue ES_un_LOGIN_PW(iES_un_LOGIN_PW param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.ES_un_LOGIN_PW"
                    //input parameter 시작
                    , CreateParameter("@MEM_TYPE", SqlDbType.SmallInt, param.MEM_TYPE)
                    , CreateParameter("@MEM_ID", SqlDbType.Int, param.MEM_ID)
                    , CreateParameter("@BEFORE_PW", SqlDbType.VarChar, param.BEFORE_PW)
                    , CreateParameter("@AFTER_PW", SqlDbType.VarChar, param.AFTER_PW)

                    //output parameter 시작
                    , CreateParameter("@ERR_CD", SqlDbType.SmallInt, DBNull.Value, ParameterDirection.Output)
                    , CreateParameter("@ERR_MSG", SqlDbType.VarChar, DBNull.Value, 50, ParameterDirection.Output)

                );

                if (cmd.Parameters["@ERR_CD"].Value != DBNull.Value)
                    ret["@ERR_CD"] = Convert.ToInt16(cmd.Parameters["@ERR_CD"].Value);
                if (cmd.Parameters["@ERR_MSG"].Value != DBNull.Value)
                    ret["@ERR_MSG"] = Convert.ToString(cmd.Parameters["@ERR_MSG"].Value);

                cmd.Dispose();

                // 프로시저 자체에서 반환하는 오류에 대한 처리 추가
                if (ret.IsContainsKey("@ERR_CD") && Convert.ToInt32(ret["@ERR_CD"]) != 1)
                {
                    ret.setCode(-1);
                    if (ret.IsContainsKey("@ERR_MSG"))
                        ret.Message = ret["@ERR_MSG"].ToString();
                }
                else
                    ret.setCode(1);
            }
            catch (Exception ex)
            {
                ret.Message = ex.Message;
                ret.setCode(-1);
            }
            finally
            {
                ret["@SQLLOG"] = base.ErrorSQL.ToString();
            }
            return ret;
        }

        ///<summary>
        ///작성일 : 2023-11-27 오전 10:53:47
        ///수정일 : 2023-11-27 오전 10:54:55
        ///</summary>
        public ESNfx.GenericReturn<ES_sr_MEMBER> ES_sr_MEMBER(int? mem_id)
        {
            List<BOL.ES.ES_sr_MEMBER> list =
                base.GetListOfType<ES_sr_MEMBER>("dbo.ES_sr_MEMBER"             //input parameter 시작
                , CreateParameter("@MEM_ID", SqlDbType.Int, mem_id)

            );
            return ESNfx.Generic.GetTopOne<ES_sr_MEMBER>(list);
        }


        ///<summary>
        ///작성일 : 2023-12-08 오전 11:49:20
        ///수정일 : 2023-12-08 오후 12:16:35
        ///</summary>
        public ESNfx.GenericReturn<ES_sr_MemLoginUser> ES_sr_MemLoginUser(string loginid, string password)
        {
            List<BOL.ES.ES_sr_MemLoginUser> list =
                base.GetListOfType<ES_sr_MemLoginUser>("dbo.ES_sr_MemLoginUser"             //input parameter 시작
                , CreateParameter("@LoginID", SqlDbType.VarChar, loginid)
                , CreateParameter("@PassWord", SqlDbType.VarChar, password)

            );
            return ESNfx.Generic.GetTopOne<ES_sr_MemLoginUser>(list);
        }



    }
}
