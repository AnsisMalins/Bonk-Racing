Option Explicit On
Option Infer Off
Option Strict On
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic

Public Class MainForm

	Public Shared ReadOnly jx As Single = 0.1
	Public Shared ReadOnly jy As Single = 0.3
	Private gameLost As Boolean
	Private gameWon As Boolean

	Public Sub GameLose()
		gameLost = True
	End Sub

	Public Sub GameWin()
		gameWon = True
	End Sub

	Private direction As Integer = 1
	Private Sub PlayerControl(ByVal keyData As Keys)
		If player Is Nothing Then Return
		Select Case keyData
			Case Keys.Left
				If direction <> -1 Then player.Bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
				direction = -1
			Case Keys.Right
				If direction <> 1 Then player.Bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
				direction = 1
			Case Keys.Up
				If player.IsColliding Then
					player.Velocity += New Vector(1 * direction * jx, -1.6 * jy)
				End If
			Case Keys.Down
				If player.IsColliding Then
					player.Velocity += New Vector(2 * direction * jx, -0.5 * jy)
				End If
			Case Keys.Escape
				world.Stop()
				mainMenu.Visible = True
		End Select
	End Sub

	Public Sub AssignActor(ByVal entity As Entity)
		Select Case entity.Name
			Case "player"
				player = entity
			Case "pinkie"
				entity.Actor = New PinkiePie()
			Case Else
				entity.Actor = Nothing
		End Select
	End Sub

	Private Sub LoadLevel(ByVal fileName As String)
		Dim xml As New XmlDocument()
		xml.Load(fileName)
		world = New World()
		Dim worldNode As XmlNode = xml.SelectSingleNode("//world")
		If worldNode IsNot Nothing Then
			Dim attr1 As XmlAttribute, attr2 As XmlAttribute
			attr1 = worldNode.Attributes("gx")
			attr2 = worldNode.Attributes("gy")
			If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then world.Gravity = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
			attr1 = worldNode.Attributes("speed")
			If attr1 IsNot Nothing Then world.Speed = Single.Parse(attr1.Value)
		End If
		SyncLock world.Entities
			For Each i As XmlNode In xml.SelectNodes("//entity")
				Dim entity As New Entity(i)
				world.Entities.Add(entity)
				AssignActor(entity)
			Next
		End SyncLock
		Dim cameraNode As XmlNode = xml.SelectSingleNode("//camera")
		If cameraNode IsNot Nothing Then
			Dim attr1 As XmlAttribute, attr2 As XmlAttribute
			attr1 = cameraNode.Attributes("w")
			attr2 = cameraNode.Attributes("h")
			If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then camera = New Camera(New Size(Integer.Parse(attr1.Value), Integer.Parse(attr2.Value)), Vector.Zero) Else Return
			attr1 = cameraNode.Attributes("x")
			attr2 = cameraNode.Attributes("y")
			If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then camera.Location = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
			attr1 = cameraNode.Attributes("speed")
			camera.Speed = If(attr1 IsNot Nothing, Single.Parse(attr1.Value), 1)
		End If
	End Sub

	Private Sub SaveLevel(ByVal fileName As String)
		Using writer As New StreamWriter(fileName)
			writer.WriteLine("<world" _
			& " gx=""" & world.Gravity.X & """" _
			& " gy=""" & world.Gravity.Y & """" _
			& " speed=""" & world.Speed & """>")
			writer.WriteLine("<camera" _
			& " w=""" & camera.Size.X & """" _
			& " h=""" & camera.Size.Y & """" _
			& " x=""" & camera.Location.X & """" _
			& " y=""" & camera.Location.Y & """" _
			& " speed=""" & camera.Speed & """/>")
			SyncLock world.Entities
				For Each i As Entity In world.Entities
					writer.WriteLine(i.ToXml())
				Next
			End SyncLock
			writer.Write("</world>")
		End Using
	End Sub

	Private textureTool As New TextureTool()
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
	Public player As Entity
	Private Shared Random As New Random()

	Public Sub New()
		InitializeComponent()
		Dim resolution As New Size(1024, 600)
		If Screen.PrimaryScreen.Bounds.Size = resolution _
		Then FormBorderStyle = Windows.Forms.FormBorderStyle.None _
		Else FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
		ClientSize = resolution
		camera = New Camera(resolution, Vector.Zero)
		world = New World()
		If Not Debugger.IsAttached Then
			mainMenu.Dock = DockStyle.Fill
			mainMenu.Visible = True
		End If
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim nowTicks As Long = DateTime.Now.Ticks
		If prevSecond = 0 Then prevSecond = nowTicks
		fpsTemp += 1

		Dim mouse As Vector = camera.VectorToWorld(PointToClient(Control.MousePosition))

		If world.IsRunning Then camera.Follow(player)
		e.Graphics.TranslateTransform(camera.X, camera.Y)

		Dim entities As Entity()
		SyncLock world.Entities
			entities = world.Entities.ToArray()
		End SyncLock
		Array.Sort(entities, Function(a As Entity, b As Entity) a.ZOrder - b.ZOrder)

		For Each i As Entity In entities
			i.RenderCallback(Me)
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
			If i Is player AndAlso i.Location.Y > 500 Then
				gameWon = True
			End If
		Next

		If Debugger.IsAttached Then
			For Each i As Entity In entities
				e.Graphics.DrawLine(Pens.Cyan, i.Location, i.Location + i.Velocity * world.Speed * 100)
			Next
			e.Graphics.DrawLine(Pens.Black, camera.Location + New Vector(-20, 0), camera.Location + New Vector(20, 0))
			e.Graphics.DrawLine(Pens.Black, camera.Location + New Vector(0, -20), camera.Location + New Vector(0, 20))
			e.Graphics.DrawString(camera.Location.ToString(), Font, Brushes.Black, camera.Location)
		End If

		If currentTool IsNot Nothing Then
			If currentTool IsNot selectTool Then selectTool.Paint(Me, e)
			currentTool.Paint(Me, e)
		End If

		e.Graphics.TranslateTransform(-camera.X, -camera.Y)

		If Debugger.IsAttached Then e.Graphics.DrawString( _
		"Physics: " & If(world.IsRunning, world.FramesPerSecond & " fps", "stopped") & Environment.NewLine _
		& "Rendering: " & FramesPerSecond & " fps" & Environment.NewLine _
		& "Camera: " & camera.Location.ToString() & Environment.NewLine _
		& "Cursor: " & mouse.ToString(), Font, Brushes.Black, 6, 6)

		If gameWon Then
			Using f As New StringFormat()
				f.Alignment = StringAlignment.Center
				f.LineAlignment = StringAlignment.Center
				e.Graphics.DrawString("WIN" & Environment.NewLine & "press Esc to play again", mainMenu.Font, Brushes.Green, ClientRectangle, f)
			End Using
		ElseIf gameLost Then
			Using f As New StringFormat()
				f.Alignment = StringAlignment.Center
				f.LineAlignment = StringAlignment.Center
				e.Graphics.DrawString("FAIL" & Environment.NewLine & "press Esc to retry", mainMenu.Font, Brushes.Red, ClientRectangle, f)
			End Using
		End If

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
		If world.IsRunning Then
			world.Stop()
			startButton.Text = "Start"
		Else
			world.Start()
			startButton.Text = "Stop"
		End If
	End Sub

	Private Sub clearButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ClearToolStripMenuItem.Click
		SyncLock world.Entities
			world.Entities.Clear()
		End SyncLock
		camera.Location = Vector.Zero
	End Sub

	Private Sub MainForm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
		If e.Button = Windows.Forms.MouseButtons.Middle Then mouseDrag1 = e.Location
		If e.Button = Windows.Forms.MouseButtons.Right Then
			ActivateTool(selectTool)
		End If
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

	Private Sub createButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CreateToolStripMenuItem.Click
		ActivateTool(createTool)
	End Sub

	Private Sub selectButton_Click(ByVal sender As Object, ByVal e As EventArgs)
		ActivateTool(selectTool)
	End Sub

	Private Sub ImageToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ImageToolStripMenuItem.Click
		Dim diag As New OpenFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		diag.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*"
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		Dim fileName As String = Path.GetFileName(diag.FileName)
		If Path.GetDirectoryName(diag.FileName) <> My.Application.Info.DirectoryPath Then File.Copy(diag.FileName, Path.Combine(My.Application.Info.DirectoryPath, fileName))
		Dim texture As New Bitmap(fileName)
		For Each i As Entity In selectTool.selection
			i.Bitmap = texture
			i.ImageFile = fileName
		Next
	End Sub

	Private Sub editButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles editButton.Click
		editButton.ContextMenuStrip.Show(editButton, 0, 0)
	End Sub

	Private Sub BrushToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrushToolStripMenuItem.Click
		Dim diag As New OpenFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		diag.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*"
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		Dim fileName As String = Path.GetFileName(diag.FileName)
		If Path.GetDirectoryName(diag.FileName) <> My.Application.Info.DirectoryPath Then File.Copy(diag.FileName, Path.Combine(My.Application.Info.DirectoryPath, fileName))
		Dim texture As New TextureBrush(New Bitmap(fileName))
		For Each i As Entity In selectTool.selection
			i.Brush = texture
			i.ImageFile = fileName
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
		PropertiesForm.Focus()
	End Sub

	Private Sub moveButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MoveToolStripMenuItem.Click
		ActivateTool(moveTool)
	End Sub

	Private Sub resizeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResizeToolStripMenuItem.Click
		ActivateTool(resizeTool)
	End Sub

	Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
		If world.IsRunning Then
			If keyData = Keys.F5 Then startButton_Click(Nothing, Nothing)
			PlayerControl(keyData)
		Else
			Select Case keyData
				Case Keys.Delete
					SyncLock world.Entities
						For Each i As Entity In selectTool.selection
							world.Entities.Remove(i)
						Next
					End SyncLock
					selectTool.selection.Clear()
				Case Keys.M
					moveButton_Click(Nothing, Nothing)
				Case Keys.C
					createButton_Click(Nothing, Nothing)
				Case Keys.R
					resizeButton_Click(Nothing, Nothing)
				Case Keys.F5
					startButton_Click(Nothing, Nothing)
				Case Keys.P
					PropertiesToolStripMenuItem_Click(Nothing, Nothing)
				Case Keys.T
					ToolStripMenuItem3_Click(Nothing, Nothing)
			End Select
		End If
		Return MyBase.ProcessCmdKey(msg, keyData)
	End Function

	Private Sub loadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
		Dim diag As New OpenFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		diag.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		Dim fileName As String = Path.GetFileName(diag.FileName)
		If Path.GetDirectoryName(diag.FileName) <> My.Application.Info.DirectoryPath Then File.Copy(diag.FileName, Path.Combine(My.Application.Info.DirectoryPath, fileName))
		LoadLevel(fileName)
	End Sub

	Private Sub saveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
		Dim diag As New SaveFileDialog()
		diag.InitialDirectory = My.Application.Info.DirectoryPath
		diag.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
		If diag.ShowDialog() <> Windows.Forms.DialogResult.OK Then Return
		SaveLevel(diag.FileName)
	End Sub

	Dim mouseDrag1 As Vector
	Private Sub MainForm_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
		If e.Button <> Windows.Forms.MouseButtons.Middle Then Return
		Dim mouseDrag2 As Vector = e.Location
		camera.Location -= mouseDrag2 - mouseDrag1
		mouseDrag1 = mouseDrag2
	End Sub

	Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
		ActivateTool(textureTool)
	End Sub

	Private Sub gameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gameButton.Click
		startButton.Visible = False
		editButton.Visible = False
		gameWon = False
		gameLost = False
		mainMenu.Visible = False
		LoadLevel("level.xml")
		world.Start()
	End Sub
End Class