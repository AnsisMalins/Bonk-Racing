Option Explicit On
Option Infer Off
Option Strict On
Imports System
Imports System.Diagnostics
Imports System.Drawing

<DebuggerDisplay("{pX}, {pY}")> _
Public Structure Vector

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Public Shared ReadOnly Zero As New Vector(0, 0)

	Public Sub New(ByVal x As Single, ByVal y As Single)
		pX = x
		pY = y
	End Sub

	Public Sub New(ByVal x As Double, ByVal y As Double)
		pX = CSng(x)
		pY = CSng(y)
	End Sub

	Public Sub New(ByVal point As PointF)
		pX = point.X
		pY = point.Y
	End Sub

	Public Sub New(ByVal size As SizeF)
		pX = size.Width
		pY = size.Height
	End Sub

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pX As Single
	Public ReadOnly Property X() As Single
		Get
			Return pX
		End Get
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
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

	Public Shared Operator *(ByVal a As Single, ByVal b As Vector) As Vector
		Return New Vector(a * b.pX, a * b.pY)
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX * b, a.pY * b)
	End Operator

	Public Shared Operator *(ByVal a As PointF, ByVal b As Vector) As Single
		Return a.X * b.pX + a.Y * b.pY
	End Operator

	Public Shared Operator *(ByVal a As SizeF, ByVal b As Vector) As Single
		Return a.Width * b.pX + a.Height * b.pY
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As PointF) As Single
		Return a.pX * b.X + a.pX + b.Y
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As SizeF) As Single
		Return a.pX * b.Width + a.pY * b.Height
	End Operator

	Public Shared Operator *(ByVal a As Vector, ByVal b As Vector) As Single
		Return a.pX * b.pX + a.pY * b.pY
	End Operator

	Public Shared Operator /(ByVal a As Vector, ByVal b As Single) As Vector
		Return New Vector(a.pX / b, a.pY / b)
	End Operator

	Public Shared Operator +(ByVal a As SizeF, ByVal b As Vector) As Vector
		Return New Vector(a.Width + b.pX, a.Height + b.pY)
	End Operator

	Public Shared Operator +(ByVal a As PointF, ByVal b As Vector) As Vector
		Return New Vector(a.X + b.pX, a.Y + b.pY)
	End Operator

	Public Shared Operator +(ByVal a As Vector, ByVal b As SizeF) As Vector
		Return New Vector(a.pX + b.Width, a.pY + b.Height)
	End Operator

	Public Shared Operator +(ByVal a As Vector, ByVal b As PointF) As Vector
		Return New Vector(a.pX + b.X, a.pY + b.Y)
	End Operator

	Public Shared Operator +(ByVal a As Vector, ByVal b As Vector) As Vector
		Return New Vector(a.pX + b.pX, a.pY + b.pY)
	End Operator

	Public Shared Operator -(ByVal a As SizeF, ByVal b As Vector) As Vector
		Return New Vector(a.Width - b.pX, a.Height - b.pY)
	End Operator

	Public Shared Operator -(ByVal a As PointF, ByVal b As Vector) As Vector
		Return New Vector(a.X - b.pX, a.Y - b.pY)
	End Operator

	Public Shared Operator -(ByVal a As Vector, ByVal b As SizeF) As Vector
		Return New Vector(a.pX - b.Width, a.pY - b.Height)
	End Operator

	Public Shared Operator -(ByVal a As Vector, ByVal b As PointF) As Vector
		Return New Vector(a.pX - b.X, a.pY - b.Y)
	End Operator

	Public Shared Operator -(ByVal a As Vector, ByVal b As Vector) As Vector
		Return New Vector(a.pX - b.pX, a.pY - b.pY)
	End Operator

	Public Shared Operator =(ByVal a As SizeF, ByVal b As Vector) As Boolean
		Return a.Width = b.pX AndAlso a.Height = b.pY
	End Operator

	Public Shared Operator =(ByVal a As PointF, ByVal b As Vector) As Boolean
		Return a.X = b.pX AndAlso a.Y = b.pY
	End Operator

	Public Shared Operator =(ByVal a As Vector, ByVal b As SizeF) As Boolean
		Return a.pX = b.Width AndAlso a.pY = b.Height
	End Operator

	Public Shared Operator =(ByVal a As Vector, ByVal b As PointF) As Boolean
		Return a.pX = b.X AndAlso a.pY = b.Y
	End Operator

	Public Shared Operator =(ByVal a As Vector, ByVal b As Vector) As Boolean
		Return a.pX = b.pX AndAlso a.pY = b.pY
	End Operator

	Public Shared Operator <>(ByVal a As SizeF, ByVal b As Vector) As Boolean
		Return a.Width <> b.pX OrElse a.Height <> b.pY
	End Operator

	Public Shared Operator <>(ByVal a As PointF, ByVal b As Vector) As Boolean
		Return a.X <> b.pX OrElse a.Y <> b.pY
	End Operator

	Public Shared Operator <>(ByVal a As Vector, ByVal b As SizeF) As Boolean
		Return a.pX <> b.Width OrElse a.pY <> b.Height
	End Operator

	Public Shared Operator <>(ByVal a As Vector, ByVal b As PointF) As Boolean
		Return a.pX <> b.X OrElse a.pY <> b.Y
	End Operator

	Public Shared Operator <>(ByVal a As Vector, ByVal b As Vector) As Boolean
		Return a.pX <> b.pX OrElse a.pY <> b.pY
	End Operator

	Public Shared Widening Operator CType(ByVal a As Size) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As SizeF) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Point) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As PointF) As Vector
		Return New Vector(a)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As Size
		Return New Size(CInt(a.pX), CInt(a.pY))
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As SizeF
		Return New SizeF(a.pX, a.pY)
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As Point
		Return New Point(CInt(a.pX), CInt(a.pY))
	End Operator

	Public Shared Widening Operator CType(ByVal a As Vector) As PointF
		Return New PointF(a.pX, a.pY)
	End Operator
End Structure