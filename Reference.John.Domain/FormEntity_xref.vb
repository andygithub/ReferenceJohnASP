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
	<DebuggerDisplay("Models.FormEntity_xref: {FormId} {EntityId} {RelationshipType}")>
Partial Public Class FormEntity_xref
	Implements Interfaces.IEntityDates


	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="FormId")>
    Public Property FormId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="EntityId")>
    Public Property EntityId As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RelationshipType")>
    Public Property RelationshipType As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	 <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RelationshipStatus")>
    Public Property RelationshipStatus As Integer

	''' <summary>
	''' #TODO Populate Model Descriptions
	''' </summary>
	''' <remarks>#TODO Populate Model Descriptions</remarks>
	<StringLengthAttribute(200, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RelationshipDescription")>
    Public Property RelationshipDescription As String

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
    Public Overridable Property Entity As Entity
    Public Overridable Property FormSimpleZero As FormSimpleZero

End Class