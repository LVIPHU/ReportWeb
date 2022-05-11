using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DataTableSQL
{
    public static DataTable ExecSqlDataTable(string cmd)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection Conn = new SqlConnection(connectionString);
        Conn.Open();
        DataTable dt = new DataTable();
        if (Conn.State == ConnectionState.Closed) Conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd, Conn);
        da.Fill(dt);
        Conn.Close();
        return dt;
    }

    public static DataTable getInfoForeignKey(int object_id)
    {
        string lenh = string.Format("EXEC sp_GetForeignKey @OBJECT_ID = {0}", object_id);
        return ExecSqlDataTable(lenh);
    }

    public static DataTable getInfoForeignKey(string table_name)
    {
        string lenh = string.Format("EXEC sp_GetForeignKeyByTableName @TABLE_NAME = N'{0}'", table_name);
        return ExecSqlDataTable(lenh);
    }

    public static DataTable getInfoColumn(int object_id)
    {
        string lenh = string.Format("EXEC sp_GetColumn @OBJECT_ID = {0}", object_id);
        return ExecSqlDataTable(lenh);
    }
}