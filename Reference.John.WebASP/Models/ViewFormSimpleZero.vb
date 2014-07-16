Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class ViewFormSimpleZero
        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="FirstName")>
       <StringLengthAttribute(50, ErrorMessage:="First Name cannot be longer than 50 characters.")>
       <Display(name:="First Name")>
        Public Property FirstName As String

        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="LastName")>
       <StringLengthAttribute(50, ErrorMessage:="Last Name cannot be longer than 50 characters.")>
       <Display(name:="Last Name")>
        Public Property LastName As String

        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Required(errormessageresourceType:=GetType(Reference.John.Resources.Resources.RequiredMessages), errormessageResourcename:="GenderId")>
       <Display(name:="Gender Id")>
        Public Property GenderId As Integer

        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Display(name:="Race Id")>
        Public Property RaceId As Nullable(Of Integer)

        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Display(name:="Region Id")>
        Public Property RegionId As Nullable(Of Integer)

        Public Property ClientToken As Guid

        Public Property RowVersion As Byte()

        ''' <summary>
        ''' #TODO Populate Model Descriptions
        ''' </summary>
        ''' <remarks>#TODO Populate Model Descriptions</remarks>
        <Display(name:="Ethnicity Id")>
        Public Property EthnicityId As Nullable(Of Integer)

        'Public Property GenderList As IEnumerable(Of SelectListItem)

        'Public Property RaceList As IEnumerable(Of SelectListItem)

        'Public Property RegionList As IEnumerable(Of SelectListItem)

        'Public Property EthnicityList As IEnumerable(Of SelectListItem)

    End Class

End Namespace