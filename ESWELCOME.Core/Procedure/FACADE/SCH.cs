using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESWELCOME.DataBase.Procedure.DAL;
using ESWELCOME.DataBase.Procedure.BOL.SCH;

namespace ESWELCOME.DataBase.Procedure.Facade
{
    public class SCHFacade
    {
        private SCH proc = null;
        private static SCHFacade instance = new SCHFacade();

        SCHFacade()
        {
            proc = new SCH();
        }

        public static SCHFacade GetInstance
        {
            get
            {
                return instance;
            }
        }


        #region Single Select

        ///<summary>
        ///작성일 : 2023-11-23 오후 11:00:27
        ///수정일 : 2023-11-24 오후 2:40:28
        ///</summary>
        public ESNfx.GenericReturn<SCH_sr_SCHEDULE> GetSCHEDULE(int? sch_id)
        {
            return proc.SCH_sr_SCHEDULE(sch_id);
        }

        #endregion

        #region Multiple Select

        ///<summary>
        ///작성일 : 2023-11-22 오후 4:45:57
        ///수정일 : 2023-11-26 오후 2:04:25
        ///</summary>
        public List<SCH_sd_STAFF> InquirySTAFF(int? cpy_cd, string dept_nm, string team_nm)
        {
            return proc.SCH_sd_STAFF(cpy_cd, dept_nm, team_nm);
        }

        ///<summary>
        ///작성일 : 2023-11-23 오전 11:56:07
        ///수정일 : 2023-11-23 오후 3:15:07
        ///</summary>
        public List<SCH_sd_SCHEDULE_MAIN> InquirySCHEDULE_MAIN(iSCH_sd_SCHEDULE_MAIN param)
        {
            return proc.SCH_sd_SCHEDULE_MAIN(param);
        }

        ///<summary>
        ///작성일 : 2023-11-23 오후 3:11:38
        ///수정일 : 2023-11-26 오후 3:38:26
        ///</summary>
        public List<SCH_sd_SCHEDULE_LIST> InquirySCHEDULE_LIST(iSCH_sd_SCHEDULE_LIST param)
        {
            return proc.SCH_sd_SCHEDULE_LIST(param);
        }

        ///<summary>
        ///작성일 : 2023-11-23 오후 3:19:24
        ///수정일 : 2023-11-23 오후 3:19:24
        ///</summary>
        public List<SCH_sd_CALENDAR> InquiryCALENDAR(string search_year, string search_month)
        {
            return proc.SCH_sd_CALENDAR(search_year, search_month);
        }

        #endregion

        #region Insert / Delete / Update

        ///<summary>
        ///작성일 : 2023-11-24 오후 7:11:21
        ///수정일 : 2023-11-24 오후 7:12:22
        ///</summary>
        public ESNfx.ReturnValue UpdateSCHEDULE(iSCH_un_SCHEDULE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new SCH(txn);
                        ret = tran.SCH_un_SCHEDULE(param);
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.setCode(-1);
                    }
                    finally
                    {
                        if (ret.Result)
                        {
                            txn.Commit();
                        }
                        else
                        {
                            txn.Rollback();
                        }
                    }
                }
            }
            return ret;
        }

        ///<summary>
        ///작성일 : 2023-11-22 오후 2:17:39
        ///수정일 : 2023-11-23 오후 10:20:45
        ///</summary>
        public ESNfx.ReturnValue SCH_iu_STAFF(iSCH_iu_STAFF param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new SCH(txn);
                        ret = tran.SCH_iu_STAFF(param);
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.setCode(-1);
                    }
                    finally
                    {
                        if (ret.Result)
                        {
                            txn.Commit();
                        }
                        else
                        {
                            txn.Rollback();
                        }
                    }
                }
            }
            return ret;
        }

        ///<summary>
        ///작성일 : 2023-11-24 오후 5:51:14
        ///수정일 : 2023-11-24 오후 6:38:28
        ///</summary>
        public ESNfx.ReturnValue SCH_iu_CHECKIN(string gst_mobile_no, string msg_code)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();

                try
                {
                    var tran = new SCH();
                    ret = tran.SCH_iu_CHECKIN(gst_mobile_no, msg_code);
                }
                catch (Exception ex)
                {
                    ret.Message = ex.Message;
                    ret.setCode(-1);
                }
                finally
                {
                }

            }
            return ret;
        }

        ///<summary>
        ///작성일 : 2023-11-22 오후 1:54:57
        ///수정일 : 2023-11-25 오후 2:49:23
        ///</summary>
        public ESNfx.ReturnValue InsertSCHEDULE(iSCH_in_SCHEDULE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new SCH(txn);
                        ret = tran.SCH_in_SCHEDULE(param);
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.setCode(-1);
                    }
                    finally
                    {
                        if (ret.Result)
                        {
                            txn.Commit();
                        }
                        else
                        {
                            txn.Rollback();
                        }
                    }
                }
            }
            return ret;
        }

        #endregion

    }
}
