using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcherCore
{
   public class General
    {
        public static string GetTimeServer()
        {
            string stringtime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string idgenerate = stringtime.Replace("/", "").Replace(" ", "").Replace(":", "");
            return idgenerate;
        }
        
    }
}
