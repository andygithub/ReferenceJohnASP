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
Imports System.ComponentModel.DataAnnotations

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<DebuggerDisplay("Models.RaceOptionList: {RaceId} {Name} {SortOrder}")>
Partial Public Class RaceOptionList
	Implements Interfaces.IEntityDates


	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RaceId")>
    Public Property RaceId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="Name")>
    Public Property Name As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="SortOrder")>
    Public Property SortOrder As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="IsActive")>
    Public Property IsActive As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="StartDate")>
    Public Property StartDate As Date

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="EndDate")>
    Public Property EndDate As Nullable(Of Date)

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="DateCreated")>
    Public Property DateCreated As Date Implements Interfaces.IEntityDates.DateCreated

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="LastChangeUser")>
    Public Property LastChangeUser As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="LastChangeDate")>
    Public Property LastChangeDate As Date Implements Interfaces.IEntityDates.LastChangeDate
    Public Overridable Property FormSimpleZeroes As ICollection(Of FormSimpleZero) = New HashSet(Of FormSimpleZero)
    Public Overridable Property FormSimpleZeroHistories As ICollection(Of FormSimpleZeroHistory) = New HashSet(Of FormSimpleZeroHistory)

End Class
