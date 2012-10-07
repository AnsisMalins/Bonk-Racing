Option Explicit On
Option Infer Off
Option Strict On

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
	Private rnd As New Random()

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Control.MouseButtons <> MouseButtons.Left Then Return
		mouse1 = Form.camera.VectorToWorld(e.Location)
		mdown = True
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		Dim entity As New Entity(New Rectangle(topLeft, downRight - topLeft), Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)))
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
		Dim rect As New Rectangle(topLeft, downRight - topLeft)
		e.Graphics.DrawRectangle(Pens.Green, rect)
		e.Graphics.DrawString(New Vector(rect.Size).ToString(), Form.Font, Brushes.Black, mouse2 + New Vector(16, 16))
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
		If Not My.Computer.Keyboard.ShiftKeyDown AndAlso Not My.Computer.Keyboard.CtrlKeyDown Then selection.Clear()
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		Dim topLeft As New Vector(Math.Min(mouse1.X, mouse2.X), Math.Min(mouse1.Y, mouse2.Y))
		Dim downRight As New Vector(Math.Max(mouse1.X, mouse2.X), Math.Max(mouse1.Y, mouse2.Y))
		SyncLock Form.world.Entities
			For Each i As Entity In Form.world.Entities
				Dim selectRect As New RectangleF(topLeft, downRight - topLeft)
				If RectangleF.Intersect(selectRect, i.Rectangle) <> RectangleF.Empty Then
					If My.Computer.Keyboard.CtrlKeyDown Then selection.Remove(i) Else selection.Add(i)
					If selectRect.Size = Size.Empty Then Exit For
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

Public Class MoveTool
	Inherits BaseTool

	Private mdown As Boolean
	Private mouse1 As Vector

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If e.Button <> MouseButtons.Left Then Return
		mdown = True
		mouse1 = Form.camera.VectorToWorld(e.Location)
	End Sub

	Public Overrides Sub MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Not mdown Then Return
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		For Each i As Entity In Form.selectTool.selection
			i.Location += mouse2 - mouse1
			i.Velocity = Vector.Zero
		Next
		mouse1 = mouse2
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		mdown = False
	End Sub

	Public Overrides Sub Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
		If Not mdown OrElse Form.selectTool.selection.Count = 0 Then Return
		Dim entity As Entity = Form.selectTool.selection(0)
		e.Graphics.DrawString(entity.Location.ToString(), Form.Font, Brushes.Black, mouse1 + New Vector(16, 16))
	End Sub
End Class

Public Class ResizeTool
	Inherits BaseTool

	Private mdown As Boolean
	Private mouse1 As Vector

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		If e.Button <> MouseButtons.Left Then Return
		mdown = True
		mouse1 = Form.camera.VectorToWorld(e.Location)
	End Sub

	Public Overrides Sub MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		If Not mdown Then Return
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		If Form.selectTool.selection.Count = 0 Then Return
		Dim entity As Entity = Form.selectTool.selection(0)
		Dim side As Integer = 0
		If mouse2.X > entity.Rectangle.Left AndAlso mouse2.X < entity.Rectangle.Right Then
			If mouse2.Y < entity.Location.Y Then
				side = 1
			ElseIf mouse2.Y > entity.Location.Y Then
				side = 2
			End If
		ElseIf mouse2.Y > entity.Rectangle.Top AndAlso mouse2.Y < entity.Rectangle.Bottom Then
			If mouse2.X < entity.Location.X Then
				side = 3
			ElseIf mouse2.X > entity.Location.X Then
				side = 4
			End If
		End If
		Dim dmouse As Vector = mouse2 - mouse1
		For Each i As Entity In Form.selectTool.selection
			Dim rect As RectangleF = i.Rectangle
			Select Case side
				Case 1
					rect.Inflate(0, -dmouse.Y / 2)
					rect.Offset(0, dmouse.Y / 2)
				Case 2
					rect.Inflate(0, dmouse.Y / 2)
					rect.Offset(0, dmouse.Y / 2)
				Case 3
					rect.Inflate(-dmouse.X / 2, 0)
					rect.Offset(dmouse.X / 2, 0)
				Case 4
					rect.Inflate(dmouse.X / 2, 0)
					rect.Offset(dmouse.X / 2, 0)
			End Select
			i.Rectangle = rect
		Next
		mouse1 = mouse2
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		mdown = False
	End Sub

	Public Overrides Sub Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
		If Form.selectTool.selection.Count = 0 Then Return
		Dim entity As Entity = Form.selectTool.selection(0)
		e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(entity.Rectangle))
		If Not mdown Then Return
		e.Graphics.DrawString(entity.Size.ToString(), Form.Font, Brushes.Black, mouse1 + New Vector(16, 16))
	End Sub
End Class

Public Class TextureTool
	Inherits BaseTool

	Private mdown As Boolean
	Private mouse1 As Vector

	Public Overrides Sub MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
		If e.Button <> MouseButtons.Left Then Return
		mdown = True
		mouse1 = Form.camera.VectorToWorld(e.Location)
	End Sub

	Public Overrides Sub MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
		If Not mdown Then Return
		Dim mouse2 As Vector = Form.camera.VectorToWorld(e.Location)
		For Each i As Entity In Form.selectTool.selection
			If i.Brush IsNot Nothing Then
				i.Brush.TranslateTransform(-i.TextureOffset.X, -i.TextureOffset.Y)
				i.TextureOffset += mouse2 - mouse1
				i.Brush.TranslateTransform(i.TextureOffset.X, i.TextureOffset.Y)
			End If
		Next
		mouse1 = mouse2
	End Sub

	Public Overrides Sub MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		mdown = False
	End Sub

	Public Overrides Sub Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
		If Not mdown OrElse Form.selectTool.selection.Count = 0 Then Return
		Dim entity As Entity = Form.selectTool.selection(0)
		e.Graphics.DrawString(entity.Location.ToString(), Form.Font, Brushes.Black, mouse1 + New Vector(16, 16))
	End Sub
End Class