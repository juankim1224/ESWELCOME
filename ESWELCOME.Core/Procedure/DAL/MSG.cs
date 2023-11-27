using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ESWELCOME.DataBase.Procedure.BOL.MSG;

namespace ESWELCOME.DataBase.Procedure.DAL
{
    internal sealed class MSG : ESNfx.DataBase.MSSQL.DataServiceBase
    {
        public MSG() : base() { }
        public MSG(IDbTransaction txn) : base(txn) { }

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
        ///작성일 : 2023-11-23 오후 10:13:55
        ///수정일 : 2023-11-24 오후 7:17:10
        ///</summary>
        public ESNfx.ReturnValue MSG_iu_MESSAGE(iMSG_iu_MESSAGE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.MSG_iu_MESSAGE"
                    //input parameter 시작
                    , CreateParameter("@MSG_ID", SqlDbType.Int, param.MSG_ID)
                    , CreateParameter("@SCH_ID", SqlDbType.Int, param.SCH_ID)
                    , CreateParameter("@MSG_GUBUN", SqlDbType.Int, param.MSG_GUBUN)
                    , CreateParameter("@MSG_TO", SqlDbType.VarChar, param.MSG_TO)
                    , CreateParameter("@MSG_FROM", SqlDbType.VarChar, param.MSG_FROM)
                    , CreateParameter("@MSG_CONTENT", SqlDbType.VarChar, param.MSG_CONTENT)
                    , CreateParameter("@MSG_YEARMD", SqlDbType.VarChar, param.MSG_YEARMD)
                    , CreateParameter("@MSG_HOUR", SqlDbType.VarChar, param.MSG_HOUR)
                    , CreateParameter("@MSG_MIN", SqlDbType.VarChar, param.MSG_MIN)
                    , CreateParameter("@TND_CHECK", SqlDbType.Char, param.TND_CHECK)
                    , CreateParameter("@CRE_MEMID", SqlDbType.Int, param.CRE_MEMID)

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
        ///작성일 : 2023-11-24 오후 7:13:06
        ///수정일 : 2023-11-24 오후 7:13:06
        ///</summary>
        public ESNfx.GenericReturn<MSG_sr_SCHSTAFF> MSG_sr_SCHSTAFF(int? stf_id)
        {
            SqlCommand cmd;

            List<BOL.MSG.MSG_sr_SCHSTAFF> list =
                base.GetListOfType<MSG_sr_SCHSTAFF>(out cmd, "dbo.MSG_sr_SCHSTAFF"
                //input parameter 시작
                    , CreateParameter("@STF_ID", SqlDbType.Int, stf_id)

                    //output parameter 시작
                    , CreateParameter("@memMobileNo", SqlDbType.VarChar, DBNull.Value, 30, ParameterDirection.Output)

            );
            return ESNfx.Generic.GetTopOne<MSG_sr_SCHSTAFF>(list);
        }

        ///<summary>
        ///작성일 : 2023-11-25 오후 4:01:06
        ///수정일 : 2023-11-25 오후 4:01:06
        ///</summary>
        public ESNfx.GenericReturn<MSG_sr_STAFFHP> MSG_sr_STAFFHP(int? stf_id)
        {
            SqlCommand cmd;

            List<BOL.MSG.MSG_sr_STAFFHP> list =
                base.GetListOfType<MSG_sr_STAFFHP>(out cmd, "dbo.MSG_sr_STAFFHP"
                //input parameter 시작
                    , CreateParameter("@STF_ID", SqlDbType.Int, stf_id)

                    //output parameter 시작
                    , CreateParameter("@memMobileNo", SqlDbType.VarChar, DBNull.Value, 30, ParameterDirection.Output)

            );
            return ESNfx.Generic.GetTopOne<MSG_sr_STAFFHP>(list);
        }



    }
}
