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
	<DebuggerDisplay("Models.Address: {Id} {FormSimpleZeroId} {AddressTypeId}")>
Partial Public Class Address
	Implements Interfaces.IEntityDates


	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="Id")>
    Public Property Id As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="FormSimpleZeroId")>
    Public Property FormSimpleZeroId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="AddressTypeId")>
    Public Property AddressTypeId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="AddressLine1")>
    Public Property AddressLine1 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="AddressLine2")>
    Public Property AddressLine2 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="AddressLine3")>
    Public Property AddressLine3 As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="City")>
    Public Property City As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="State")>
    Public Property State As String

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="Zip")>
    Public Property Zip As String

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

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="ClientToken")>
    Public Property ClientToken As System.Guid

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RowVersion")>
    Public Property RowVersion As Byte()
    Public Overridable Property AddressTypeOptionList As AddressTypeOptionList
    Public Overridable Property FormSimpleZero As FormSimpleZero

End Class
