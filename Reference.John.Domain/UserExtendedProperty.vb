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
Partial Public Class UserExtendedProperty
	Implements Interfaces.IEntityDates


	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="UserId")>
	<StringLengthAttribute(50, ErrorMessage:="User Id cannot be longer than 50 characters.")>
    Public Property UserId As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="KeyId")>
	<StringLengthAttribute(50, ErrorMessage:="Key Id cannot be longer than 50 characters.")>
    Public Property KeyId As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="Value")>
    Public Property Value As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DateCreated")>
    Public Property DateCreated As Date Implements Interfaces.IEntityDates.DateCreated

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DateExpired")>
    Public Property DateExpired As Date

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="LastChangeDate")>
    Public Property LastChangeDate As Date Implements Interfaces.IEntityDates.LastChangeDate
End Class
