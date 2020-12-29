using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace skelot
{
    public partial class PrintBill : Form
    {
        public PrintBill()
        {
            InitializeComponent();
        }

        private void PrintBill_Load(object sender, EventArgs e)
        {

            //LocalReport report = new LocalReport();
            //report.ReportPath = @"C:\Users\khizar.ali\Downloads\salesandinventory\SalesAndInventory\Sample\salesAndInventory\Report\Bill.rdlc";
            //report.Refresh();
            //this.reportViewer1.LocalReport.ReportPath = @"C:\Users\khizar.ali\Downloads\salesandinventory\SalesAndInventory\Sample\salesAndInventory\Report\Bill.rdlc"; ;
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();

        }
    }
}
