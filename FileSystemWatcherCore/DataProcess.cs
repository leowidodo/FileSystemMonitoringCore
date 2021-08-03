using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelperForCore;

namespace FileSystemWatcherCore
{
    public class DataProcess
    {
        public static  string InsertFileMonitoring(string pathfile, string namafile, string filesize, string key, string process, string FileFrom, string FileTo, string User, string computername)
        {
     
            DBParams dbprocess = new DBParams();
            dbprocess.AddParameter("@PATH_FILE", pathfile);
            dbprocess.AddParameter("@NAMA_FILE", namafile);
            dbprocess.AddParameter("@FILE_SIZE", filesize);
            dbprocess.AddParameter("@UNIQUE_KEY", key);
            dbprocess.AddParameter("@SYSTEM_PROCESS", process);
            dbprocess.AddParameter("@FILE_FROM", FileFrom);
            dbprocess.AddParameter("@FILE_TO", FileTo);
            dbprocess.AddParameter("@COMPUTER_NAME", computername);
            dbprocess.AddParameter("@USER", User);

            // DBConn.Conn.InsertUpdateDeleteProcedure("INSERT_TR_FILE_MONITORING", dbprocess);

            int prhasil = DBConn.Conn.InsertUpdateDeleteProcedure("INSERT_TR_FILE_MONITORING", dbprocess);
            if (prhasil > 0)
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }

    }
}
