Public Class PropertiesForm

	Private mainForm As MainForm

	Public Sub New(ByVal mainForm As MainForm)
		Me.mainForm = mainForm
		InitializeComponent()
	End Sub

	Public Sub Reload()
		If mainForm.selectTool.selection.Count > 0 Then
			Dim value As Entity = mainForm.selectTool.selection(0)
			x.Text = value.Body.Location.X.ToString()
			y.Text = value.Body.Location.Y.ToString()
			width.Text = value.Body.Size.X.ToString()
			height.Text = value.Body.Size.Y.ToString()
			mass.Text = value.Body.Mass.ToString()
			restitution.Text = value.Body.Restitution.ToString()
			locked.Checked = value.IsLocked
			solid.Checked = value.IsSolid
			zorder.Text = value.ZOrder.ToString()
			tname.Text = value.Name
		Else
			x.Text = ""
			y.Text = ""
			width.Text = ""
			height.Text = ""
			mass.Text = ""
			restitution.Text = ""
			locked.Checked = False
			solid.Checked = False
			zorder.Text = ""
			tname.Text = ""
		End If
		ClearTags()
	End Sub

	Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
		For Each i As Entity In mainForm.selectTool.selection
			If x.Tag IsNot Nothing OrElse y.Tag IsNot Nothing Then i.Body.Location = New Vector(Single.Parse(x.Text), Single.Parse(y.Text))
			If width.Tag IsNot Nothing OrElse height.Tag IsNot Nothing Then i.Body.Size = New Vector(Single.Parse(width.Text), Single.Parse(height.Text))
			If mass.Tag IsNot Nothing Then i.Body.Mass = Single.Parse(mass.Text)
			If restitution.Tag IsNot Nothing Then i.Body.Restitution = Single.Parse(restitution.Text)
			If locked.Tag IsNot Nothing Then i.IsLocked = locked.Checked
			If solid.Tag IsNot Nothing Then i.IsSolid = solid.Checked
			If zorder.Tag IsNot Nothing Then i.ZOrder = Integer.Parse(zorder.Text)
			If tname.Tag IsNot Nothing Then
				If i.Name = "player" AndAlso tname.Text <> "player" Then mainForm.player = Nothing
				i.Name = tname.Text
			End If
			mainForm.AssignActor(i)
		Next
		ClearTags()
	End Sub

	Private Sub PropertiesForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If e.CloseReason = CloseReason.FormOwnerClosing Then Return
		e.Cancel = True
		Hide()
	End Sub

	Private Sub PropertyChanged(ByVal sender As Object, ByVal e As EventArgs) Handles y.TextChanged, x.TextChanged, width.TextChanged, solid.CheckedChanged, restitution.TextChanged, mass.TextChanged, locked.CheckedChanged, height.TextChanged, zorder.TextChanged, tname.TextChanged
		Dim control As Control = DirectCast(sender, Control)
		control.Tag = ""
	End Sub

	Private Sub reloadButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles reloadButton.Click
		Reload()
	End Sub

	Private Sub ClearTags()
		x.Tag = Nothing
		y.Tag = Nothing
		width.Tag = Nothing
		height.Tag = Nothing
		mass.Tag = Nothing
		restitution.Tag = Nothing
		locked.Tag = Nothing
		solid.Tag = Nothing
		zorder.Tag = Nothing
		tname.Tag = Nothing
	End Sub
End Class