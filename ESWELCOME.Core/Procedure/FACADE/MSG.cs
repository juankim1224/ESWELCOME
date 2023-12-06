using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESWELCOME.DataBase.Procedure.DAL;
using ESWELCOME.DataBase.Procedure.BOL.MSG;

namespace ESWELCOME.DataBase.Procedure.Facade
{
    public class MSGFacade
    {
        private MSG proc = null;
        private static MSGFacade instance = new MSGFacade();

        MSGFacade()
        {
            proc = new MSG();
        }

        public static MSGFacade GetInstance
        {
            get
            {
                return instance;
            }
        }


        #region Single Select

        ///<summary>
        ///작성일 : 2023-11-25 오후 4:01:06
        ///수정일 : 2023-12-06 오후 2:01:00
        ///</summary>
        public ESNfx.GenericReturn<MSG_sr_STAFFHP> GetSTAFFHP(int? sch_id)
        {
            return proc.MSG_sr_STAFFHP(sch_id);
        }

        #endregion

        #region Insert / Delete / Update

        ///<summary>
        ///작성일 : 2023-11-23 오후 10:13:55
        ///수정일 : 2023-12-05 오후 10:02:24
        ///</summary>
        public ESNfx.ReturnValue MSG_iu_MESSAGE(iMSG_iu_MESSAGE param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new MSG(txn);
                        ret = tran.MSG_iu_MESSAGE(param);
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
