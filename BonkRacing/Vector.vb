Option Explicit On
Option Infer Off
Option Strict On

Public Structure Vector

	Public Shared ReadOnly Zero As New Vector(0, 0)

	Public Sub New(ByVal x As Single, ByVal y As Single)
		pX = x
		pY = y
	End Sub

	Public Sub New(ByVal size As SizeF)
		pX = size.Width
		pY = size.Height
	End Sub

	Public Sub New(ByVal point As PointF)
		pX = point.X
		pY = point.Y
	End Sub

	Private pX As Single
	Public ReadOnly Property X() As Single
		Get
			Return pX
		End Get
	End Property

	Private pY As Single
	Public ReadOnly Property Y() As Single
		Get
			Return pY
		End Get
	End Property

	Public Shared Operator +(ByVal a As Vector, ByVal b As Vector) As Vector
		Return New Vector(a.pX + b.pX, a.pY + b.pY)
	End Operator

	Public Shared Operator -(ByVal a As Vector) As Vector
		Return New Vector(-a.pX, -a.pY)
	End Operator

	Public Shared Operator -(ByVal a As Vector, ByVal b As Vector) As Vector
		Return a + -b
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As PointF
		Return New PointF(a.pX, a.pY)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As SizeF
		Return New SizeF(a.pX, a.pY)
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX * b, a.pY * b)
	End Operator

	Public Shared Operator *(ByVal a As Single, ByVal b As Vector) As Vector
		Return b * a
	End Operator

	Public Shared Operator /(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX / b, a.pY / b)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Point) As Vector
		Return New Vector(a.X, a.Y)
	End Operator

	Public Shared Widening Operator CType(ByVal a As SizeF) As Vector
		Return New Vector(a.Width, a.Height)
	End Operator

	Public Shared Widening Operator CType(ByVal a As PointF) As Vector
		Return New Vector(a.X, a.Y)
	End Operator
End Structure