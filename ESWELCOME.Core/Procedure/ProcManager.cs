using ESWELCOME.DataBase.Procedure.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESWELCOME.Core.Procedure
{
    public class ProcManager
    {
        private static ProcManager _instance;

        static ProcManager()
        {
            _instance = new ProcManager();
        }

        public static ProcManager Proc
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 1. ADM Facade
        /// </summary>
        public ADMFacade ADMFacade
        {
            get
            {
                return ADMFacade.GetInstance;
            }
        }

        /// <summary>
        /// 2. ES Facade
        /// </summary>
        public ESFacade ESFacade
        {
            get
            {
                return ESFacade.GetInstance;
            }
        }

        /// <summary>
        /// 3. SCH Facade
        /// </summary>
        public SCHFacade SCHFacade
        {
            get
            {
                return SCHFacade.GetInstance;
            }
        }

        /// <summary>
        /// 4. MSG Facade
        /// </summary>
        public MSGFacade MSGFacade
        {
            get
            {
                return MSGFacade.GetInstance;
            }
        }
    }
}
