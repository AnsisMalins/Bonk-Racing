Option Explicit On
Option Infer Off
Option Strict On

Public Structure Vector

	Public Shared ReadOnly Zero As New Vector(0, 0)

	Public Sub New(ByVal x As Single, ByVal y As Single)
		pX = x
		pY = y
	End Sub

	Public Sub New(ByVal x As Double, ByVal y As Double)
		pX = CSng(x)
		pY = CSng(y)
	End Sub

	Public Sub New(ByVal point As Point)
		pX = point.X
		pY = point.Y
	End Sub

	Public Sub New(ByVal point As PointF)
		pX = point.X
		pY = point.Y
	End Sub

	Public Sub New(ByVal size As Size)
		pX = size.Width
		pY = size.Height
	End Sub

	Public Sub New(ByVal size As SizeF)
		pX = size.Width
		pY = size.Height
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

	Public Function GetLength() As Single
		Return CSng(Math.Sqrt(Me * Me))
	End Function

	Public Function GetNormal() As Vector
		Return Me / GetLength()
	End Function

	Public Overrides Function ToString() As String
		Return pX.ToString() & ", " & pY.ToString()
	End Function

	Public Shared Operator +(ByVal a As Vector) As Vector
		Return a
	End Operator

	Public Shared Operator -(ByVal a As Vector) As Vector
		Return New Vector(-a.pX, -a.pY)
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX * b, a.pY * b)
	End Operator

	Public Shared Operator *(ByVal a As Single, ByVal b As Vector) As Vector
		Return New Vector(a * b.pX, a * b.pY)
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As Vector) As Single
		Return a.pX * b.pX + a.pY * b.pY
	End Operator

	Public Shared Operator /(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX / b, a.pY / b)
	End Operator

	Public Shared Operator +(ByVal a As Vector, ByVal b As Vector) As Vector
		Return New Vector(a.pX + b.pX, a.pY + b.pY)
	End Operator

	Public Shared Operator -(ByVal a As Vector, ByVal b As Vector) As Vector
		Return New Vector(a.pX - b.pX, a.pY - b.pY)
	End Operator

	Public Shared Operator =(ByVal a As Vector, ByVal b As Vector) As Boolean
		Return a.pX = b.pX AndAlso a.pY = b.pY
	End Operator

	Public Shared Operator <>(ByVal a As Vector, ByVal b As Vector) As Boolean
		Return a.pX <> b.pX OrElse a.pY <> b.pY
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As Point
		Return New Point(CInt(a.pX), CInt(a.pY))
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As PointF
		Return New PointF(a.pX, a.pY)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As Size
		Return New Size(CInt(a.pX), CInt(a.pY))
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As SizeF
		Return New SizeF(a.pX, a.pY)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Point) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As PointF) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Size) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As SizeF) As Vector
		Return New Vector(a)
	End Operator
End Structure