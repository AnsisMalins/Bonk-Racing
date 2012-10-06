Option Explicit On
Option Infer Off
Option Strict On

Public Class Camera

	Private world As World

	Public Sub New(ByVal world As World, ByVal location As Vector)
		Me.world = world
		Me.Location = location
	End Sub

	Public Location As Vector
End Class