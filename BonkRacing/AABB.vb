Option Explicit On
Option Infer Off
Option Strict On
Imports System
Imports System.Drawing

Public Structure AABB

	Public Sub New(ByVal x As Single, ByVal y As Single, ByVal w As Single, ByVal h As Single)
		pL = New Vector(x, y)
		pS = New Vector(w, h)
	End Sub

	Public Sub New(ByVal l As Vector, ByVal s As Vector)
		pL = l
		pS = s
	End Sub

	Public Sub New(ByVal r As RectangleF)
		pS = r.Size
		pL = r.Location + pS / 2
	End Sub

	Private pL As Vector
	Public ReadOnly Property L() As Vector
		Get
			Return pL
		End Get
	End Property

	Private pS As Vector
	Public ReadOnly Property S() As Vector
		Get
			Return pS
		End Get
	End Property

	Public Function GetArea() As Single
		Return pS.X * pS.Y
	End Function

	Public Function Offset(ByVal v As Vector) As AABB
		Return New AABB(pL + v, S)
	End Function

	Public Function Resize(ByVal v As Vector) As AABB
		Return New AABB(pL, pS + v)
	End Function
End Structure