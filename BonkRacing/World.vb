﻿Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.Threading

Public Class World

	Private prevSecond As Long
	Private fpsTemp As Integer
	Private running As EventWaitHandle

	Public Sub New()
		Entities = New List(Of Entity)()
		Speed = 1
		Dim thread As New Thread(AddressOf DoWork)
		thread.IsBackground = True
		thread.Priority = ThreadPriority.Highest
		running = New EventWaitHandle(False, EventResetMode.ManualReset)
		thread.Start()
	End Sub

	Public Entities As List(Of Entity)

	Public Gravity As Vector

	Private pIsRunning As Boolean
	Public Property IsRunning() As Boolean
		Get
			Return pIsRunning
		End Get
		Set(ByVal value As Boolean)
			If value Then Start() Else [Stop]()
		End Set
	End Property

	Public Speed As Single

	Public FramesPerSecond As Integer

	Public Sub Start()
		pIsRunning = True
		prevSecond = DateTime.Now.Ticks
		running.Set()
	End Sub

	Public Sub [Stop]()
		pIsRunning = False
		running.Reset()
	End Sub

	Private Sub DoWork(ByVal state As Object)
		While True
			running.WaitOne()
			Dim nowTicks As Long = DateTime.Now.Ticks
			fpsTemp += 1
			SyncLock Entities
				For Each i As Entity In Entities
					Dim iRect As New RectangleF(i.Location + i.Velocity * Speed - i.Size / 2, i.Size)
					For Each j As Entity In Entities
						If i Is j Then Continue For
						Dim jRect As RectangleF = j.Rectangle
						If RectangleF.Intersect(iRect, jRect) <> RectangleF.Empty Then
							Dim iNewVelocity As Vector = (i.Restitution * j.Restitution * j.Mass * (j.Velocity - i.Velocity) + i.Mass * i.Velocity + j.Mass * j.Velocity) / (i.Mass + j.Mass)
							Dim jNewVelocity As Vector = (j.Restitution * i.Restitution * i.Mass * (i.Velocity - j.Velocity) + j.Mass * j.Velocity + i.Mass * i.Velocity) / (j.Mass + i.Mass)
							If i.IsLocked Then
								j.Velocity = jNewVelocity - iNewVelocity - i.Velocity
							ElseIf j.IsLocked Then
								i.Velocity = iNewVelocity - jNewVelocity - j.Velocity
							Else
								i.Velocity = iNewVelocity
								j.Velocity = jNewVelocity
							End If
							iRect = i.Rectangle	' Hack because we need current instead of predicted location.
							If iRect.Right > jRect.Left AndAlso iRect.Left < jRect.Right _
							OrElse jRect.Right > iRect.Left AndAlso jRect.Left < iRect.Right Then
								i.Velocity = New Vector(-i.Velocity.X, i.Velocity.Y)
								j.Velocity = New Vector(-j.Velocity.X, j.Velocity.Y)
							ElseIf iRect.Bottom > jRect.Top AndAlso iRect.Top < jRect.Bottom _
							OrElse jRect.Bottom > iRect.Top AndAlso jRect.Top < iRect.Bottom Then
								i.Velocity = New Vector(i.Velocity.X, -i.Velocity.Y)
								j.Velocity = New Vector(j.Velocity.X, -j.Velocity.Y)
							End If
						End If
					Next
					i.Location += i.Velocity * Speed
					If Not i.IsLocked Then i.Velocity += Gravity
				Next
			End SyncLock
			If nowTicks - prevSecond > 10000000 Then
				FramesPerSecond = fpsTemp
				fpsTemp = 0
				prevSecond += 10000000
			End If
			Thread.Sleep(1)
		End While
	End Sub
End Class