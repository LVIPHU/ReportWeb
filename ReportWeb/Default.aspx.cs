using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public List<string> listCol = new List<string>(){
                            "Table",
                            "Field",
                            "Total",
                            "Rename",
                            "Sort",
                            "Show",
                            "Criteria",
                            "Or",
        };

    [System.Web.Services.WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public static string getColumns(int object_id)
    {
        DataTable dt = DataTableSQL.getInfoColumn(object_id);
        string JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public static string getForeignKey(int object_id)
    {
        DataTable dt = DataTableSQL.getInfoForeignKey(object_id);
        string JSONresult = JsonConvert.SerializeObject(dt);
        return JSONresult;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public static string genSQL(
        List<string> gen_Table, List<string> gen_Field,
        List<string> gen_Sort, List<string> gen_Criteria,
        List<string> gen_Total, List<string> object_ids,
        List<string> gen_Rename, List<string> gen_Show,
        List<string> TableList, List<List<string>> gen_Or
    )
    {
        var selectFunction = new List<string>();
        var orFunction = new List<string>();
        var whereFunction = new List<string>();
        var joinFunction = new List<string>();
        var groupFunction = new List<string>();
        var sortFunction = new List<string>();

        var SortVarible = new List<string>() { "ASC", "DESC" };
        var TotalVarible = new List<string>() { "group_by", "sum", "count", "min", "max", "avg", "none"};
        var TempTable = gen_Table.Where(s => !string.IsNullOrWhiteSpace(s));

        if (
            TempTable.Count() == 0 ||
            gen_Table.Count() == 0 ||
            object_ids.Count() == 0 ||
            gen_Field.Count() == 0 ||
            gen_Table.Count() != object_ids.Count()
        ) return "ERR|Chọn ít nhất 1 bảng";


        for (var i = 0; i < gen_Table.Count(); i++)
        {

            var field = "";
            if (Utils.LimitedIn(i, gen_Field))
            {
                field = gen_Field[i].Trim();
            }

            var table = gen_Table[i].Trim();

            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(table)) continue;
            var table_field = table + "." + field;

            var rename = "";
            if (Utils.LimitedIn(i, gen_Rename))
            {
                rename = gen_Rename[i].Trim().Replace("'", "");
                if (!string.IsNullOrEmpty(rename))
                {
                    if (field == "*")
                    {
                        return "ERR|Đổi tên không áp dụng cho Field select all";
                    }

                    rename = " AS '" + rename + "'";
                }

            }

            var isTotal = false;
            var total = "";
            if (Utils.LimitedIn(i, gen_Total))
            {
                total = gen_Total[i].Trim();
                if (!string.IsNullOrEmpty(total))
                {
                    if (!TotalVarible.Contains(total))
                    {
                        return "ERR|Giá trị total không hợp lệ";
                    }

                    if (field == "*")
                    {
                        return "ERR|Giá trị Total không hợp lệ cho Field select all";
                    }
                    isTotal = true;
                }
            }

            if (gen_Show.Contains(i.ToString()))
            {
                if (isTotal && total != "group_by")
                {
                    selectFunction.Add(total.ToUpper() + "(" + table_field + ")" + rename);
                }
                else
                {
                    selectFunction.Add(table_field + rename);
                }
            }

            if (Utils.LimitedIn(i, gen_Criteria))
            {
                var criteria_value = gen_Criteria[i].Trim();
                if (!string.IsNullOrEmpty(criteria_value))
                {
                    if (field == "*")
                    {
                        return "ERR|Không thể áp dụng điều kiện criteria cho Field select all";
                    }

                    whereFunction.Add(string.Format("({0})", table_field + criteria_value));
                }
            }

            if (Utils.LimitedIn(i, gen_Sort))
            {
                var sort_value = gen_Sort[i].Trim().ToUpper();
                if (!string.IsNullOrEmpty(sort_value))
                {
                    if (!SortVarible.Contains(sort_value))
                    {
                        return "ERR|Giá trị sort không đúng cú pháp";
                    }

                    if (field == "*")
                    {
                        return "ERR|Không thể sắp xếp cho Field select all";
                    }

                    if (isTotal && total != "group_by")
                    {
                        sortFunction.Add(string.Format("{0}({1}) {2}", total.ToUpper(), table_field, sort_value));
                    }
                    else
                    {
                        sortFunction.Add(table_field + " " + sort_value);
                    }

                }
            }

            if (isTotal && total == "group_by" && !(total == "none"))
            {
                groupFunction.Add(table_field);
            }
        }


        foreach (List<string> or_elms in gen_Or)
        {
            var or_condition = new List<string>();
            for (var i = 0; i < gen_Table.Count(); i++)
            {
                var field = gen_Field[i].Trim();
                var table = gen_Table[i].Trim();
                var table_field = table + "." + field;

                if (Utils.LimitedIn(i, or_elms))
                {
                    var or_elm = or_elms[i].Trim();
                    if (!string.IsNullOrEmpty(or_elm))
                    {
                        if (field == "*")
                        {
                            return "ERR|Không thể áp dụng điều kiện or cho Field select all";
                        }

                        or_condition.Add(
                            string.Format("({0})", table_field + or_elm)
                       );
                    }
                }
            }

            if (or_condition.Count() > 0)
            {
                orFunction.Add(
                    string.Format("({0})", string.Join(" AND ", or_condition))
                );
            }
        }

        foreach (var table in TableList)
        {
            DataTable dt = DataTableSQL.getInfoForeignKey(table);
            foreach (DataRow row in dt.Rows)
            {
                var column = row["column"].ToString();
                var referenced_table = row["referenced_table"].ToString();
                var referenced_column = row["referenced_column"].ToString();

                if (!TableList.Contains(referenced_table))
                {
                    continue;
                }

                joinFunction.Add(string.Format("{0}.{1} = {2}.{3}", table, column, referenced_table, referenced_column));
            }
        }

        if (selectFunction.Count() == 0)
        {
            return "ERR|Câu lệnh SELECT phải có ít nhất 1 Field";
        }

        string query = string.Format("SELECT {0} FROM {1} ", string.Join(", ", selectFunction), string.Join(", ", TableList));

        var where = "";
        if (whereFunction.Count() > 0 && orFunction.Count() > 0)
        {
            where = string.Format("({0}) OR {1}", string.Join(" AND ", whereFunction), string.Join(" OR ", orFunction));
        }
        else if (whereFunction.Count() > 0 && orFunction.Count() == 0)
        {
            where = string.Format("{0}", string.Join(" AND ", whereFunction));
        }
        else if (whereFunction.Count() == 0 && orFunction.Count() > 0)
        {
            where = string.Format("{0}", string.Join(" OR ", orFunction));
        }

        if (joinFunction.Count() > 0 && !string.IsNullOrEmpty(where))
        {
            query += string.Format("WHERE ({0}) AND ({1}) ", string.Join(" AND ", joinFunction), where);
        }
        else if (joinFunction.Count() > 0 && string.IsNullOrEmpty(where))
        {
            query += string.Format("WHERE ({0}) ", string.Join(" AND ", joinFunction));
        }
        else if (joinFunction.Count() == 0 && !string.IsNullOrEmpty(where))
        {
            query += string.Format("WHERE ({0}) ", where);
        }

        if (groupFunction.Count() > 0)
        {
            query += string.Format("GROUP BY {0} ", string.Join(", ", groupFunction));
        }

        if (sortFunction.Count() > 0)
        {
            query += string.Format("ORDER BY {0}", string.Join(", ", sortFunction));
        }

        Debug.WriteLine(query);
        HttpContext.Current.Session["querySQL"] = query;
        return query;
    }

    [System.Web.Services.WebMethod]
    [ScriptMethod(UseHttpGet = false)]
    public static string genReport(string query, string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return "ERR|Thiếu tiêu đề báo cáo";
        }

        if (string.IsNullOrEmpty(query))
        {
            return "ERR|Thiếu câu lệnh query";
        }

        try
        {
            DataTable dt = DataTableSQL.ExecSqlDataTable(query);
            HttpContext.Current.Session["querySQL"] = query;
            HttpContext.Current.Session["title"] = title;
        }
        catch (Exception ex)
        {
            return "ERR|" + ex.Message;
        }
        return "success";
    }
}