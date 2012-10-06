Public Class MainForm

	Private world As World
	Private mouseDown As Point
	Private prevSecond As Long
	Private fpsTemp As Integer
	Public FramesPerSecond As Integer

	Public Sub New()
		InitializeComponent()
		world = New World()
		Dim entity As New Entity(New Vector(300, 300), Vector.Zero, New Vector(100, 100), 100000, Color.Red, 1)
		entity.IsLocked = True
		world.Entities.Add(entity)
		entity = New Entity(New Vector(500, 550), Vector.Zero, New Vector(600, 20), 100000, Color.Red, 1)
		entity.IsLocked = True
		world.Entities.Add(entity)
		world.Speed = 0.001
		world.Gravity = New Vector(0, 1)
		world.Start()
	End Sub

	Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		Dim nowTicks As Long = DateTime.Now.Ticks
		If prevSecond = 0 Then prevSecond = nowTicks
		fpsTemp += 1
		Dim entities As Entity()
		SyncLock world.Entities
			entities = world.Entities.ToArray()
		End SyncLock
		For Each i As Entity In entities
			Using brush As New SolidBrush(i.Color)
				e.Graphics.FillRectangle(brush, i.Rectangle)
			End Using
		Next
		e.Graphics.DrawString("Physics: " & world.FramesPerSecond & " fps" & Environment.NewLine & "Rendering: " & FramesPerSecond & " fps", Font, Brushes.Black, 6, 60)
		If nowTicks - prevSecond > 10000000 Then
			FramesPerSecond = fpsTemp
			fpsTemp = 0
			prevSecond += 10000000
		End If
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
		mouseDown = e.Location
	End Sub

	Private Sub MainForm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
		Dim entity As New Entity(mouseDown, Point.Subtract(e.Location, New Size(mouseDown)), New Vector(20, 20), 1, Color.Blue, 0.9)
		SyncLock world.Entities
			world.Entities.Add(entity)
		End SyncLock
	End Sub
End Class