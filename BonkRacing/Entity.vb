Option Explicit On
Option Infer Off
Option Strict On
Imports System.Drawing
Imports System.Xml

Public Class Entity

	Public Sub New(ByVal xml As XmlNode)
		Body = New Body()
		Dim attr1 As XmlAttribute, attr2 As XmlAttribute
		attr1 = xml.Attributes("x")
		attr2 = xml.Attributes("y")
		If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then Body.Location = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
		attr1 = xml.Attributes("vx")
		attr2 = xml.Attributes("vy")
		If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then Body.Velocity = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
		attr1 = xml.Attributes("w")
		attr2 = xml.Attributes("h")
		If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then Body.Size = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
		attr1 = xml.Attributes("mass")
		Body.Mass = If(attr1 IsNot Nothing, Single.Parse(attr1.Value), 1000000)
		attr1 = xml.Attributes("rest")
		If attr1 IsNot Nothing Then Body.Restitution = Single.Parse(attr1.Value)
		attr1 = xml.Attributes("zorder")
		If attr1 IsNot Nothing Then ZOrder = Integer.Parse(attr1.Value)
		attr1 = xml.Attributes("solid")
		If attr1 IsNot Nothing Then IsSolid = Boolean.Parse(attr1.Value)
		attr1 = xml.Attributes("locked")
		If attr1 IsNot Nothing Then IsLocked = Boolean.Parse(attr1.Value)
		attr1 = xml.Attributes("image")
		attr2 = xml.Attributes("brush")
		If attr1 IsNot Nothing Then
			ImageFile = attr1.Value
			Bitmap = New Bitmap(ImageFile)
		ElseIf attr2 IsNot Nothing Then
			ImageFile = attr2.Value
			Brush = New TextureBrush(New Bitmap(ImageFile))
			attr1 = xml.Attributes("tx")
			attr2 = xml.Attributes("ty")
			If attr1 IsNot Nothing AndAlso attr2 IsNot Nothing Then TextureOffset = New Vector(Single.Parse(attr1.Value), Single.Parse(attr2.Value))
			Brush.TranslateTransform(TextureOffset.X, TextureOffset.Y)
		Else
			attr1 = xml.Attributes("color")
			If attr1 IsNot Nothing Then Color = Color.FromArgb(Convert.ToInt32(attr1.Value, 16)) Else Color = Drawing.Color.Blue
		End If
		attr1 = xml.Attributes("name")
		If attr1 IsNot Nothing Then Name = attr1.Value
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap)
		Initialize(rectangle)
		Me.Bitmap = bitmap
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal brush As TextureBrush)
		Initialize(rectangle)
		Me.Brush = brush
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color)
		Initialize(rectangle)
		Me.Color = color
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal bitmap As Bitmap, ByVal mass As Single, ByVal restitution As Single)
		Initialize(rectangle, mass, restitution)
		Me.Bitmap = bitmap
	End Sub

	Public Sub New(ByVal rectangle As RectangleF, ByVal color As Color, ByVal mass As Single, ByVal restitution As Single)
		Initialize(rectangle, mass, restitution)
		Me.Color = color
	End Sub

	Private Sub Initialize(ByVal rectangle As RectangleF)
		Body = New Body()
		Body.Rectangle = rectangle
		IsLocked = True
		IsSolid = True
		Body.Mass = 0
	End Sub

	Private Sub Initialize(ByVal rectangle As RectangleF, ByVal mass As Single, ByVal restitution As Single)
		Body = New Body()
		Body.Rectangle = rectangle
		Body.Mass = mass
		IsSolid = True
		Body.Restitution = restitution
	End Sub

	Public Bitmap As Bitmap
	Public Brush As TextureBrush
	Public Color As Color
	Public TextureOffset As Vector

	Public Body As Body

	Public Property IsLocked() As Boolean
		Get
			Return Body.Mass <= 0
		End Get
		Set(ByVal value As Boolean)
			If value Then Body.Mass = 0
		End Set
	End Property

	Public Property IsSolid() As Boolean
		Get
			Return (Body.CollisionMask And 1) > 0
		End Get
		Set(ByVal value As Boolean)
			Body.CollisionMask = If(value, 1, 0)
		End Set
	End Property

	Public IsColliding As Boolean

	Public ZOrder As Integer

	Public ImageFile As String

	Public Name As String

	Public Function ToXml() As String
		Return "<entity" _
		& If(Not String.IsNullOrEmpty(Name), " name=""" & Name & """", "") _
		& " x=""" & Body.Location.X.ToString() & """" _
		& " y=""" & Body.Location.Y.ToString() & """" _
		& " vx=""" & Body.Velocity.X.ToString() & """" _
		& " vy=""" & Body.Velocity.Y.ToString() & """" _
		& " w=""" & Body.Size.X.ToString() & """" _
		& " h=""" & Body.Size.Y.ToString() & """" _
		& " mass=""" & Body.Mass.ToString() & """" _
		& " locked=""" & IsLocked.ToString() & """" _
		& " solid=""" & IsSolid.ToString() & """" _
		& " rest=""" & Body.Restitution.ToString() & """" _
		& " zorder=""" & ZOrder.ToString() & """" _
		& If(Bitmap IsNot Nothing, " image=""" & ImageFile & """", If(Brush IsNot Nothing, " brush=""" & ImageFile & """", " color=""" & Convert.ToString(Color.ToArgb(), 16) & """")) _
		& If(Brush IsNot Nothing, " tx=""" & TextureOffset.X.ToString() & """ ty=""" & TextureOffset.Y.ToString() & """", "") _
		& "/>"
	End Function

	Public Overridable Sub PhysicsCallback(ByVal world As World)
		If Actor IsNot Nothing Then Actor.PhysicsCallback(Me, world)
	End Sub

	Public Overridable Sub RenderCallback(ByVal mainForm As MainForm)
		If Actor IsNot Nothing Then Actor.RenderCallback(Me, mainForm)
	End Sub

	Public Overridable Sub CollideCallback(ByVal other As Entity, ByVal side As Integer)
		If Actor IsNot Nothing Then Actor.CollideCallback(Me, other, side)
	End Sub

	Public Actor As Actor

	Public Shared Function Collides(ByVal i As Entity, ByVal j As Entity, ByVal world As World) As Boolean
		Dim iRect As New RectangleF(i.Body.Location + i.Body.Velocity * world.Rate - i.Body.Size / 2, i.Body.Size)
		Dim jRect As New RectangleF(j.Body.Location + j.Body.Velocity * world.Rate - j.Body.Size / 2, j.Body.Size)
		Return RectangleF.Intersect(iRect, jRect) <> RectangleF.Empty
	End Function
End Class

Public Class Actor

	Public Overridable Sub PhysicsCallback(ByVal entity As Entity, ByVal world As World)
	End Sub

	Public Overridable Sub RenderCallback(ByVal entity As Entity, ByVal mainForm As MainForm)
	End Sub

	Public Overridable Sub CollideCallback(ByVal entity As Entity, ByVal other As Entity, ByVal side As Integer)
	End Sub
End Class

Public Class PinkiePie
	Inherits Actor

	Private touchingGound As Boolean
	Private caughtPlayer As Boolean

	Public Overrides Sub CollideCallback(ByVal entity As Entity, ByVal other As Entity, ByVal side As Integer)
		If side = 3 AndAlso other.Name = "ground" Then touchingGound = True
		If other.Name = "player" Then caughtPlayer = True
	End Sub

	Public Overrides Sub RenderCallback(ByVal entity As Entity, ByVal mainForm As MainForm)
		If touchingGound Then
			entity.Body.Velocity += New Vector(1.5 * mainForm.jx, -1.5 * mainForm.jy) ' Bounce! Whee!
			touchingGound = False
		End If
		If caughtPlayer Then mainForm.GameLose()
	End Sub
End Class

Public Class Scene

End Class