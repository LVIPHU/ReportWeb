using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    public List<Table> listTables = new List<Table>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lenh = "SELECT name, object_id FROM NGANHANG.sys.Tables WHERE is_ms_shipped = 0 AND name != 'sysdiagrams'";
        DataTable dt = DataTableSQL.ExecSqlDataTable(lenh);
        ListTable.DataSource = dt;
        ListTable.DataBind();
    }
}
