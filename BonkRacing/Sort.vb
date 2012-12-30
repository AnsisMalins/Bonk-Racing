Option Explicit On
Option Infer Off
Option Strict On
Imports System

Public NotInheritable Class Sort

	Private Sub New()
	End Sub

	''' <summary>http://www.sorting-algorithms.com/insertion-sort</summary>
	Public Shared Sub Insertion(Of T)(ByVal list As IList(Of T))
		Insertion(list, Function(x As T, y As T) Comparer(Of T).Default.Compare(x, y))
	End Sub

	''' <summary>http://www.sorting-algorithms.com/insertion-sort</summary>
	Public Shared Sub Insertion(Of T)(ByVal list As IList(Of T), ByVal comparison As Comparison(Of T))
		If list Is Nothing Then Throw New ArgumentNullException("list")
		If comparison Is Nothing Then Throw New ArgumentNullException("comparison")
		For i As Integer = 1 To list.Count - 1
			Dim k As Integer = i
			While k > 0 AndAlso comparison(list(k), list(k - 1)) < 0
				Dim j As T = list(k)
				list(k) = list(k - 1)
				list(k - 1) = j
				k -= 1
			End While
		Next
	End Sub
End Class