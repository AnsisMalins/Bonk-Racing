Option Explicit On
Option Infer Off
Option Strict On
Imports System.Drawing

Public Class Entity

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap)
		Me.Rectangle = rectangle
		Me.Bitmap = bitmap
		IsLocked = True
		Mass = 1000000
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color)
		Me.Rectangle = rectangle
		Me.Color = color
		IsLocked = True
		Mass = 1000000
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap, ByVal mass As Single, ByVal restitution As Single)
		Me.Rectangle = rectangle
		Me.Bitmap = bitmap
		Me.Mass = mass
		Me.Restitution = restitution
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color, ByVal mass As Single, ByVal restitution As Single)
		Me.Rectangle = rectangle
		Me.Color = color
		Me.Mass = mass
		Me.Restitution = restitution
	End Sub

	Public Property Rectangle() As RectangleF
		Get
			Return New RectangleF(Location - Size / 2, Size)
		End Get
		Set(ByVal value As RectangleF)
			Location = New Vector(value.Location) + New Vector(value.Size) / 2
			Size = value.Size
		End Set
	End Property

	Public Location As Vector

	Public Velocity As Vector

	Public Size As Vector

	Public Mass As Single

	Public IsLocked As Boolean

	Public Color As Color

	Public Restitution As Single

	Public Bitmap As Bitmap
End Class