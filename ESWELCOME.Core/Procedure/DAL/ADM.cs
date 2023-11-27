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



    }
}
