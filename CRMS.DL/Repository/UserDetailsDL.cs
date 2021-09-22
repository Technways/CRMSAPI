using CRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data;

namespace CRMS.DL.Repository
{
    public class UserDetailsDL : IUserDetailsDL
    {

        public async Task<List<UserResponse>> GetUserDetails()
        {

            return await GetAllUserDetailsAsync();
        }
        public async Task<UserResponse> GetUserDetailsByIdAsync(int Id)
        {
            UserResponse result = new UserResponse();
            result = await GetUserDetailsAsync(Id);
            return result;
        }

        public async Task<bool> DeleteUserDetailsByIdAsync(int Id)
        {
            bool result = await DeleteUserDetailsAsync(Id);
            return result;
        }

        public async Task<List<UserResponse>> GetAllUserDetailsAsync()
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            List<UserResponse> userDetail = new List<UserResponse>();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "select * from userdetails";//"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";
                                                                      // cmd.Parameters.Add("i_CUSTOMERID", OracleDbType.Int32, CustomerID, ParameterDirection.Input);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            UserResponse _user = new UserResponse();
                            _user.UserId = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                            _user.FirstName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                            _user.LastName = await reader.IsDBNullAsync(2) ? null : reader.GetString(2);
                            _user.Mobile = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3));
                            _user.Country = await reader.IsDBNullAsync(4) ? null : reader.GetString(4);
                            _user.Email = await reader.IsDBNullAsync(5) ? null : reader.GetString(5);
                            _user.PackageId = await reader.IsDBNullAsync(6) ? 0 : Convert.ToInt32(reader.GetString(6));


                            userDetail.Add(_user);
                        }
                        await reader.DisposeAsync();
                        return userDetail;
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

        public async Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest)
        {
            
            return await CreateCRMSUserAsync(createUserRequest); ;
        }

        public async Task<bool> DeleteUserDetailsAsync(int CustomerID)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            UserResponse userDetail = new UserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "Delete from userdetails where userid = " + CustomerID;//"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";
                                                                                                   // cmd.Parameters.Add("i_CUSTOMERID", OracleDbType.Int32, CustomerID, ParameterDirection.Input);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        await reader.DisposeAsync();
                        return true;
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

        public async Task<UserResponse> GetUserDetailsAsync(int CustomerID)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            UserResponse userDetail = new UserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandText = "select * from userdetails where userid = "+ CustomerID;//"PKG_PAYU_MANAGER.PROC_PAYU_IS_CUSTOMER_EXISTS";
                                                                      // cmd.Parameters.Add("i_CUSTOMERID", OracleDbType.Int32, CustomerID, ParameterDirection.Input);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            UserResponse _user = new UserResponse();
                            userDetail.Mobile = await reader.IsDBNullAsync(0) ? 0 : Convert.ToInt32(reader.GetString(0));
                            userDetail.FirstName = await reader.IsDBNullAsync(1) ? null : reader.GetString(1);
                            userDetail.LastName = await reader.IsDBNullAsync(2) ? null : reader.GetString(2);
                            userDetail.Mobile = await reader.IsDBNullAsync(3) ? 0 : Convert.ToInt32(reader.GetString(3));
                            userDetail.Country = await reader.IsDBNullAsync(4) ? null : reader.GetString(4);
                            userDetail.Email = await reader.IsDBNullAsync(5) ? null : reader.GetString(5);
                            userDetail.PackageId = await reader.IsDBNullAsync(6) ? 0 : Convert.ToInt32(reader.GetString(6));


                            //userList.Add(_user);
                        }
                        await reader.DisposeAsync();
                        return userDetail;
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

        public async Task<CreateUserResponse> CreateCRMSUserAsync(CreateUserRequest createUserRequest)
        {
            string connectionString = "User Id=SYSTEM;Password=admin;Data Source=DESKTOP-6PD0NT9:1521/XE:SYSTEM";
            OracleConnection oracleConnection = null;
            CreateUserResponse userDetail = new CreateUserResponse();
            try
            {
                using (oracleConnection = new OracleConnection(connectionString))
                {
                    oracleConnection.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = oracleConnection;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "CRMS_SP_INSERT_USERDETAILS";
                        cmd.Parameters.Add("FIRSTNAME", OracleDbType.Varchar2, createUserRequest.FirstName, ParameterDirection.Input);
                        cmd.Parameters.Add("LASTNAME", OracleDbType.Varchar2, createUserRequest.LastName, ParameterDirection.Input);
                        cmd.Parameters.Add("COUNTRY", OracleDbType.Varchar2, createUserRequest.Country, ParameterDirection.Input);
                        cmd.Parameters.Add("MOBILE", OracleDbType.Int32, createUserRequest.Mobile, ParameterDirection.Input);
                        cmd.Parameters.Add("EMAIL", OracleDbType.Varchar2, createUserRequest.Email, ParameterDirection.Input);
                        cmd.Parameters.Add("PACKAGEID", OracleDbType.Int32, createUserRequest.PackageId, ParameterDirection.Input);
                        cmd.Parameters.Add("USERID", OracleDbType.Int32, ParameterDirection.Output);

                        DbDataReader reader = await cmd.ExecuteReaderAsync();

                        userDetail.UserId = Convert.ToInt32(((Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters[6].Value).Value);

                        await reader.DisposeAsync();
                        return userDetail;
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
