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
<DebuggerDisplay("Models.FormSimpleZero: {Id} {FirstName} {LastName}")>
Partial Public Class FormSimpleZero
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
   <StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="FirstName")>
 Public Property FirstName As String

    ''' <summary>
    ''' #TODO Populate Model Descriptions
    ''' </summary>
    ''' <remarks>#TODO Populate Model Descriptions</remarks>
    <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
   <StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="LastName")>
 Public Property LastName As String

    ''' <summary>
    ''' #TODO Populate Model Descriptions
    ''' </summary>
    ''' <remarks>#TODO Populate Model Descriptions</remarks>
    <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
<Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="GenderId")>
 Public Property GenderId As Integer

    ''' <summary>
    ''' #TODO Populate Model Descriptions
    ''' </summary>
    ''' <remarks>#TODO Populate Model Descriptions</remarks>
    <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RaceId")>
    Public Property RaceId As Nullable(Of Integer)

    ''' <summary>
    ''' #TODO Populate Model Descriptions
    ''' </summary>
    ''' <remarks>#TODO Populate Model Descriptions</remarks>
    <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RegionId")>
    Public Property RegionId As Nullable(Of Integer)

    ''' <summary>
    ''' #TODO Populate Model Descriptions
    ''' </summary>
    ''' <remarks>#TODO Populate Model Descriptions</remarks>
    <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="EthnicityId")>
    Public Property EthnicityId As Nullable(Of Integer)

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
    Public Overridable Property Addresses As ICollection(Of Address) = New HashSet(Of Address)
    Public Overridable Property EthnicityOptionList As EthnicityOptionList
    Public Overridable Property GenderOptionList As GenderOptionList
    Public Overridable Property RaceOptionList As RaceOptionList
    Public Overridable Property RegionOptionList As RegionOptionList
    Public Overridable Property FormEntity_xref As ICollection(Of FormEntity_xref) = New HashSet(Of FormEntity_xref)

End Class
