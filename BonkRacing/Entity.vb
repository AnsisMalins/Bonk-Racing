Option Explicit On
Option Infer Off
Option Strict On
Imports System.Drawing

Public Class Entity

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap)
		Initialize(rectangle)
		Me.Bitmap = bitmap
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal brush As Brush)
		Initialize(rectangle)
		Me.Brush = brush
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color)
		Initialize(rectangle)
		Me.Color = color
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap, ByVal mass As Single, ByVal restitution As Single)
		Initialize(rectangle, mass, restitution)
		Me.Bitmap = bitmap
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color, ByVal mass As Single, ByVal restitution As Single)
		Initialize(rectangle, mass, restitution)
		Me.Color = color
	End Sub

	Private Sub Initialize(ByVal rectangle As RectangleF)
		Me.Rectangle = rectangle
		IsLocked = True
		IsSolid = True
		Mass = 1000000
	End Sub

	Private Sub Initialize(ByVal rectangle As RectangleF, ByVal mass As Single, ByVal restitution As Single)
		Me.Rectangle = rectangle
		Me.Mass = mass
		IsSolid = True
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

	Public Bitmap As Bitmap
	Public Brush As Brush
	Public Color As Color

	Public Location As Vector

	Public Velocity As Vector

	Public Size As Vector

	Public Mass As Single

	Public IsLocked As Boolean

	Public IsSolid As Boolean

	Public Restitution As Single

	Public IsColliding As Boolean
End Class