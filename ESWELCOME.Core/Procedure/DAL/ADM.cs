using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ESWELCOME.DataBase.Procedure.BOL.ADM;

namespace ESWELCOME.DataBase.Procedure.DAL
{
    internal sealed class ADM : ESNfx.DataBase.MSSQL.DataServiceBase
    {
        public ADM() : base() { }
        public ADM(IDbTransaction txn) : base(txn) { }

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
        ///작성일 : 2023-11-26 오후 3:02:09
        ///수정일 : 2023-11-26 오후 3:13:35
        ///</summary>
        public List<ADM_sd_MEMBER> ADM_sd_MEMBER(iADM_sd_MEMBER param)
        {
            List<ADM_sd_MEMBER> list =
                base.GetListOfType<ADM_sd_MEMBER>("dbo.ADM_sd_MEMBER"
                //input parameter 시작
                , CreateParameter("@MEMBER_CPY", SqlDbType.VarChar, param.MEMBER_CPY)
                , CreateParameter("@SEARCH_TYPE", SqlDbType.VarChar, param.SEARCH_TYPE)
                , CreateParameter("@SEARCH_TEXT", SqlDbType.VarChar, param.SEARCH_TEXT)
                , CreateParameter("@CURRENTPAGEINDEX", SqlDbType.Int, param.CURRENTPAGEINDEX)
                , CreateParameter("@PAGINGSIZE", SqlDbType.Int, param.PAGINGSIZE)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-12-04 오후 9:45:15
        ///수정일 : 2023-12-04 오후 9:59:20
        ///</summary>
        public ESNfx.ReturnValue ADM_un_DeleteMEM(int? mem_id)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.ADM_un_DeleteMEM"
                    //input parameter 시작
                    , CreateParameter("@MEM_ID", SqlDbType.Int, mem_id)

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




    }
}
