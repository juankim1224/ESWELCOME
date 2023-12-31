﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESWELCOME.DataBase.Procedure.DAL;
using ESWELCOME.DataBase.Procedure.BOL.SCH;
using ESWELCOME.Core;
using ESWELCOME.DataBase.Procedure.BOL.MSG;

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
        ///수정일 : 2023-11-30 오후 10:51:58
        ///</summary>
        public ESNfx.GenericReturn<SCH_sr_SCHEDULE> GetSCHEDULE(int? sch_id)
        {
            return proc.SCH_sr_SCHEDULE(sch_id);
        }

        #endregion

        #region Multiple Select

        ///<summary>
        ///작성일 : 2023-12-01 오후 4:48:33
        ///수정일 : 2023-12-01 오후 4:56:10
        ///</summary>
        public List<SCH_sd_StaffForSchId> InquiryStaffForSchId(int? sch_id)
        {
            return proc.SCH_sd_StaffForSchId(sch_id);
        }

        ///<summary>
        ///작성일 : 2023-11-22 오후 4:45:57
        ///수정일 : 2023-11-29 오후 5:41:49
        ///</summary>
        public List<SCH_sd_STAFF> InquirySTAFF(int? cpy_cd, string dept_nm, string team_nm)
        {
            return proc.SCH_sd_STAFF(cpy_cd, dept_nm, team_nm);
        }

        ///<summary>
        ///작성일 : 2023-12-03 오후 7:10:52
        ///수정일 : 2023-12-03 오후 7:10:52
        ///</summary>
        public List<SCH_sd_SelectStaff> InquirySelectStaff(int? sch_id, string arr_staff_id)
        {
            return proc.SCH_sd_SelectStaff(sch_id, arr_staff_id);
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
        ///수정일 : 2023-11-26 오후 3:59:38
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
        ///수정일 : 2023-11-27 오전 10:59:49
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
        ///수정일 : 2023-11-30 오후 11:16:50
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


        /// <summary>
        ///  스케줄 등록 & 접견인 등록 & 메세지 등록 3개 프로시저 호출
        /// </summary>
        public ESNfx.ReturnValue RegisterSchedule(iSCH_in_SCHEDULE shedule, List<iSCH_iu_STAFF> staff, iMSG_iu_MESSAGE message, string msgStaff, out string schpk)
        {
            schpk = string.Empty;

            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new SCH(txn);
                        // Step1. 스케줄 등록 SCH_SCHEDULE INSERT
                        ret = tran.SCH_in_SCHEDULE(shedule);
                        if (!ret.Result)
                            throw new Exception(ret.Message);

                        var msgCode = ret["@msgCode"].ToString();
                        schpk = ret["@sch_pk"].ToString();

                        message.SCH_ID = Convert.ToInt32(schpk);

                        // 메세지 내용 만들기
                        var content = MsgMaker.MakeMsgContent(shedule.SCH_TYPE, shedule.GST_CPY, shedule.GST_PST, shedule.GST_NAME, shedule.SCH_YEARMD, shedule.SCH_HOUR, shedule.SCH_MIN, msgCode, msgStaff);
                        message.MSG_CONTENT = content;

                        foreach (var s in staff)
                        {
                            s.SCH_ID = Convert.ToInt32(schpk);
                            // Step2. 접견인 등록 SCH_STAFF INSERT
                            ret = tran.SCH_iu_STAFF(s);
                            if (!ret.Result)
                                throw new Exception(ret.Message);
                        }

                        // Step3. 메세지 등록 MSG_MESSAGE INSERT
                        var tran2 = new MSG(txn);
                        ret = tran2.MSG_iu_MESSAGE(message);
                        if (!ret.Result)
                            throw new Exception(ret.Message);
                    }
                    catch (Exception ex)
                    {
                        ret.Message = ex.Message;
                        ret.setCode(-1);
                        schpk = string.Empty;
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


        // <summary>
        //  스케줄 수정 & 접견인 수정 & 메세지 수정 3개 프로시저 호출
        // schId로 sr SCH 돌렸을 때 오류남 -> msgCode 별도 가져옴
        // </summary>
        public ESNfx.ReturnValue EditSchedule(iSCH_un_SCHEDULE shedule, List<iSCH_iu_STAFF> staff, iMSG_iu_MESSAGE message, string msgStaff, string msgCode, int schId)
        {

            int schpk = Convert.ToInt32(shedule.SCH_ID);

            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new SCH(txn);
                        // Step1. 스케줄 수정 SCH_SCHEDULE UPDATE
                        ret = tran.SCH_un_SCHEDULE(shedule);

                        if (!ret.Result)
                            throw new Exception(ret.Message);

                        // Step2-1. 접견인 등록 SCH_STAFF INSERT
                        string arrStfId = null;

                        foreach (var s in staff)
                        {
                            s.SCH_ID = schpk;
                            arrStfId += s.MEM_ID + ",";

                            ret = tran.SCH_iu_STAFF(s);
                            if (!ret.Result)
                                throw new Exception(ret.Message);
                        }

                        // Step2-2. 수정 대상 없는 접견인 삭제

                        // 여기에서 자꾸 시간 초과됨
                        ret = SCHFacade.GetInstance.UpdateSCHSTAFF(arrStfId, schpk, shedule.CRE_MEMID);
                        if (!ret.Result)
                            throw new Exception(ret.Message);


                        // Step3. 메세지 수정 MSG_MESSAGE UPDATE

                        // 메세지 내용 만들기
                        message.SCH_ID = schpk;

                        var content = MsgMaker.MakeMsgContent(shedule.SCH_TYPE, shedule.GST_CPY, shedule.GST_PST, shedule.GST_NAME, shedule.SCH_YEARMD, shedule.SCH_HOUR, shedule.SCH_MIN, msgCode, msgStaff);
                        message.MSG_CONTENT = content;

                        var tran2 = new MSG(txn);
                        ret = tran2.MSG_iu_MESSAGE(message);
                        if (!ret.Result)
                            throw new Exception(ret.Message);
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
        ///작성일 : 2023-12-05 오후 4:22:04
        ///수정일 : 2023-12-05 오후 5:41:08
        ///</summary>
        public ESNfx.ReturnValue UpdateSCHSTAFF(string update_staff_id, int? sch_id, int? cre_memid)
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
                        ret = tran.SCH_un_SCHSTAFF(update_staff_id, sch_id, cre_memid);
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
