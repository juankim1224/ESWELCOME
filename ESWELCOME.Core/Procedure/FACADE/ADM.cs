using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESWELCOME.DataBase.Procedure.DAL;
using ESWELCOME.DataBase.Procedure.BOL.ADM;

namespace ESWELCOME.DataBase.Procedure.Facade
{
    public class ADMFacade
    {
        private ADM proc = null;
        private static ADMFacade instance = new ADMFacade();

        ADMFacade()
        {
            proc = new ADM();
        }

        public static ADMFacade GetInstance
        {
            get
            {
                return instance;
            }
        }


        #region Single Select

        #endregion

        #region Multiple Select

        ///<summary>
        ///작성일 : 2023-11-26 오후 3:02:09
        ///수정일 : 2023-11-26 오후 3:13:35
        ///</summary>
        public List<ADM_sd_MEMBER> InquiryMEMBER(iADM_sd_MEMBER param)
        {
            return proc.ADM_sd_MEMBER(param);
        }

        #endregion

        #region Insert / Delete / Update


        ///<summary>
        ///작성일 : 2023-12-04 오후 9:45:15
        ///수정일 : 2023-12-04 오후 9:59:20
        ///</summary>
        public ESNfx.ReturnValue UpdateDeleteMEM(int? mem_id)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new ADM(txn);
                        ret = tran.ADM_un_DeleteMEM(mem_id);
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
