Imports System.Data.Entity
Imports System.Net
Imports System.Web.Http
Imports Reference.John.Repository
Imports Reference.John.Infrastructure.Logging
Imports System.Web.Http.Description
Imports AutoMapper.QueryableExtensions

Namespace Controllers

    Public Class FormSimpleEFController
        Inherits ApiController

        Private _lazyrepository As Lazy(Of IRepository)
        Private _lazylogger As Lazy(Of ILogger)

        'have these properties to hide the lazy usage from within controller 
        Private ReadOnly Property _repository As IRepository
            Get
                Return _lazyrepository.Value
            End Get
        End Property

        Private ReadOnly Property _logger As ILogger
            Get
                Return _lazylogger.Value
            End Get
        End Property

        Public Sub New(repository As Lazy(Of IRepository), logger As Lazy(Of ILogger))
            If repository Is Nothing Then Throw New ArgumentNullException("repository")
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _lazyrepository = repository
            _lazylogger = logger
        End Sub

        ' GET: api/FormSimpleEF
        Public Function GetValues() As IQueryable(Of Reference.John.UI.Model.SearchResult)
            System.Threading.Thread.Sleep(3000)
            Return _repository.GetQuery(Of Reference.John.Domain.SearchResult).AsNoTracking.Project.To(Of Reference.John.UI.Model.SearchResult)()
        End Function

        ' GET: api/FormSimpleEF/5
        <ResponseType(GetType(Reference.John.UI.Model.ViewFormSimpleZero))>
        Public Async Function GetValue(ByVal ClientToken As Guid) As Threading.Tasks.Task(Of IHttpActionResult)
            Dim item As Reference.John.UI.Model.ViewFormSimpleZero = Await _repository.GetQuery(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken) _
                                    .Project.To(Of Reference.John.UI.Model.ViewFormSimpleZero)().FirstOrDefaultAsync
            If IsNothing(item) Then Return NotFound()

            Return Ok(item)
        End Function

        ' POST: api/FormSimpleEF
        Public Function PostValue(<FromBody()> ByVal value As Reference.John.UI.Model.ViewFormSimpleZero) As IHttpActionResult
            'modelstate should be validated by action filter, if this is not in place then this would need to be in active
            'If Not ModelState.IsValid Then
            '    Return BadRequest(ModelState)
            'End If
            'map view model into domain model 
            Dim _item As New Reference.John.Domain.FormSimpleZero
            With _item
                .FirstName = value.FirstName
                .LastName = value.LastName
                .GenderId = value.GenderId
                .RaceId = value.RaceId
                .RegionId = value.RegionId
                .EthnicityId = value.EthnicityId
                .LastChangeUser = "api" & Now.Minute
            End With

            _repository.Add(Of Reference.John.Domain.FormSimpleZero)(_item)
            _repository.UnitOfWork.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.ClientToken = value.ClientToken}, value)
        End Function

        ' PUT: api/FormSimpleEF/5
        <ResponseType(GetType(Void))>
        Public Function PutValue(ByVal ClientToken As Guid, <FromBody()> ByVal value As Reference.John.UI.Model.ViewFormSimpleZero) As IHttpActionResult
            'modelstate should be validated by action filter, if this is not in place then this would need to be in active
            'If Not ModelState.IsValid Then
            '    Return BadRequest(ModelState)
            'End If

            If Not ClientToken = value.ClientToken Then Return BadRequest()

            Dim _item As Reference.John.Domain.FormSimpleZero = _repository.GetQuery(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = value.ClientToken).AsNoTracking.FirstOrDefault
            If IsNothing(_item) Then Return NotFound()
            'map in the fields we care about from the message
            With _item
                .FirstName = value.FirstName
                .LastName = value.LastName
                .GenderId = value.GenderId
                .RaceId = value.RaceId
                .RegionId = value.RegionId
                .EthnicityId = value.EthnicityId
                .RowVersion = value.RowVersion
                .LastChangeUser = "api" & Now.Minute
            End With

            'would expect that this would be passed in as a DTO and then there would be a mapping effort before an attach or update takes place.
            'db.Entry(formSimpleZero).State = EntityState.Modified
            _repository.Attach(Of Reference.John.Domain.FormSimpleZero)(_item)

            Try
                _repository.UnitOfWork.SaveChanges()
            Catch ex As Data.DBConcurrencyException
                If Not (FormSimpleZeroExists(ClientToken)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' DELETE: api/FormSimpleEF/5
        <ResponseType(GetType(Reference.John.Domain.FormSimpleZero))>
        Public Function DeleteValue(ByVal ClientToken As Guid) As IHttpActionResult
            Dim item As Reference.John.Domain.FormSimpleZero = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
            If IsNothing(item) Then
                Return NotFound()
            End If

            _repository.Delete(Of Reference.John.Domain.FormSimpleZero)(item)
            _repository.UnitOfWork.SaveChanges()

            Return Ok(item)
        End Function

        Private Function FormSimpleZeroExists(ByVal ClientToken As Guid) As Boolean
            Return _repository.Count(Of Reference.John.Domain.FormSimpleZero)(Function(e) e.ClientToken = ClientToken) > 0
        End Function

    End Class
End Namespace