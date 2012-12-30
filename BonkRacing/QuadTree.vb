'Public Class Quad(Of T)

'	Private items As List(Of T)
'	Private subQuads As Quad(Of T)()

'	Public Sub New(ByVal source As IEnumerable(Of T), ByVal selector As Func(Of T, RectangleF))
'		pRectangle = Enumerable.Aggregate(source, RectangleF.Empty, _
'		Function(union As RectangleF, i As T) RectangleF.Union(union, selector(i)))
'		For Each i As T In source
'			Add(i)
'		Next



'		Dim count As Integer = Enumerable.Count(source)

'		Dim i As RectangleF = pRectangle
'		i.Inflate(-New Vector(i.Size) / 2)
'		i.Offset(i.Size.ToPointF())
'		Dim ii As RectangleF = i
'		ii.Offset(-ii.Width, 0)
'		Dim iii As RectangleF = i
'		iii.Offset(-iii.Width, -iii.Height)
'		Dim iv As RectangleF = i
'		iv.Offset(0, -iv.Height)

'		If count > 4 Then

'		End If

'	End Sub

'	Private pRectangle As RectangleF
'	Public ReadOnly Property Rectangle() As RectangleF
'		Get
'			Return pRectangle
'		End Get
'	End Property

'	Public Sub Add(ByVal item As T)
'		If items Is Nothing Then
'			items = New List(Of T)(4)
'		ElseIf items.Count = 4 Then

'		End If

'	End Sub
'End Class