using DevExpress.XtraReports.Web;
using System;
using System.Data;

public partial class Viewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["querySQL"] != null && Session["title"] != null)
        {
            var title = Session["title"].ToString();
            var sql = Session["querySQL"].ToString();

            DataTable dt = DataTableSQL.ExecSqlDataTable(sql);

            var cachedReportSource = new CachedReportSourceWeb(new XtraReport(title, dt));
            documentViewer.OpenReport(cachedReportSource);
        }
        else
        {
            Response.Redirect("/");
        }
    }
}