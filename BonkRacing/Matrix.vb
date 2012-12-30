Option Explicit On
Option Infer Off
Option Strict On
Imports System
Imports GdiMatrix = System.Drawing.Drawing2D.Matrix

Public Class Matrix

	Private m As GdiMatrix
	Public Shared ReadOnly Identity As New Matrix()

	Public Sub New()
		'm = GdiMatrix.
	End Sub

	Public Sub New(ByVal m11 As Single, ByVal m12 As Single, ByVal m21 As Single, ByVal m22 As Single, ByVal dx As Single, ByVal dy As Single)
		m = New GdiMatrix(m11, m12, m21, m22, dx, dy)
	End Sub

End Class