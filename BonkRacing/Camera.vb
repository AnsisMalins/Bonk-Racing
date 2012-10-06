Option Explicit On
Option Infer Off
Option Strict On

Public Class Camera

	Private world As World

	Public Sub New(ByVal size As Size, ByVal location As Vector)
		Me.world = world
		Me.Location = location
		Me.Size = size
	End Sub

	Public Function VectorToWorld(ByVal v As Vector) As Vector
		Return v + Location - Size / 2
	End Function

	Public Sub Follow(ByVal target As Entity)
		If Target Is Nothing Then Return
		Location += (Target.Location - Location) * Speed
		X = -Location.X + Size.X / 2
		Y = -Location.Y + Size.Y / 2
	End Sub

	Public X As Single

	Public Y As Single

	Public Location As Vector

	Public Speed As Single

	Public Size As Vector
End Class