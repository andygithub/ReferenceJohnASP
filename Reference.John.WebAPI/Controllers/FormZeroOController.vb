Imports System.Net
Imports System.Web.Http
Imports System.Web.Http.OData
Imports Reference.John.Repository
Imports Reference.John.Infrastructure.Logging
Imports System.Data.Entity.Infrastructure


Namespace Controllers

    'The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    ''' <summary>
    ''' Odata test endpoint
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FormSimpleZeroController
        Inherits ODataController


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

        ' GET: odata/FormZeroO
        'it expects a get method name for the default action.  if some other method name is desired than use the attribute to map it.
        'http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint
        <EnableQuery>
        Public Function [Get]() As IQueryable(Of Reference.John.Domain.FormSimpleZero)
            System.Threading.Thread.Sleep(3000)
            Return _repository.GetQuery(Of Reference.John.Domain.FormSimpleZero)()
        End Function

        ' GET: odata/FormZeroO/5
        <EnableQuery>
        Public Function [Get](<FromODataUri> key As Guid) As SingleResult(Of Reference.John.Domain.FormSimpleZero)
            'add null check
            Return SingleResult.Create(Of Reference.John.Domain.FormSimpleZero)(_repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = key))
        End Function

        ' POST: odata/FormZeroO
        Public Function PostValue(<FromBody()> ByVal value As Reference.John.Domain.FormSimpleZero) As IHttpActionResult
            'expect this to be handled in the action filter
            'If Not ModelState.IsValid Then
            '    Return BadRequest(ModelState)
            'End If
            'obviously more validation or logic would be applied before the save.

            _repository.Add(value)
            _repository.UnitOfWork.SaveChanges()

            Return Created(value)
        End Function

        ' PUT: odata/FormZeroO/5
        Public Function PutValue(ByVal key As Guid, <FromBody()> ByVal value As Reference.John.Domain.FormSimpleZero) As IHttpActionResult
            'expect this to be handled in the action filter
            'If Not ModelState.IsValid Then
            '    Return BadRequest(ModelState)
            'End If
            'obviously more validation or logic would be applied before the save.

            If Not key = value.ClientToken Then Return BadRequest()

            _repository.Attach(Of Reference.John.Domain.FormSimpleZero)(value)
            'db.Entry(formSimpleZero).State = EntityState.Modified

            Try
                _repository.UnitOfWork.SaveChanges()
                'Await db.SaveChangesAsync()
            Catch ex As DbUpdateConcurrencyException
                If Not (FormSimpleZeroExists(key)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return Updated(value)
        End Function

        ' PATCH: odata/FormSimpleZeroesControllerOData(5)
        <AcceptVerbs("PATCH", "MERGE")>
        Public Function Patch(<FromODataUri> ByVal ClientToken As Guid, ByVal patchValue As Delta(Of Reference.John.Domain.FormSimpleZero)) As IHttpActionResult
            'expect this to be handled in the action filter
            'If Not ModelState.IsValid Then
            '    Return BadRequest(ModelState)
            'End If
            'obviously more validation or logic would be applied before the save.

            Dim item As Reference.John.Domain.FormSimpleZero = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
            If IsNothing(item) Then Return NotFound()

            patchValue.Patch(item)

            Try
                _repository.UnitOfWork.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (FormSimpleZeroExists(ClientToken)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return Updated(item)
        End Function

        ' DELETE: odata/FormZeroO/5
        Public Function DeleteValue(ByVal ClientToken As Guid) As IHttpActionResult
            Dim item As Reference.John.Domain.FormSimpleZero = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
            If IsNothing(item) Then
                Return NotFound()
            End If

            _repository.Delete(Of Reference.John.Domain.FormSimpleZero)(item)
            _repository.UnitOfWork.SaveChanges()

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/Addresses
        <EnableQuery>
        Function GetAddresses(<FromODataUri> ByVal key As Integer) As IQueryable(Of Reference.John.Domain.Address)
            Return _repository.GetQuery(Of Reference.John.Domain.Address)(Function(m) m.Id = key) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).SelectMany(Function(m) m.Addresses)
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/EthnicityOptionList
        <EnableQuery>
        Function GetEthnicityOptionList(<FromODataUri> ByVal key As Integer) As SingleResult(Of Reference.John.Domain.EthnicityOptionList)
            Return SingleResult.Create(Of Reference.John.Domain.EthnicityOptionList)(_repository.FindOne(Of Reference.John.Domain.EthnicityOptionList)(Function(m) m.EthnicityId = key)) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).Select(Function(m) m.EthnicityOptionList))
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/FormEntity_xref
        <EnableQuery>
        Function GetFormEntity_xref(<FromODataUri> ByVal key As Integer) As IQueryable(Of Reference.John.Domain.FormEntity_xref)
            Return _repository.GetQuery(Of Reference.John.Domain.FormEntity_xref)(Function(m) m.FormId = key) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).SelectMany(Function(m) m.FormEntity_xref)
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/GenderOptionList
        <EnableQuery>
        Function GetGenderOptionList(<FromODataUri> ByVal key As Integer) As SingleResult(Of Reference.John.Domain.GenderOptionList)
            Return SingleResult.Create(Of Reference.John.Domain.GenderOptionList)(_repository.FindOne(Of Reference.John.Domain.GenderOptionList)(Function(m) m.GenderId = key)) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).Select(Function(m) m.GenderOptionList))
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/RaceOptionList
        <EnableQuery>
        Function GetRaceOptionList(<FromODataUri> ByVal key As Integer) As SingleResult(Of Reference.John.Domain.RaceOptionList)
            Return SingleResult.Create(Of Reference.John.Domain.RaceOptionList)(_repository.FindOne(Of Reference.John.Domain.RaceOptionList)(Function(m) m.RaceId = key)) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).Select(Function(m) m.RaceOptionList))
        End Function

        ' GET: odata/FormSimpleZeroesControllerOData(5)/RegionOptionList
        <EnableQuery>
        Function GetRegionOptionList(<FromODataUri> ByVal key As Integer) As SingleResult(Of Reference.John.Domain.RegionOptionList)
            Return SingleResult.Create(Of Reference.John.Domain.RegionOptionList)(_repository.FindOne(Of Reference.John.Domain.RegionOptionList)(Function(m) m.RegionId = key)) 'db.FormSimpleZeroes.Where(Function(m) m.Id = key).Select(Function(m) m.RegionOptionList))
        End Function

        <EnableQuery>
        Function GetAddressTypeOptionList(<FromODataUri> ByVal key As Integer) As SingleResult(Of Reference.John.Domain.AddressTypeOptionList)
            Return SingleResult.Create(Of Reference.John.Domain.AddressTypeOptionList)(_repository.FindOne(Of Reference.John.Domain.AddressTypeOptionList)(Function(m) m.AddressTypeId = key))
        End Function

        Private Function FormSimpleZeroExists(ByVal ClientToken As Guid) As Boolean
            Return _repository.Count(Of Reference.John.Domain.FormSimpleZero)(Function(e) e.ClientToken = ClientToken) > 0
        End Function



    End Class
End Namespace