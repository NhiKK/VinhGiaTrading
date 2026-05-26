using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Mbg
{
    namespace Data
    {
        namespace Util
        {
            public class SqlClient
            {
                private SqlConnection sqlCnn;
                
                public bool SetSqlConnection(SqlConnection connection)
                {
                    try
                    {
                        sqlCnn = connection;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

               /* public DataTable GetDataTable(String sql, params object[] nameValue)
                {
                    return Fill(new DataTable(), sql, nameValue);
                }

                public SqlDataReader ExecuteReader(String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        cmd.Connection.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        return rd;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        cmd.Connection.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        return rd;
                    }
                }

                public int ExcuteNonQuery(String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        return rows;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        return rows;
                    }
                }

                public object ExecuteScalar(String sql, params object[] parameters)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, parameters);
                        cmd.Connection.Open();
                        object value = cmd.ExecuteScalar();
                        cmd.Connection.Close();
                        return value;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, parameters);
                        cmd.Connection.Open();
                        object value = cmd.ExecuteScalar();
                        cmd.Connection.Close();
                        return value;
                    }
                }

                public DataTable Fill(DataTable dt, String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        return dt;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        return dt;
                    }
                }

                public SqlCommand CreateCommand(String sql, bool isProcedure, params object[] nameValue)
                {
                    SqlCommand Command = new SqlCommand(sql, sqlCnn);

                    if (isProcedure)
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        Command.CommandType = CommandType.Text;
                    }

                    for (int i = 0; i < nameValue.Length; i += 2)
                    {
                        Command.Parameters.AddWithValue(nameValue[i].ToString(), nameValue[i + 1].ToString());
                    }
                    return Command;
                }*/
            
				public DataTable GetDataTable(String sql, params object[] nameValue)
                {
                    return Fill(new DataTable(), sql, nameValue);
                }

                public SqlDataReader ExecuteReader(String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        return rd;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        return rd;
                    }
                }

                public int ExcuteNonQuery(String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        return rows;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        return rows;
                    }
                }

                public object ExecuteScalar(String sql, params object[] parameters)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, parameters);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        object value = cmd.ExecuteScalar();
                        cmd.Connection.Close();
                        return value;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, parameters);
                        cmd.CommandTimeout = 50000;
                        cmd.Connection.Open();
                        object value = cmd.ExecuteScalar();
                        cmd.Connection.Close();
                        return value;
                    }
                }

                public DataTable Fill(DataTable dt, String sql, params object[] nameValue)
                {
                    try
                    {
                        SqlCommand cmd = CreateCommand(sql, false, nameValue);
                        cmd.CommandTimeout = 50000;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        return dt;
                    }
                    catch
                    {
                        SqlCommand cmd = CreateCommand(sql, true, nameValue);
                        cmd.CommandTimeout = 50000;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        return dt;
                    }
                }

                public SqlCommand CreateCommand(String sql, bool isProcedure, params object[] nameValue)
                {
                    SqlCommand Command = new SqlCommand(sql, sqlCnn);
                    Command.CommandTimeout = 50000;
                    if (isProcedure)
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        Command.CommandType = CommandType.Text;
                    }

                    for (int i = 0; i < nameValue.Length; i += 2)
                    {
                        Command.Parameters.AddWithValue(nameValue[i].ToString(), nameValue[i + 1].ToString());
                    }
                    return Command;
                }
			
			}
        }
    }
}