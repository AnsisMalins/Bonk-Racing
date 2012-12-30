Option Explicit On
Option Infer Off
Option Strict On
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Threading

Public Class World
	Implements IDisposable

	Private prevSecond As Long
	Private fpsTemp As Integer
	Private running As EventWaitHandle

	Public Sub New()
		pBodies = New List(Of Body)()
		pGravity = New Vector(0, 0.001)
		pMaxAdjustmentPasses = 4
		pRate = 1
		Dim thread As New Thread(AddressOf DoWork)
		thread.IsBackground = True
		thread.Priority = ThreadPriority.Highest
		running = New EventWaitHandle(False, EventResetMode.ManualReset)
		thread.Start()
	End Sub

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pBodies As List(Of Body)
	Public ReadOnly Property Bodies() As List(Of Body)
		Get
			Return pBodies
		End Get
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pFramesPerSecond As Integer
	Public ReadOnly Property FramesPerSecond() As Integer
		Get
			Return pFramesPerSecond
		End Get
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pGravity As Vector
	Public Property Gravity() As Vector
		Get
			Return pGravity
		End Get
		Set(ByVal value As Vector)
			pGravity = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pIsRunning As Boolean
	Public Property IsRunning() As Boolean
		Get
			Return pIsRunning
		End Get
		Set(ByVal value As Boolean)
			If value Then Start() Else [Stop]()
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pMaxAdjustmentPasses As Integer
	Public Property MaxAdjustmentPasses() As Integer
		Get
			Return pMaxAdjustmentPasses
		End Get
		Set(ByVal value As Integer)
			If value < 0 Then Throw New ArgumentOutOfRangeException("value")
			pMaxAdjustmentPasses = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pRate As Single
	Public Property Rate() As Single
		Get
			Return pRate
		End Get
		Set(ByVal value As Single)
			If value < 0 Then Throw New ArgumentOutOfRangeException("value")
			pRate = value
		End Set
	End Property

	Public Sub Dispose() Implements IDisposable.Dispose
		running.Close()
	End Sub

	Public Sub Start()
		pIsRunning = True
		prevSecond = DateTime.Now.Ticks
		running.Set()
	End Sub

	Public Sub StepSimulation()
		For Each i As Body In pBodies
			If i.Mass <= 0 Then Continue For
			i.Velocity += pGravity * pRate
			i.Location += i.Velocity * pRate
		Next
		For i As Integer = 0 To pBodies.Count - 1
			Dim a As Body = pBodies(i)
			Dim aRect As RectangleF = a.Rectangle
			For j As Integer = i + 1 To pBodies.Count - 1
				Dim b As Body = pBodies(j)
				If (a.CollisionMask And b.CollisionMask) = 0 Then Continue For
				Dim normal As Vector = Physics.CollisionAABB(aRect, b.Rectangle)
				If normal = Vector.Zero Then Continue For
				'If i.Mass > 0 AndAlso j.Mass > 0 Then Debugger.Break()
				Dim aNewVelocity As Vector = If(a.Mass > 0, Physics.Reflection(Physics.Collision(a.Mass, a.Velocity, b.Mass, b.Velocity, a.Restitution * b.Restitution), normal) * pRate, Vector.Zero)
				Dim bNewVelocity As Vector = If(b.Mass > 0, Physics.Reflection(Physics.Collision(b.Mass, b.Velocity, a.Mass, a.Velocity, b.Restitution * a.Restitution), -normal) * pRate, Vector.Zero)
				a.Velocity = aNewVelocity
				b.Velocity = bNewVelocity
				'If a.Mass > 0 Then
				'	Dim a1 As Vector = Physics.Collision(a.Mass, a.Velocity, b.Mass, b.Velocity, a.Restitution * b.Restitution) * pRate
				'	Dim b1 As Vector = Physics.Reflection(a1, normal)
				'	aNewVelocity = b1 * pRate
				'End If
				'If b.Mass > 0 Then b.Velocity = Physics.Reflection(Physics.Collision(b.Mass, b.Velocity, a.Mass, a.Velocity, b.Restitution * a.Restitution), normal) * pRate
			Next
		Next
		For k As Integer = 1 To pMaxAdjustmentPasses
			Dim adjustments As Integer = 0
			For Each i As Body In pBodies
				For Each j As Body In pBodies
					If i Is j OrElse (i.CollisionMask And j.CollisionMask) = 0 OrElse i.Mass <= 0 AndAlso j.Mass <= 0 Then Continue For
					Dim normal As Vector = Physics.CollisionAABB(i.Rectangle, j.Rectangle)
					If normal = Vector.Zero Then Continue For
					If i.Mass > 0 Then i.Location += normal * If(j.Mass > 0, j.Mass / (i.Mass + j.Mass), 1)
					If j.Mass > 0 Then j.Location -= normal * If(i.Mass > 0, i.Mass / (i.Mass + j.Mass), 1)
					adjustments += 1
				Next
			Next
			If adjustments = 0 Then Exit For
		Next
	End Sub

	Public Sub [Stop]()
		pIsRunning = False
		running.Reset()
	End Sub

	Private Sub DoWork(ByVal state As Object)
		Try
			While True
				running.WaitOne()
				Dim nowTicks As Long = DateTime.Now.Ticks
				fpsTemp += 1
				SyncLock pBodies
					StepSimulation()
				End SyncLock
				If nowTicks - prevSecond > 10000000 Then
					pFramesPerSecond = fpsTemp
					fpsTemp = 0
					prevSecond += 10000000
				End If
				Thread.Sleep(1)
			End While
		Catch ex As ObjectDisposedException
		End Try
	End Sub
End Class