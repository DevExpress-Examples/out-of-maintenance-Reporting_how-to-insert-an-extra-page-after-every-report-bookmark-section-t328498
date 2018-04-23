using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraCharts;
using System.Collections;

namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private XtraReport GetReport() {
            XtraReport mainReport = new XtraReportMain();
            mainReport.AfterPrint += mainReport_AfterPrint;
            return mainReport;
        }
        void mainReport_AfterPrint(object sender, EventArgs e) {            
            XtraReport report = sender as XtraReport;
            int insertAt = 0;
            XtraReportInformation targetReport;
            PageList pages = report.Pages;

            List<int> pageIndices = GetPageIndices(report);

            for(int i = pageIndices.Count - 1; i >= 0; i--) {
                targetReport = new XtraReportInformation();
                targetReport.CreateDocument(false);
                insertAt = pageIndices[i] + 1;
                MergeReport(report, targetReport, insertAt);
            }
        }
        private List<int> GetPageIndices(XtraReport report) {
            List<int> pageIndices = new List<int>();
            for(int i = 0; i < report.PrintingSystem.Document.BookmarkNodes.Count; i++) {
                pageIndices.Add(GetIndex(report.PrintingSystem.Document.BookmarkNodes[i]));
            }
            return pageIndices;
        }
        private int GetIndex(BookmarkNode n) {
            if(n != null) {
                if(n.Nodes.Count == 0) {
                    return n.PageIndex;
                } else {                    
                 return GetIndex(n.Nodes[n.Nodes.Count-1]);
                }
            }
            return -1;
        }
        public XtraReport MergeReport(XtraReport oReport, XtraReport oReportToMerge, int InsertAtIndex) {
            for(int page = oReportToMerge.Pages.Count - 1; page >= 0; page--) {
                oReport.Pages.Insert(InsertAtIndex, oReportToMerge.Pages[page]);
            }
            oReport.PrintingSystem.ContinuousPageNumbering = true;
            return oReport;
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            XtraReport report = new XtraReportMain();
            new ReportPrintTool(report).ShowRibbonPreviewDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            XtraReport report = GetReport();
            new ReportPrintTool(report).ShowRibbonPreviewDialog();
        }
    }
}
