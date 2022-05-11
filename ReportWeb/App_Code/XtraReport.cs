using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.XtraPrinting.Drawing;

/// <summary>
/// Summary description for XtraReport1
/// </summary>
public class XtraReport : DevExpress.XtraReports.UI.XtraReport
{
    private TopMarginBand TopMargin;
    private BottomMarginBand BottomMargin;
    private DetailBand Detail;
    private ReportHeaderBand ReportHeader;
    private XRLabel xrLabel;
    private GroupHeaderBand GroupHeader;
    private XRTable table1;
    private XRTable table2;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private XRTableCell GetTableCellHeader(int index, string text)
    {
        XRTableCell tableCell = new XRTableCell();
        tableCell.BackColor = Color.Transparent;
        tableCell.BorderColor = Color.Black;
        tableCell.Borders = ((BorderSide)((((BorderSide.Left | BorderSide.Top) | BorderSide.Right) | BorderSide.Bottom)));
        tableCell.BorderWidth = 1F;
        tableCell.ForeColor = Color.Black;
        tableCell.Font = new Font("Arial", 8.25F, FontStyle.Bold);
        tableCell.Name = "tableCell_" + index;
        tableCell.Padding = new PaddingInfo(6, 6, 3, 3, 100F);
        tableCell.StylePriority.UseBackColor = false;
        tableCell.StylePriority.UseBorderColor = false;
        tableCell.StylePriority.UseBorders = false;
        tableCell.StylePriority.UseBorderWidth = false;
        tableCell.StylePriority.UseForeColor = false;
        tableCell.StylePriority.UseTextAlignment = false;
        tableCell.Text = text;
        tableCell.TextAlignment = TextAlignment.MiddleCenter;
        tableCell.Weight = 0.0690971744507255D;

        return tableCell;
    }

    private XRTableCell GetTableCellData(int index, string text, bool center, bool isOdd)
    {
        XRTableCell tableCell = new XRTableCell();
        if (isOdd)
        {
            tableCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
        }
        else
        {
            tableCell.BackColor = Color.Transparent;
        }
        tableCell.BorderColor = Color.Black;
        tableCell.Borders = ((BorderSide)(((BorderSide.Left | BorderSide.Right) | BorderSide.Bottom)));
        tableCell.BorderWidth = 1F;
        tableCell.Name = "tableCellData_" + index;
        tableCell.Padding = new PaddingInfo(6, 6, 3, 3, 100F);
        tableCell.Font = new Font("Arial", 8.25F);
        tableCell.StylePriority.UseBackColor = false;
        tableCell.StylePriority.UseBorderColor = false;
        tableCell.StylePriority.UseBorders = false;
        tableCell.StylePriority.UseBorderWidth = false;
        tableCell.Text = text;
        if (center)
        {
            tableCell.StylePriority.UseTextAlignment = false;
            tableCell.TextAlignment = TextAlignment.MiddleCenter;
        }
        tableCell.Weight = 0.0690971744507255D;

        return tableCell;
    }

    private XRTable GetTable(int index)
    {
        XRTable table = new XRTable();
        table.LocationFloat = new PointFloat(0F, 0F);
        table.Name = "table_" + index;
        table.SizeF = new SizeF(650F, 28F);

        return table;
    }

    private XRTableRow GetTableRow(int index)
    {
        XRTableRow tableRow = new XRTableRow();
        tableRow.Name = "tableRow1";
        tableRow.Weight = 1D;

        return tableRow;
    }

    public XtraReport(string title, DataTable dt)
    {
        InitializeComponent();

        xrLabel.Text = title;

        //DataSource = dt;

        // Tạo 1 dòng 
        XRTableRow tableRow1 = GetTableRow(1);

        // tạo các cell để chứa tên các cột
        var index = 0;
        foreach (DataColumn col in dt.Columns)
        {
            XRTableCell tableCell = GetTableCellHeader(index, col.ColumnName.ToString());

            // thêm các ô vào dòng
            tableRow1.Cells.Add(tableCell);
            index++;
        }
        // add dòng vào bảng
        table1.Rows.Add(tableRow1);
        GroupHeader.Controls.Add(table1);


        var index2 = 3;
        foreach (DataRow row in dt.Rows)
        {
            XRTableRow tableRow = GetTableRow(index2);
            foreach (object obj in row.ItemArray)
            {
                XRTableCell tableCell = GetTableCellData(index, obj.ToString(), Utils.Numerical(obj.ToString()), index2 % 2 == 0);
                // thêm các ô vào dòng
                tableRow.Cells.Add(tableCell);
                index++;
            }
            table2.Rows.Add(tableRow);
            index2++;
        }

        Detail.Controls.Add(table2);
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        // string resourceFileName = "XtraReport.resx";
        this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
        this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrLabel = new DevExpress.XtraReports.UI.XRLabel();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.GroupHeader = new DevExpress.XtraReports.UI.GroupHeaderBand();
        this.table1 = new DevExpress.XtraReports.UI.XRTable();
        this.table2 = new DevExpress.XtraReports.UI.XRTable();

        ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.table2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // TopMargin
        // 
        this.TopMargin.Name = "TopMargin";
        // 
        // BottomMargin
        // 
        this.BottomMargin.Name = "BottomMargin";
        // 
        // Detail
        // 
        this.Detail.HeightF = 25F;
        this.Detail.Name = "Detail";
        // 
        // xrLabel
        // 
        this.xrLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel.LocationFloat = new DevExpress.Utils.PointFloat(24.58333F, 9.999993F);
        this.xrLabel.Multiline = true;
        this.xrLabel.Name = "xrLabel";
        this.xrLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
        this.xrLabel.SizeF = new System.Drawing.SizeF(600.709F, 24.42544F);
        this.xrLabel.StylePriority.UseFont = false;
        this.xrLabel.StylePriority.UseTextAlignment = false;
        this.xrLabel.Text = "xrLabel";
        this.xrLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel});
        this.ReportHeader.HeightF = 56.79823F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // GroupHeader
        // 
        this.GroupHeader.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
        this.GroupHeader.HeightF = 28F;
        this.GroupHeader.Name = "GroupHeader";
        // 
        // table1
        // 
        this.table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.table1.Name = "table1";
        this.table1.SizeF = new System.Drawing.SizeF(650F, 28F);
        // 
        // table2
        // 
        this.table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
        this.table2.Name = "table2";
        this.table2.SizeF = new System.Drawing.SizeF(650F, 25F);
        // 
        // XtraReport
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.GroupHeader});
        this.Font = new System.Drawing.Font("Arial", 9.75F);
        this.Version = "19.2";
        ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.table2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
}
