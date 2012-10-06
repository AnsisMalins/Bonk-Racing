Option Explicit On
Option Infer Off
Option Strict On
Imports System.Drawing

Public Class Entity

	Public Sub New(ByVal location As Vector, ByVal velocity As Vector, ByVal size As Vector, ByVal mass As Single, ByVal color As Color, ByVal restitution As Single)
		Me.Location = location
		Me.Velocity = velocity
		Me.Size = size
		Me.Mass = If(mass > 0, mass, Single.MaxValue)
		Me.Color = color
		Me.Restitution = restitution
	End Sub

	Public Property Rectangle() As RectangleF
		Get
			Return New RectangleF(Location - Size / 2, Size)
		End Get
		Set(ByVal value As RectangleF)
			Location = New Vector(Rectangle.Location) + New Vector(Rectangle.Size) / 2
			Size = Rectangle.Size
		End Set
	End Property

	Public Location As Vector

	Public Velocity As Vector

	Public Size As Vector

	Public Mass As Single

	Public IsLocked As Boolean

	Public Color As Color

	Public Restitution As Single
End Class