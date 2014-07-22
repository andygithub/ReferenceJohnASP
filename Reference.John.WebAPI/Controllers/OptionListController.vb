Imports System.Data.Entity
Imports System.Net
Imports System.Web.Http
Imports Reference.John.Repository
Imports Reference.John.Infrastructure.Logging
Imports System.Web.Http.Description
Imports AutoMapper.QueryableExtensions

Namespace Controllers

    Public Class OptionListController
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
        Public Function GetValues() As Reference.John.UI.Model.OptionListSet
            Return New Reference.John.UI.Model.OptionListSet With {
            .GenderList = _repository.GetQuery(Of Reference.John.Domain.GenderOptionList).AsNoTracking.Project.To(Of Reference.John.UI.Model.OptionList)(),
            .RaceList = _repository.GetQuery(Of Reference.John.Domain.RaceOptionList).AsNoTracking.Project.To(Of Reference.John.UI.Model.OptionList)(),
            .RegionList = _repository.GetQuery(Of Reference.John.Domain.RegionOptionList).AsNoTracking.Project.To(Of Reference.John.UI.Model.OptionList)(),
            .EthnicityList = _repository.GetQuery(Of Reference.John.Domain.EthnicityOptionList).AsNoTracking.Project.To(Of Reference.John.UI.Model.OptionList)()
        }
        End Function

    End Class
End Namespace