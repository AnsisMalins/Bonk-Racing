Option Explicit On
Option Infer Off
Option Strict On
Imports System

Public Class Body

	Public Sub New()
	End Sub

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pCollisionMask As Integer
	Public Property CollisionMask() As Integer
		Get
			Return pCollisionMask
		End Get
		Set(ByVal value As Integer)
			pCollisionMask = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pLocation As Vector
	Public Property Location() As Vector
		Get
			Return pLocation
		End Get
		Set(ByVal value As Vector)
			pLocation = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pMass As Single
	Public Property Mass() As Single
		Get
			Return pMass
		End Get
		Set(ByVal value As Single)
			pMass = value
		End Set
	End Property

	Public Property Rectangle() As RectangleF
		Get
			Return New RectangleF(Location - Size / 2, Size)
		End Get
		Set(ByVal value As RectangleF)
			Size = value.Size
			Location = value.Location + Size / 2
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pRestitution As Single
	Public Property Restitution() As Single
		Get
			Return pRestitution
		End Get
		Set(ByVal value As Single)
			pRestitution = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pSize As Vector
	Public Property Size() As Vector
		Get
			Return pSize
		End Get
		Set(ByVal value As Vector)
			pSize = value
		End Set
	End Property

	<DebuggerBrowsable(DebuggerBrowsableState.Never)> _
	Private pVelocity As Vector
	Public Property Velocity() As Vector
		Get
			Return pVelocity
		End Get
		Set(ByVal value As Vector)
			pVelocity = value
		End Set
	End Property
End Class