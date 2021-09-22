using CRMS.DL.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DL.Repository
{
   public class AuthorizationDL : IAuthorizationDL
    {
        public async Task<int> ValidateUser(string Email, string Password)
        {
            return await ValidateUserAsync(Email, Password);
        }

        public async Task<int> ValidateUserAsync(string Email, string Password)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            int userID = 0;
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "SELECT USERID FROM CRMSVALIDUSER WHERE EMAIL = " + string.Format("'{0}'", Email) + " AND PASSWORD = " + string.Format("'{0}'", Password);  //"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {

                            userID = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                            //userList.Add(_user);
                        }
                        await reader.DisposeAsync();
                        return userID;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (oracleConnection != null)
                { oracleConnection.Close(); }
            }

        }
    }
}
