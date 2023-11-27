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

        #endregion

    }
}
