'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class GenderOptionList
    Public Property Id As Integer
    Public Property Name As String
    Public Property SortOrder As Integer
    Public Property IsActive As Integer
    Public Property StartDate As Date
    Public Property EndDate As Nullable(Of Date)
    Public Property LastChangeUser As String
    Public Property LastChangeDate As Date

    Public Overridable Property FormSimpleZeroes As ICollection(Of FormSimpleZero) = New HashSet(Of FormSimpleZero)
    Public Overridable Property FormSimpleZeroHistories As ICollection(Of FormSimpleZeroHistory) = New HashSet(Of FormSimpleZeroHistory)

End Class
