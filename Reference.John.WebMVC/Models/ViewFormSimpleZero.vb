﻿Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class ViewFormSimpleZero

        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
       <StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
   <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="FirstName")>
        Public Property FirstName As String

        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
       <StringLengthAttribute(50, errormessageresourceType:=GetType(Reference.John.Resources.Resources.ValidationMessages), errormessageResourcename:="LengthExceeded")>
   <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="LastName")>
                Public Property LastName As String

        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="DefaultField")>
   <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="GenderId")>
        Public Property GenderId As Integer

        <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RaceId")>
        Public Property RaceId As Nullable(Of Integer)

        <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="RegionId")>
        Public Property RegionId As Nullable(Of Integer)

        Public Property ClientToken As Guid

        Public Property RowVersion As Byte()

        <Display(resourceType:=GetType(Reference.John.Resources.Resources.Names), name:="EthnicityId")>
        Public Property EthnicityId As Nullable(Of Integer)

        Public Property GenderList As IEnumerable(Of SelectListItem)

        Public Property RaceList As IEnumerable(Of SelectListItem)

        Public Property RegionList As IEnumerable(Of SelectListItem)

        Public Property EthnicityList As IEnumerable(Of SelectListItem)

    End Class

End Namespace