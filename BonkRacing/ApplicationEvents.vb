Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
	Partial Friend Class MyApplication

		Protected Overrides Function OnUnhandledException(ByVal e As UnhandledExceptionEventArgs) As Boolean
			e.ExitApplication = False
			MessageBox.Show(e.ToString())
		End Function
	End Class
End Namespace