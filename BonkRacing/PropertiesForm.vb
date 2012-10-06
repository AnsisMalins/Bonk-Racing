Public Class PropertiesForm

	Private pTarget As Entity
	Public Property Target() As Entity
		Get
			Return pTarget
		End Get
		Set(ByVal value As Entity)
			pTarget = value
			If value IsNot Nothing Then
				x.Text = value.Location.X.ToString()
				y.Text = value.Location.Y.ToString()
				width.Text = value.Size.X.ToString()
				height.Text = value.Size.Y.ToString()
				mass.Text = value.Mass.ToString()
				restitution.Text = value.Restitution.ToString()
				locked.Checked = value.IsLocked
				solid.Checked = value.IsSolid
			End If
		End Set
	End Property

	Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
		If pTarget Is Nothing Then Return
		pTarget.Location = New Vector(Single.Parse(x.Text), Single.Parse(y.Text))
		pTarget.Size = New Vector(Single.Parse(width.Text), Single.Parse(height.Text))
		pTarget.Mass = Single.Parse(mass.Text)
		pTarget.Restitution = Single.Parse(restitution.Text)
		pTarget.IsLocked = locked.Checked
		pTarget.IsSolid = solid.Checked
	End Sub

	Private Sub PropertiesForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If e.CloseReason = CloseReason.FormOwnerClosing Then Return
		e.Cancel = True
		Hide()
	End Sub
End Class