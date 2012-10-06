Option Explicit On
Option Infer Off
Option Strict On

Public Class MainForm

	Private Sub LoadLevel1()
		world = New World()
		world.Speed = 0.001
		world.Gravity = New Vector(0, 1)
		Dim floor As New Entity(New Rectangle(50, 660, 9001, 20), New TextureBrush(My.Resources.wtffloor))
		floor.Restitution = 0.5
		world.Entities.Add(floor)
		Dim discord As New Entity(New Rectangle(200, 200, 40, 80), My.Resources.discord, 1, 0.5)
		player = discord
		world.Entities.Add(discord)
		camera = New Camera(ClientSize, Vector.Zero)
		camera.Speed = 0.1
	End Sub

	Private resizeTool As New ResizeTool()
	Private moveTool As New MoveTool()
	Private currentTool As BaseTool
	Private createTool As New CreateTool()
	Public selectTool As New SelectTool()
	Private resources As List(Of IDisposable)
	Public camera As Camera
	Public world As World
	Private mouseDown1 As Point
	Private prevSecond As Long
	Private fpsTemp As Integer
	Public FramesPerSecond As Integer
	Private player As Entity
	Private Shared Random As New Random()

	Public Sub New()
		InitializeComponent()
		ClientSize = New Size(1024, 600)
		resources = New List(Of IDisposable)()
		LoadLevel1()
		world.Start()
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim nowTicks As Long = DateTime.Now.Ticks
		If prevSecond = 0 Then prevSecond = nowTicks
		fpsTemp += 1

		Dim mouse As Vector = camera.VectorToWorld(PointToClient(Control.MousePosition))
		MainForm_MouseDown(Nothing, New MouseEventArgs(If(Random.Next(0, 99) >= 50, MouseButtons.Left, MouseButtons.Right), 1, 0, 0, 0))

		camera.Follow(player)
		e.Graphics.TranslateTransform(camera.X, camera.Y)

		Dim entities As Entity()
		SyncLock world.Entities
			entities = world.Entities.ToArray()
		End SyncLock
		Array.Sort(entities, Function(a As Entity, b As Entity) a.ZOrder - b.ZOrder)

		For Each i As Entity In entities
			If i.Bitmap IsNot Nothing Then
				e.Graphics.DrawImage(i.Bitmap, i.Rectangle)
			ElseIf i.Brush IsNot Nothing Then
				e.Graphics.FillRectangle(i.Brush, i.Rectangle)
			Else
				Using brush As New SolidBrush(i.Color)
					e.Graphics.FillRectangle(brush, i.Rectangle)
				End Using
			End If
			i.IsColliding = False
		Next

		If Debugger.IsAttached Then
			For Each i As Entity In entities
				e.Graphics.DrawLine(Pens.Cyan, i.Location, i.Location + i.Velocity * world.Speed * 100)
			Next
			e.Graphics.DrawLine(Pens.Black, camera.Location + New Vector(-20, 0), camera.Location + New Vector(20, 0))
			e.Graphics.DrawLine(Pens.Black, camera.Location + New Vector(0, -20), camera.Location + New Vector(0, 20))
		End If

		If currentTool IsNot Nothing Then
			If currentTool IsNot selectTool Then selectTool.Paint(Me, e)
			currentTool.Paint(Me, e)
		End If

		e.Graphics.TranslateTransform(-camera.X, -camera.Y)

		e.Graphics.DrawString( _
		"Physics: " & If(world.IsRunning, world.FramesPerSecond & " fps", "stopped") & Environment.NewLine _
		& "Rendering: " & FramesPerSecond & " fps" & Environment.NewLine _
		& "Camera: " & camera.Location.ToString() & Environment.NewLine _
		& "Cursor: " & mouse.ToString(), Font, Brushes.Black, 6, 6)

		If nowTicks - prevSecond > 10000000 Then
			FramesPerSecond = fpsTemp
			fpsTemp = 0
			prevSecond += 10000000
		End If
	End Sub

	Private Sub UnloadLevel()
		If world IsNot Nothing Then world.Dispose()
	End Sub

	Private Sub renderTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles renderTimer.Tick
		Invalidate()
	End Sub

	Private Sub startButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles startButton.Click
		world.Start()
	End Sub

	Private Sub stopButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles stopButton.Click
		world.Stop()
	End Sub

	Private Sub clearButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clearButton.Click
		SyncLock world.Entities
			world.Entities.Clear()
		End SyncLock
	End Sub

	Private Sub MainForm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
		Return
		If player.IsColliding Then
			If e.Button = MouseButtons.Left Then
				player.Velocity += New Vector(-200, -200)
			ElseIf e.Button = MouseButtons.Right Then
				player.Velocity += New Vector(200, -200)
			End If
		End If
	End Sub

	Private Sub MainForm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
	End Sub

	Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
		UnloadLevel()
	End Sub

	Private Sub ActivateTool(ByVal tool As BaseTool)
		If currentTool IsNot Nothing Then currentTool.Deactivate()
		currentTool = tool
		If currentTool IsNot Nothing Then currentTool.Activate(Me)
	End Sub

	Private Sub createButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles createButton.Click
		ActivateTool(createTool)
	End Sub

	Private Sub selectButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles selectButton.Click
		ActivateTool(selectTool)
	End Sub

	Private Sub ImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImageToolStripMenuItem.Click
		Dim diag As New OpenFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		Dim texture As New Bitmap(diag.FileName)
		For Each i As Entity In selectTool.selection
			i.Bitmap = texture
		Next
	End Sub

	Private Sub editButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles editButton.Click
		editButton.ContextMenuStrip.Show(editButton, 0, 0)
	End Sub

	Private Sub BrushToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrushToolStripMenuItem.Click
		Dim diag As New OpenFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		Dim texture As New TextureBrush(New Bitmap(diag.FileName))
		For Each i As Entity In selectTool.selection
			i.Brush = texture
		Next
	End Sub

	Private pPropertiesForm As PropertiesForm
	Public ReadOnly Property PropertiesForm() As PropertiesForm
		Get
			If pPropertiesForm Is Nothing Then pPropertiesForm = New PropertiesForm(Me)
			Return pPropertiesForm
		End Get
	End Property

	Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles PropertiesToolStripMenuItem.Click
		If selectTool.selection.Count = 0 Then Return
		If Not PropertiesForm.Visible Then PropertiesForm.Show(Me)
		PropertiesForm.Reload()
	End Sub

	Private Sub moveButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moveButton.Click
		ActivateTool(moveTool)
	End Sub

	Private Sub resizeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles resizeButton.Click
		ActivateTool(resizeTool)
	End Sub

	Private Sub MainForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode <> Keys.Delete Then Return
		SyncLock world.Entities
			For Each i As Entity In selectTool.selection
				world.Entities.Remove(i)
			Next
		End SyncLock
		selectTool.selection.Clear()
	End Sub
End Class