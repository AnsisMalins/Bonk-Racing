Option Explicit On
Option Infer Off
Option Strict On

''' <summary>Backported from .NET 3.5</summary>
Public Delegate Function Func(Of TResult)() As TResult
''' <summary>Backported from .NET 3.5</summary>
Public Delegate Function Func(Of T, TResult)(ByVal arg As T) As TResult
''' <summary>Backported from .NET 3.5</summary>
Public Delegate Function Func(Of T1, T2, TResult)(ByVal arg1 As T1, ByVal arg2 As T2) As TResult