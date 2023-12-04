using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ESWELCOME.DataBase.Procedure.BOL.SCH;

namespace ESWELCOME.DataBase.Procedure.DAL
{
    internal sealed class SCH : ESNfx.DataBase.MSSQL.DataServiceBase
    {
        public SCH() : base() { }
        public SCH(IDbTransaction txn) : base(txn) { }

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
        ///작성일 : 2023-11-24 오후 7:11:21
        ///수정일 : 2023-11-27 오전 10:59:49
        ///</summary>
        public ESNfx.ReturnValue SCH_un_SCHEDULE(iSCH_un_SCHEDULE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.SCH_un_SCHEDULE"
                    //input parameter 시작
                    , CreateParameter("@SCH_ID", SqlDbType.Int, param.SCH_ID)
                    , CreateParameter("@EDIT_MODE", SqlDbType.VarChar, param.EDIT_MODE)
                    , CreateParameter("@GST_CPY", SqlDbType.VarChar, param.GST_CPY)
                    , CreateParameter("@GST_PST", SqlDbType.VarChar, param.GST_PST)
                    , CreateParameter("@GST_NAME", SqlDbType.VarChar, param.GST_NAME)
                    , CreateParameter("@GST_MOBILE_NO", SqlDbType.VarChar, param.GST_MOBILE_NO)
                    , CreateParameter("@SCH_YEARMD", SqlDbType.VarChar, param.SCH_YEARMD)
                    , CreateParameter("@SCH_HOUR", SqlDbType.VarChar, param.SCH_HOUR)
                    , CreateParameter("@SCH_MIN", SqlDbType.VarChar, param.SCH_MIN)
                    , CreateParameter("@SCH_TYPE", SqlDbType.VarChar, param.SCH_TYPE)
                    , CreateParameter("@SCH_MONITER", SqlDbType.VarChar, param.SCH_MONITER)
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
        ///작성일 : 2023-11-23 오후 11:00:27
        ///수정일 : 2023-12-05 오후 1:28:10
        ///</summary>
        public ESNfx.GenericReturn<SCH_sr_SCHEDULE> SCH_sr_SCHEDULE(int? sch_id)
        {
            List<BOL.SCH.SCH_sr_SCHEDULE> list =
                base.GetListOfType<SCH_sr_SCHEDULE>("dbo.SCH_sr_SCHEDULE"               //input parameter 시작
                , CreateParameter("@SCH_ID", SqlDbType.Int, sch_id)

            );
            return ESNfx.Generic.GetTopOne<SCH_sr_SCHEDULE>(list);
        }


        ///<summary>
        ///작성일 : 2023-12-01 오후 4:48:33
        ///수정일 : 2023-12-01 오후 4:56:10
        ///</summary>
        public List<SCH_sd_StaffForSchId> SCH_sd_StaffForSchId(int? sch_id)
        {
            List<SCH_sd_StaffForSchId> list =
                base.GetListOfType<SCH_sd_StaffForSchId>("dbo.SCH_sd_StaffForSchId"
                //input parameter 시작
                , CreateParameter("@SCH_ID", SqlDbType.Int, sch_id)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-11-22 오후 4:45:57
        ///수정일 : 2023-11-29 오후 5:41:49
        ///</summary>
        public List<SCH_sd_STAFF> SCH_sd_STAFF(int? cpy_cd, string dept_nm, string team_nm)
        {
            List<SCH_sd_STAFF> list =
                base.GetListOfType<SCH_sd_STAFF>("dbo.SCH_sd_STAFF"
                //input parameter 시작
                , CreateParameter("@CPY_CD", SqlDbType.Int, cpy_cd)
                , CreateParameter("@DEPT_NM", SqlDbType.VarChar, dept_nm)
                , CreateParameter("@TEAM_NM", SqlDbType.VarChar, team_nm)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-12-03 오후 7:10:52
        ///수정일 : 2023-12-03 오후 7:10:52
        ///</summary>
        public List<SCH_sd_SelectStaff> SCH_sd_SelectStaff(int? sch_id, string arr_staff_id)
        {
            List<SCH_sd_SelectStaff> list =
                base.GetListOfType<SCH_sd_SelectStaff>("dbo.SCH_sd_SelectStaff"
                //input parameter 시작
                , CreateParameter("@SCH_ID", SqlDbType.Int, sch_id)
                , CreateParameter("@ARR_STAFF_ID", SqlDbType.VarChar, arr_staff_id)

            );
            return list;
        }


        ///<summary>
        ///작성일 : 2023-11-23 오전 11:56:07
        ///수정일 : 2023-11-23 오후 3:15:07
        ///</summary>
        public List<SCH_sd_SCHEDULE_MAIN> SCH_sd_SCHEDULE_MAIN(iSCH_sd_SCHEDULE_MAIN param)
        {
            List<SCH_sd_SCHEDULE_MAIN> list =
                base.GetListOfType<SCH_sd_SCHEDULE_MAIN>("dbo.SCH_sd_SCHEDULE_MAIN"
                //input parameter 시작
                , CreateParameter("@SEARCH_YEAR", SqlDbType.VarChar, param.SEARCH_YEAR)
                , CreateParameter("@SEARCH_MONTH", SqlDbType.VarChar, param.SEARCH_MONTH)
                , CreateParameter("@CURRENTPAGEINDEX", SqlDbType.Int, param.CURRENTPAGEINDEX)
                , CreateParameter("@PAGINGSIZE", SqlDbType.Int, param.PAGINGSIZE)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-11-23 오후 3:11:38
        ///수정일 : 2023-11-26 오후 3:59:38
        ///</summary>
        public List<SCH_sd_SCHEDULE_LIST> SCH_sd_SCHEDULE_LIST(iSCH_sd_SCHEDULE_LIST param)
        {
            List<SCH_sd_SCHEDULE_LIST> list =
                base.GetListOfType<SCH_sd_SCHEDULE_LIST>("dbo.SCH_sd_SCHEDULE_LIST"
                //input parameter 시작
                , CreateParameter("@SEARCH_TYPE", SqlDbType.VarChar, param.SEARCH_TYPE)
                , CreateParameter("@SEARCH_TXT", SqlDbType.VarChar, param.SEARCH_TXT)
                , CreateParameter("@SCH_TYPE", SqlDbType.VarChar, param.SCH_TYPE)
                , CreateParameter("@START_DATE", SqlDbType.VarChar, param.START_DATE)
                , CreateParameter("@END_DATE", SqlDbType.VarChar, param.END_DATE)
                , CreateParameter("@CURRENTPAGEINDEX", SqlDbType.Int, param.CURRENTPAGEINDEX)
                , CreateParameter("@PAGINGSIZE", SqlDbType.Int, param.PAGINGSIZE)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-11-23 오후 3:19:24
        ///수정일 : 2023-11-23 오후 3:19:24
        ///</summary>
        public List<SCH_sd_CALENDAR> SCH_sd_CALENDAR(string search_year, string search_month)
        {
            List<SCH_sd_CALENDAR> list =
                base.GetListOfType<SCH_sd_CALENDAR>("dbo.SCH_sd_CALENDAR"
                //input parameter 시작
                , CreateParameter("@SEARCH_YEAR", SqlDbType.VarChar, search_year)
                , CreateParameter("@SEARCH_MONTH", SqlDbType.VarChar, search_month)

            );
            return list;
        }

        ///<summary>
        ///작성일 : 2023-11-22 오후 2:17:39
        ///수정일 : 2023-11-30 오후 11:16:50
        ///</summary>
        public ESNfx.ReturnValue SCH_iu_STAFF(iSCH_iu_STAFF param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.SCH_iu_STAFF"
                    //input parameter 시작
                    , CreateParameter("@MEM_ID", SqlDbType.Int, param.MEM_ID)
                    , CreateParameter("@SCH_ID", SqlDbType.Int, param.SCH_ID)
                    , CreateParameter("@STF_GUBUN", SqlDbType.Int, param.STF_GUBUN)
                    , CreateParameter("@IU_GUBUN", SqlDbType.VarChar, param.IU_GUBUN)
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
        ///작성일 : 2023-11-24 오후 5:51:14
        ///수정일 : 2023-11-27 오전 12:42:42
        ///</summary>
        public ESNfx.ReturnValue SCH_iu_CHECKIN(string gst_mobile_no, string msg_code)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.SCH_iu_CHECKIN"
                    //input parameter 시작
                    , CreateParameter("@GST_MOBILE_NO", SqlDbType.VarChar, gst_mobile_no)
                    , CreateParameter("@MSG_CODE", SqlDbType.VarChar, msg_code)

                    //output parameter 시작
                    , CreateParameter("@ERR_CD", SqlDbType.SmallInt, DBNull.Value, ParameterDirection.Output)
                    , CreateParameter("@ERR_MSG", SqlDbType.VarChar, DBNull.Value, 50, ParameterDirection.Output)
                    , CreateParameter("@SCH_ID", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)

                );

                if (cmd.Parameters["@ERR_CD"].Value != DBNull.Value)
                    ret["@ERR_CD"] = Convert.ToInt16(cmd.Parameters["@ERR_CD"].Value);
                if (cmd.Parameters["@ERR_MSG"].Value != DBNull.Value)
                    ret["@ERR_MSG"] = Convert.ToString(cmd.Parameters["@ERR_MSG"].Value);
                if (cmd.Parameters["@SCH_ID"].Value != DBNull.Value)
                    ret["@SCH_ID"] = Convert.ToInt32(cmd.Parameters["@SCH_ID"].Value);

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
        ///작성일 : 2023-11-22 오후 1:54:57
        ///수정일 : 2023-11-25 오후 2:49:23
        ///</summary>
        public ESNfx.ReturnValue SCH_in_SCHEDULE(iSCH_in_SCHEDULE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.SCH_in_SCHEDULE"
                    //input parameter 시작
                    , CreateParameter("@GST_CPY", SqlDbType.VarChar, param.GST_CPY)
                    , CreateParameter("@GST_PST", SqlDbType.VarChar, param.GST_PST)
                    , CreateParameter("@GST_NAME", SqlDbType.VarChar, param.GST_NAME)
                    , CreateParameter("@GST_MOBILE_NO", SqlDbType.VarChar, param.GST_MOBILE_NO)
                    , CreateParameter("@SCH_YEARMD", SqlDbType.VarChar, param.SCH_YEARMD)
                    , CreateParameter("@SCH_HOUR", SqlDbType.VarChar, param.SCH_HOUR)
                    , CreateParameter("@SCH_MIN", SqlDbType.VarChar, param.SCH_MIN)
                    , CreateParameter("@SCH_TYPE", SqlDbType.VarChar, param.SCH_TYPE)
                    , CreateParameter("@SCH_MONITER", SqlDbType.VarChar, param.SCH_MONITER)
                    , CreateParameter("@CRE_MEMID", SqlDbType.Int, param.CRE_MEMID)

                    //output parameter 시작
                    , CreateParameter("@ERR_CD", SqlDbType.SmallInt, DBNull.Value, ParameterDirection.Output)
                    , CreateParameter("@ERR_MSG", SqlDbType.VarChar, DBNull.Value, 50, ParameterDirection.Output)
                    , CreateParameter("@sch_pk", SqlDbType.Int, DBNull.Value, ParameterDirection.Output)
                    , CreateParameter("@msgCode", SqlDbType.VarChar, DBNull.Value, 4, ParameterDirection.Output)

                );

                if (cmd.Parameters["@ERR_CD"].Value != DBNull.Value)
                    ret["@ERR_CD"] = Convert.ToInt16(cmd.Parameters["@ERR_CD"].Value);
                if (cmd.Parameters["@ERR_MSG"].Value != DBNull.Value)
                    ret["@ERR_MSG"] = Convert.ToString(cmd.Parameters["@ERR_MSG"].Value);
                if (cmd.Parameters["@sch_pk"].Value != DBNull.Value)
                    ret["@sch_pk"] = Convert.ToInt32(cmd.Parameters["@sch_pk"].Value);
                if (cmd.Parameters["@msgCode"].Value != DBNull.Value)
                    ret["@msgCode"] = Convert.ToString(cmd.Parameters["@msgCode"].Value);

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
        /// 수정대상 없는 접견인 삭제
        ///</summary>
        public ESNfx.ReturnValue SCH_un_SCHSTAFF(string update_staff_id, int? sch_id, int? cre_memid)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            try
            {
                SqlCommand cmd;

                base.ExecuteNonQuery(out cmd, "dbo.SCH_un_SCHSTAFF"
                    //input parameter 시작
                    , CreateParameter("@UPDATE_STAFF_ID", SqlDbType.VarChar, update_staff_id)
                    , CreateParameter("@SCH_ID", SqlDbType.Int, sch_id)
                    , CreateParameter("@CRE_MEMID", SqlDbType.Int, cre_memid)

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
