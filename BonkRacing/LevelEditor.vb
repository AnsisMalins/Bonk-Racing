
Public Class BaseTool

	Public Sub Activate(ByVal form As MainForm)
		If pForm IsNot Nothing Then Deactivate()
		pForm = form
		AddHandler pForm.MouseDown, AddressOf MouseDown
		AddHandler pForm.MouseMove, AddressOf MouseMove
		AddHandler pForm.MouseUp, AddressOf MouseUp
	End Sub

	Public Sub Deactivate()
		RemoveHandler pForm.MouseDown, AddressOf MouseDown
		RemoveHandler pForm.MouseMove, AddressOf MouseMove
		RemoveHandler pForm.MouseUp, AddressOf MouseUp
		pForm = Nothing
	End Sub

	Public Overridable Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
	End Sub

	Public Overridable Sub MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
	End Sub

	Public Overridable Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
	End Sub

	Public Overridable Sub Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
	End Sub

	Private pForm As MainForm
	Protected ReadOnly Property Form() As MainForm
		Get
			Return pForm
		End Get
	End Property
End Class

Public Class CreateTool
	Inherits BaseTool

	Private mouse1 As Vector
	Private mdown As Boolean

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Control.MouseButtons <> MouseButtons.Left Then Return
		mouse1 = Form.camera.VectorToWorld(e.Location)
		mdown = True
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		Dim entity As New Entity(New Rectangle(topLeft, downRight - topLeft), Color.Blue)
		SyncLock Form.world.Entities
			Form.world.Entities.Add(entity)
		End SyncLock
		mdown = False
	End Sub

	Public Overrides Sub Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
		If Not mdown Then Return
		Dim mouse2 As Vector = Form.camera.VectorToWorld(Form.PointToClient(Control.MousePosition))
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		e.Graphics.DrawRectangle(Pens.Green, New Rectangle(topLeft, downRight - topLeft))
	End Sub
End Class

Public Class SelectTool
	Inherits BaseTool

	Private mouse1 As Vector
	Private mdown As Boolean
	Public selection As New List(Of Entity)

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Control.MouseButtons <> MouseButtons.Left Then Return
		mouse1 = Form.camera.VectorToWorld(e.Location)
		mdown = True
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Not My.Computer.Keyboard.ShiftKeyDown Then selection.Clear()
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		SyncLock Form.world.Entities
			For Each i As Entity In Form.world.Entities
				If RectangleF.Intersect(New RectangleF(topLeft, downRight - topLeft), i.Rectangle) <> RectangleF.Empty Then
					selection.Add(i)
				End If
			Next
		End SyncLock
		mdown = False
		Form.PropertiesForm.Reload()
	End Sub

	Public Overrides Sub Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
		Using pen As New Pen(Color.LightPink, 2)
			For Each i As Entity In selection
				e.Graphics.DrawRectangle(pen, Rectangle.Round(i.Rectangle))
			Next
		End Using
		If Not mdown Then Return
		Dim mouse2 As Vector = Form.camera.VectorToWorld(Form.PointToClient(Control.MousePosition))
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		If (downRight - topLeft).GetLength() < 3 Then Return
		e.Graphics.DrawRectangle(Pens.Green, New Rectangle(topLeft, downRight - topLeft))
	End Sub
End Class