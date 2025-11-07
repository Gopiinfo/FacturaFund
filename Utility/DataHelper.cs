using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class DataHelper
    {
        internal static T GetRecord<T>(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            T oRec = default(T);
            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();
                    foreach (var param in parameters)
                    {
                        p.Add("@" + param.Param_Name, param.Param_Value);
                    }

                    oRec = Map.Query<T>(oCon, spName, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecord<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return oRec;
        }

        public static List<T> GetRecords<T>(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            List<T> oRecList = new List<T>();
            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();
                    foreach (var param in parameters)
                    {
                        p.Add("@" + param.Param_Name, param.Param_Value);
                    }
                    Map.Settings.CommandTimeout = 60;
                    oRecList = Map.Query<T>(oCon, spName, p, commandType: CommandType.StoredProcedure).ToList();
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return oRecList;
        }

        internal static int GetIntRecord<T>(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            int iRec = 0;
            try
            {

                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();
                    foreach (var param in parameters)
                    {
                        p.Add("@" + param.Param_Name, param.Param_Value);
                    }

                    using (var reader = Map.ExecuteReader(oCon, spName, p, commandType: CommandType.StoredProcedure))
                    {
                        if (reader != null && reader.Read())
                        {
                            iRec = Convert.ToInt32(reader[0].ToString());
                        }
                    }
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return iRec;
        }

        internal static long ExecuteQuery(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            long iStatus = 0;
            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();
                    foreach (var param in parameters)
                    {
                        p.Add("@" + param.Param_Name, param.Param_Value);
                    }
                    iStatus = Map.Execute(oCon, spName, p, commandType: CommandType.StoredProcedure);
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return iStatus;
        }

        internal static int ExecuteQueryWithIntOutputParam(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            long iStatus = 0;
            long i = 0;
            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();

                    string oParam = "";
                    foreach (var param in parameters)
                    {
                        if (param.Param_Direction == "OUTPUT")
                        {
                            oParam = param.Param_Name;
                            p.Add("@" + param.Param_Name, param.Param_Value, direction: ParameterDirection.Output);
                        }
                        else
                            p.Add("@" + param.Param_Name, param.Param_Value);
                    }

                    p.Add(name: "@Returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    iStatus = Map.Execute(oCon, spName, p, commandType: CommandType.StoredProcedure);

                    if (oParam == "")
                    {
                        i = p.Get<int>("@Returnvalue");
                        if (i <= 0 && iStatus > 0)
                            i = 1;
                    }
                    else
                    {
                        i = p.Get<long>("@" + oParam);
                        if (i <= 0 && iStatus > 0)
                            i = 1;
                    }

                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            // return iStatus;
            int result = (int)i;
            return result;
        }

        internal static string ExecuteQueryWithIntOutputParamReturnString(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            long iStatus = 0;
            string i = "";

            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();

                    string oParam = "";
                    foreach (var param in parameters)
                    {
                        if (param.Param_Direction == "OUTPUT")
                        {
                            oParam = param.Param_Name;
                            p.Add("@" + param.Param_Name, param.Param_Value, direction: ParameterDirection.Output);
                        }
                        else
                            p.Add("@" + param.Param_Name, param.Param_Value);
                    }

                    p.Add(name: "@Returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    iStatus = Map.Execute(oCon, spName, p, commandType: CommandType.StoredProcedure);

                    if (oParam == "")
                    {
                        i = Convert.ToString(p.Get<int>("@Returnvalue"));
                        if (Convert.ToInt32(i) <= 0 && iStatus > 0)
                            i = "1";
                    }
                    else
                    {
                        i = p.Get<string>("@" + oParam);
                        if (i.Trim() == "" && iStatus > 0)
                            i = "1";
                    }

                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return i;
        }

        internal static List<long> ExecuteQueryWithIntListOutput(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            long iStatus = 0;
            List<long> oRec = new List<long>();

            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();

                    List<string> oParam = new List<string>();
                    foreach (var param in parameters)
                    {
                        if (param.Param_Direction == "OUTPUT")
                        {
                            oRec.Add(0);
                            oParam.Add(param.Param_Name);
                            p.Add("@" + param.Param_Name, param.Param_Value, direction: ParameterDirection.Output);
                        }
                        else
                            p.Add("@" + param.Param_Name, param.Param_Value);
                    }

                    p.Add(name: "@Returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                    iStatus = Map.Execute(oCon, spName, p, commandType: CommandType.StoredProcedure);

                    if (oParam.Count() == 0)
                    {
                        oRec[0] = p.Get<int>("@Returnvalue");
                        if (oRec[0] <= 0 && iStatus > 0)
                            oRec[0] = 1;
                    }
                    else
                    {
                        for (int j = 0; j < oRec.Count(); j++)
                        {
                            oRec[j] = p.Get<long>("@" + oParam[j]);
                        }
                    }

                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }
            return oRec;
        }

        public static void LogErrorinDB(string Login_ID, string errmoduleName, string errprocedureName, string errorObject)
        {
            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    if (oCon.State == ConnectionState.Closed) oCon.Open();
                    SqlCommand scmd = new SqlCommand
                    {
                        CommandText = Constant.SP.ErrorLog,
                        CommandType = CommandType.StoredProcedure,
                        Connection = oCon
                    };
                    //scmd.Parameters.AddWithValue("@Login_id", Convert.ToString(Login_ID).Trim() == "" ? "0" : Login_ID.ToString().Trim().Replace("'", "''"));
                    scmd.Parameters.AddWithValue("@ModuleName", errmoduleName.ToString().Trim().Replace("'", "''"));
                    scmd.Parameters.AddWithValue("@ProcName", errprocedureName.ToString().Trim().Replace("'", "''"));
                    scmd.Parameters.AddWithValue("@Description", errorObject.ToString().Trim().Replace("'", "''"));
                    scmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }
            catch (ApplicationException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (SqlException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (ArgumentException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (StackOverflowException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (TimeoutException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (SystemException)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
            catch (Exception)
            {
                //LogException.WritetoFile(Login_ID, errmoduleName, errprocedureName, errorObject, ex);
            }
        }

        internal static Tuple<List<TClass>, List<TClass2>> GetMultipleQuery<TClass, TClass2>(string spName, List<ParamInfo> parameters, string strFunctionName)
        {
            List<TClass> output1 = new List<TClass>();
            List<TClass2> output2 = new List<TClass2>();
            var oRecList = new Tuple<List<TClass>, List<TClass2>>(output1, output2);

            try
            {
                using (SqlConnection oCon = new SqlConnection(Utility.ConnectionString()))
                {
                    oCon.Open();
                    DynamicParameters p = new DynamicParameters();
                    foreach (var param in parameters)
                    {
                        p.Add("@" + param.Param_Name, param.Param_Value);
                    }
                    Map.Settings.CommandTimeout = 60;
                    using (var oRecMultiList = Map.QueryMultiple(oCon, spName, p, commandType: CommandType.StoredProcedure))
                    {
                        output1 = oRecMultiList.Read<TClass>().AsList();
                        output2 = oRecMultiList.Read<TClass2>().AsList();
                    };
                    oRecList = new Tuple<List<TClass>, List<TClass2>>(output1, output2);
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                LogErrorinDB("", "DataHelper", "GetRecords<T> " + strFunctionName.ToString(), ex.Message.ToString() + "\t\t" + spName);
            }

            return oRecList;
        }

    }
}
