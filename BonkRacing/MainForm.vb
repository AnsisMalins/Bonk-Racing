Option Explicit On
Option Infer Off
Option Strict On

Public Class MainForm

	Private Sub LoadLevel1()
		world = New World()
		world.Speed = 0.001
		world.Gravity = New Vector(0, 1)
		Dim entity As Entity
		entity = New Entity(New Rectangle(50, 660, 1180, 20), Color.Blue)
		entity.Restitution = 1
		world.Entities.Add(entity)
		entity = New Entity(New Rectangle(200, 200, 40, 80), My.Resources.discord, 1, 0.5)
		world.Entities.Add(entity)
	End Sub

	Private world As World
	Private mouseDown1 As Point
	Private prevSecond As Long
	Private fpsTemp As Integer
	Public FramesPerSecond As Integer
	Private player As Entity

	Public Sub New()
		InitializeComponent()
		ClientSize = New Size(1280, 720)
		LoadLevel1()
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
			If i.Bitmap IsNot Nothing Then
				e.Graphics.DrawImage(i.Bitmap, i.Rectangle)
			Else
				Using brush As New SolidBrush(i.Color)
					e.Graphics.FillRectangle(brush, i.Rectangle)
				End Using
			End If
		Next
		e.Graphics.DrawString("Physics: " & world.FramesPerSecond & " fps" & Environment.NewLine & "Rendering: " & FramesPerSecond & " fps", Font, Brushes.Black, 6, 6)
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
		mouseDown1 = e.Location
	End Sub

	Private Sub MainForm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp
	End Sub

	Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
		UnloadLevel()
	End Sub
End Class