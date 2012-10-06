Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
	Partial Friend Class MyApplication

		Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs) Handles MyClass.UnhandledException
			e.ExitApplication = False
			MessageBox.Show(If(Debugger.IsAttached, e.ToString(), e.Exception.Message))
		End Sub
	End Class
End Namespace