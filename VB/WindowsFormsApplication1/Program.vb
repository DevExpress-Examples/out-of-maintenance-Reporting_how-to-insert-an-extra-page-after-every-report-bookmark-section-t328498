Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Threading
Imports System.IO
Imports System.Reflection

Namespace WindowsFormsApplication1
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("pt-PT")
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Form1())
            'string s = DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active.GetLocalizedString(DevExpress.XtraPrinting.Localization.PreviewStringId.PageInfo_PageNumberOfTotal);
        End Sub


        Private Shared Function CurrentDomain_AssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
            Return Nothing
        End Function
    End Class
End Namespace
