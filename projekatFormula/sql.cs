using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace Formula
{
    internal class sql {
        public static SqlConnection vrati_vezu()
        {
            string CS;
            CS = ConfigurationManager.ConnectionStrings["f1"].ConnectionString;
            return new SqlConnection(CS);
        }

        public static string imgFromResource( string img ) {
            String strAppPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            String strFilePath = Path.Combine(strAppPath, "Resources");
            String strFullFilename = Path.Combine(strFilePath, img + ".png");

            return strFullFilename;
        }
    }
}
