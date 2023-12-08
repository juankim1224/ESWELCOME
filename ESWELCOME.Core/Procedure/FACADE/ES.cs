using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESWELCOME.DataBase.Procedure.DAL;
using ESWELCOME.DataBase.Procedure.BOL.ES;

namespace ESWELCOME.DataBase.Procedure.Facade
{
    public class ESFacade
    {
        private ES proc = null;
        private static ESFacade instance = new ESFacade();

        ESFacade()
        {
            proc = new ES();
        }

        public static ESFacade GetInstance
        {
            get
            {
                return instance;
            }
        }


        #region Single Select

        ///<summary>
        ///작성일 : 2023-12-08 오전 11:49:20
        ///수정일 : 2023-12-08 오후 1:34:28
        ///</summary>
        public ESNfx.GenericReturn<ES_sr_MemLoginUser> GetMemLoginUser(string loginid, string password)
        {
            return proc.ES_sr_MemLoginUser(loginid, password);
        }


        ///<summary>
        ///작성일 : 2023-11-27 오전 10:53:47
        ///수정일 : 2023-11-27 오전 10:54:55
        ///</summary>
        public ESNfx.GenericReturn<ES_sr_MEMBER> GetMEMBER(int? mem_id)
        {
            return proc.ES_sr_MEMBER(mem_id);
        }


        #endregion

        #region Multiple Select

        #endregion

        #region Insert / Delete / Update

        ///<summary>
        ///작성일 : 2023-11-26 오후 5:46:24
        ///수정일 : 2023-11-26 오후 5:50:27
        ///</summary>
        public ESNfx.ReturnValue UpdateLOGIN_PW(iES_un_LOGIN_PW param)
        {
            ESNfx.ReturnValue ret = new ESNfx.ReturnValue();

            using (SqlConnection con = new SqlConnection(proc.GetBaseConnectionString))
            {
                con.Open();
                using (IDbTransaction txn = con.BeginTransaction())
                {
                    try
                    {
                        var tran = new ES(txn);
                        ret = tran.ES_un_LOGIN_PW(param);
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
