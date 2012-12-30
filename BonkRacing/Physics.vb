Option Explicit On
Option Infer Off
Option Strict On
Imports System
Imports System.Drawing

Public NotInheritable Class Physics

	Private Sub New()
	End Sub

	''' <summary>http://en.wikipedia.org/wiki/Inelastic_collision#Formula</summary>
	Public Shared Function Collision(ByVal m1 As Single, ByVal v1 As Vector, ByVal m2 As Single, ByVal v2 As Vector, ByVal cr As Single) As Vector
		If m2 <= 0 Then Return -v1 * cr
		Return (cr * m2 * (v2 - v1) + m1 * v1 + m2 * v2) / (m1 + m2)
	End Function

	Public Shared Function CollisionAABB(ByVal rect1 As RectangleF, ByVal rect2 As RectangleF) As Vector
		Dim right As Single = rect2.Left - rect1.Right
		Dim top As Single = rect1.Top - rect2.Bottom
		Dim left As Single = rect1.Left - rect2.Right
		Dim bottom As Single = rect2.Top - rect1.Bottom
		Dim max As Single = Math.Max(Math.Max(Math.Max(right, top), left), bottom)
		If max >= 0 Then Return Vector.Zero
		Select Case max
			Case right
				Return New Vector(-1, 0)
			Case top
				Return New Vector(0, 1)
			Case left
				Return New Vector(1, 0)
			Case bottom
				Return New Vector(0, -1)
			Case Else
				Debug.Fail("Normal undefined.")
				Return Vector.Zero
		End Select
	End Function

	''' <summary>http://en.wikipedia.org/wiki/Specular_reflection#Direction_of_reflection</summary>
	Public Shared Function Reflection(ByVal di As Vector, ByVal dn As Vector) As Vector
		Return 2 * dn * di * dn - di
	End Function
End Class