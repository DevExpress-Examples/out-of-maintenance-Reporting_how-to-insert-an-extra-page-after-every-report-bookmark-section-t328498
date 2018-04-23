Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports System.Diagnostics
Imports DevExpress.XtraPrinting
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.UserDesigner
Imports DevExpress.XtraCharts
Imports System.Collections

Namespace WindowsFormsApplication1
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Function GetReport() As XtraReport
            Dim mainReport As XtraReport = New XtraReportMain()
            AddHandler mainReport.AfterPrint, AddressOf mainReport_AfterPrint
            Return mainReport
        End Function
        Private Sub mainReport_AfterPrint(ByVal sender As Object, ByVal e As EventArgs)
            Dim report As XtraReport = TryCast(sender, XtraReport)
            Dim insertAt As Integer = 0
            Dim targetReport As XtraReportInformation
            Dim pages As PageList = report.Pages

            Dim pageIndices As List(Of Integer) = GetPageIndices(report)

            For i As Integer = pageIndices.Count - 1 To 0 Step -1
                targetReport = New XtraReportInformation()
                targetReport.CreateDocument(False)
                insertAt = pageIndices(i) + 1
                MergeReport(report, targetReport, insertAt)
            Next i
        End Sub
        Private Function GetPageIndices(ByVal report As XtraReport) As List(Of Integer)
            Dim pageIndices As New List(Of Integer)()
            For i As Integer = 0 To report.PrintingSystem.Document.BookmarkNodes.Count - 1
                pageIndices.Add(GetIndex(report.PrintingSystem.Document.BookmarkNodes(i)))
            Next i
            Return pageIndices
        End Function
        Private Function GetIndex(ByVal n As BookmarkNode) As Integer
            If n IsNot Nothing Then
                If n.Nodes.Count = 0 Then
                    Return n.PageIndex
                Else
                 Return GetIndex(n.Nodes(n.Nodes.Count-1))
                End If
            End If
            Return -1
        End Function
        Public Function MergeReport(ByVal oReport As XtraReport, ByVal oReportToMerge As XtraReport, ByVal InsertAtIndex As Integer) As XtraReport
            For page As Integer = oReportToMerge.Pages.Count - 1 To 0 Step -1
                oReport.Pages.Insert(InsertAtIndex, oReportToMerge.Pages(page))
            Next page
            oReport.PrintingSystem.ContinuousPageNumbering = True
            Return oReport
        End Function

        Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
            Dim report As XtraReport = New XtraReportMain()
            CType(New ReportPrintTool(report), ReportPrintTool).ShowRibbonPreviewDialog()
        End Sub

        Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton2.Click
            Dim report As XtraReport = GetReport()
            CType(New ReportPrintTool(report), ReportPrintTool).ShowRibbonPreviewDialog()
        End Sub
    End Class
End Namespace
