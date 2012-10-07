Option Explicit On
Option Infer Off
Option Strict On

Public Class Camera

	Public Sub New(ByVal size As Size, ByVal location As Vector)
		Me.Size = size
		Me.Location = location
		Speed = 0.1
	End Sub

	Public Function VectorToWorld(ByVal v As Vector) As Vector
		Return v + Location - Size / 2
	End Function

	Public Sub Follow(ByVal target As Entity)
		If Target Is Nothing Then Return
		Location += (Target.Location - Location) * Speed
	End Sub

	Public X As Single

	Public Y As Single

	Private pLocation As Vector
	Public Property Location() As Vector
		Get
			Return pLocation
		End Get
		Set(ByVal value As Vector)
			pLocation = value
			X = -Location.X + Size.X / 2
			Y = -Location.Y + Size.Y / 2
		End Set
	End Property

	Public Speed As Single

	Public Size As Vector
End Class