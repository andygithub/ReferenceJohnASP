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
Partial Public Class Address
	Implements Interfaces.IEntityDates


	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="Id")>
    <Display(name:="Id")>
    Public Property Id As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="FormSimpleZeroId")>
    <Display(name:="Form Simple Zero Id")>
    Public Property FormSimpleZeroId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="AddressTypeId")>
    <Display(name:="Address Type Id")>
    Public Property AddressTypeId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="AddressLine1")>
	<StringLengthAttribute(50, ErrorMessage:="Address Line1 cannot be longer than 50 characters.")>
    <Display(name:="Address Line1")>
    Public Property AddressLine1 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="Address Line2 cannot be longer than 50 characters.")>
    <Display(name:="Address Line2")>
    Public Property AddressLine2 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="Address Line3 cannot be longer than 50 characters.")>
    <Display(name:="Address Line3")>
    Public Property AddressLine3 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="City cannot be longer than 50 characters.")>
    <Display(name:="City")>
    Public Property City As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="State cannot be longer than 50 characters.")>
    <Display(name:="State")>
    Public Property State As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="Zip cannot be longer than 50 characters.")>
    <Display(name:="Zip")>
    Public Property Zip As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DateCreated")>
    <Display(name:="Date Created")>
    Public Property DateCreated As Date Implements Interfaces.IEntityDates.DateCreated

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, ErrorMessage:="Last Change User cannot be longer than 50 characters.")>
    <Display(name:="Last Change User")>
    Public Property LastChangeUser As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="LastChangeDate")>
    <Display(name:="Last Change Date")>
    Public Property LastChangeDate As Date Implements Interfaces.IEntityDates.LastChangeDate

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="ClientToken")>
    <Display(name:="Client Token")>
    Public Property ClientToken As System.Guid
    Public Overridable Property AddressTypeOptionList As AddressTypeOptionList
    Public Overridable Property FormSimpleZero As FormSimpleZero

End Class
