using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace EmbeddedMDFApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var dataDir = AppDomain.CurrentDomain.BaseDirectory;

            if (dataDir.EndsWith(@"\bin\Debug\") || dataDir.EndsWith(@"\bin\Release\"))
            {
                dataDir = System.IO.Directory.GetParent(dataDir).Parent.Parent.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            }
            using (SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\ProjectsV12;AttachDbFilename=|DataDirectory|\TestDatabase.mdf;Integrated Security=True"))
            {
                conn.Open();
                
                string strRead = "SELECT * FROM Role";
                using (SqlCommand sqlCmd = new SqlCommand(strRead, conn))
                {
                    using (SqlDataReader sdr = sqlCmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            var roleID = sdr.GetInt32(sdr.GetOrdinal("RoleID"));  
                            var roleName = sdr.GetString(sdr.GetOrdinal("RoleName"));
                            var roleGroupID = sdr.GetInt32(sdr.GetOrdinal("RoleGroupID"));
                            Console.WriteLine("{0}:{1}, {2}", roleID, roleName, roleGroupID);
                            
                        }
                    }
                }
                conn.Close();
            }

            Console.ReadLine();
        }
    
    }
}
