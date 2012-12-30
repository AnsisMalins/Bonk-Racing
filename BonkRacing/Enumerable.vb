Option Explicit On
Option Infer Off
Option Strict On

''' <summary>Backported from .NET 3.5</summary>
Public NotInheritable Class Enumerable

	Private Sub New()
	End Sub

	Public Shared Function [Aggregate](Of TSource)(ByVal source As IEnumerable(Of TSource), ByVal func As Func(Of TSource, TSource, TSource)) As TSource
		If source Is Nothing Then Throw New ArgumentNullException("source")
		If func Is Nothing Then Throw New ArgumentNullException("func")
		Dim emptySet As Boolean = True
		Dim accumulate As TSource
		For Each i As TSource In source
			accumulate = If(emptySet, i, func(accumulate, i))
			emptySet = False
		Next
		If emptySet Then Throw New InvalidOperationException()
		Return accumulate
	End Function

	Public Shared Function [Aggregate](Of TSource, TAccumulate)(ByVal source As IEnumerable(Of TSource), ByVal seed As TAccumulate, ByVal func As Func(Of TAccumulate, TSource, TAccumulate)) As TAccumulate
		If source Is Nothing Then Throw New ArgumentNullException("source")
		If func Is Nothing Then Throw New ArgumentNullException("func")
		Dim accumulate As TAccumulate = seed
		For Each i As TSource In source
			accumulate = func(accumulate, i)
		Next
		Return accumulate
	End Function

	Public Shared Function All(Of TSource)(ByVal source As IEnumerable(Of Tsource), ByVal predicate As Func(Of Tsource, Boolean)) As Boolean
		If source Is Nothing Then Throw New ArgumentNullException("source")
		If predicate Is Nothing Then Throw New ArgumentNullException("predicate")
		For Each i As Tsource In source
			If Not predicate(i) Then Return False
		Next
		Return True
	End Function

	Public Shared Function Contains(Of TSource)(ByVal source As IEnumerable(Of TSource), ByVal value As TSource) As Boolean
		Return All(source, Function(i As TSource) Not EqualityComparer(Of TSource).Default.Equals(i, value))
	End Function

	Public Shared Function Count(Of TSource)(ByVal source As IEnumerable(Of TSource)) As Integer
		Dim collection As ICollection(Of TSource) = TryCast(source, ICollection(Of TSource))
		If collection IsNot Nothing Then Return collection.Count
		Return Aggregate(source, 0, Function(sum As Integer, i As TSource) sum + 1)
	End Function

	Public Shared Function Create(Of TSource)(ByVal source As IEnumerable(Of TSource)) As Enumerable(Of TSource)
		Return New Enumerable(Of TSource)(source)
	End Function

	Public Shared Function Max(ByVal source As IEnumerable(Of Integer)) As Integer
		Return Max(source, Function(i As Integer) i)
	End Function

	Public Shared Function Max(Of TSource)(ByVal source As IEnumerable(Of TSource), ByVal selector As Func(Of TSource, Integer)) As Integer
		If selector Is Nothing Then Throw New ArgumentNullException("selector")
		Return [Aggregate]([Select](source, selector), Function(accumulate As Integer, i As Integer) Math.Max(accumulate, i))
	End Function

	Public Shared Function Min(ByVal source As IEnumerable(Of Integer)) As Integer
		Return Min(source, Function(i As Integer) i)
	End Function

	Public Shared Function Min(Of TSource)(ByVal source As IEnumerable(Of TSource), ByVal selector As Func(Of TSource, Integer)) As Integer
		If selector Is Nothing Then Throw New ArgumentNullException("selector")
		Return [Aggregate]([Select](source, selector), Function(accumulate As Integer, i As Integer) Math.Min(accumulate, i))
	End Function

	Public Shared Function [Select](Of TSource, TResult)(ByVal source As IEnumerable(Of TSource), ByVal selector As Func(Of TSource, TResult)) As IEnumerable(Of TResult)
		If source Is Nothing Then Throw New ArgumentNullException("source")
		If selector Is Nothing Then Throw New ArgumentNullException("selector")
		Return New Enumerable(Of TSource, TResult)(source, selector)
	End Function

	Private Class Enumerable(Of TSource, TResult)
		Implements IEnumerable(Of TResult)

		Private selector As Func(Of TSource, TResult)
		Private source As IEnumerable(Of TSource)

		Public Sub New(ByVal source As IEnumerable(Of TSource), ByVal selector As Func(Of TSource, TResult))
			Me.selector = selector
			Me.source = source
		End Sub

		Public Function GetEnumerator() As IEnumerator(Of TResult) Implements IEnumerable(Of TResult).GetEnumerator
			Return New Enumerator(source.GetEnumerator(), selector)
		End Function

		Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
			Return GetEnumerator()
		End Function

		Private Class Enumerator
			Implements IEnumerator(Of TResult)

			Private selector As Func(Of TSource, TResult)
			Private source As IEnumerator(Of TSource)

			Public Sub New(ByVal source As IEnumerator(Of TSource), ByVal selector As Func(Of TSource, TResult))
				Me.selector = selector
				Me.source = source
			End Sub

			Public ReadOnly Property Current() As TResult Implements IEnumerator(Of TResult).Current
				Get
					Return selector(source.Current)
				End Get
			End Property

			Public Sub Dispose() Implements IDisposable.Dispose
				source.Dispose()
			End Sub

			Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
				Return source.MoveNext()
			End Function

			Public Sub Reset() Implements IEnumerator.Reset
				source.Reset()
			End Sub

			Private ReadOnly Property Current1() As Object Implements IEnumerator.Current
				Get
					Return Current
				End Get
			End Property
		End Class
	End Class
End Class

Public Class Enumerable(Of TSource)
	Implements IEnumerable(Of TSource)

	Private source As IEnumerable(Of TSource)

	Public Sub New(ByVal source As IEnumerable(Of TSource))
		If source Is Nothing Then Throw New ArgumentNullException("source")
		Me.source = source
	End Sub

	Public Function [Aggregate](ByVal func As Func(Of TSource, TSource, TSource)) As TSource
		Return Enumerable.Aggregate(Me, func)
	End Function

	Public Function [Aggregate](Of TAccumulate)(ByVal seed As TAccumulate, ByVal func As Func(Of TAccumulate, TSource, TAccumulate)) As TAccumulate
		Return Enumerable.Aggregate(Me, seed, func)
	End Function

	Public Function All(ByVal predicate As Func(Of TSource, Boolean)) As Boolean
		Return Enumerable.All(Me, predicate)
	End Function

	Public Function Contains(ByVal value As TSource) As Boolean
		Return Enumerable.Contains(Me, value)
	End Function

	Public Function Count() As Integer
		Return Enumerable.Count(Me)
	End Function

	Public Function Max(ByVal selector As Func(Of TSource, Integer)) As Integer
		Return Enumerable.Max(Me, selector)
	End Function

	Public Function Min(ByVal selector As Func(Of TSource, Integer)) As Integer
		Return Enumerable.Min(Me, selector)
	End Function

	Public Function [Select](Of TResult)(ByVal selector As Func(Of TSource, TResult)) As Enumerable(Of TResult)
		Return New Enumerable(Of TResult)(Enumerable.Select(Me, selector))
	End Function

	Private Function GetEnumerator() As IEnumerator(Of TSource) Implements IEnumerable(Of TSource).GetEnumerator
		Return source.GetEnumerator()
	End Function

	Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
		Return source.GetEnumerator()
	End Function
End Class