Imports System.Configuration
Imports System.Data.Common
Imports System.Reflection
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects

Namespace Infrastructure

    ''ToDo cleanup this class and add rest of settings to context setup.
    Public Class DbContextBuilder(Of T As DbContext)
        'Inherits DbModelBuilder
        Implements IDbContextBuilder(Of T)

        'Private ReadOnly _factory As DbProviderFactory
        Private ReadOnly _cnStringSettings As ConnectionStringSettings
        Private ReadOnly _lazyLoadingEnabled As Boolean
        Private ReadOnly _loggingDelegate As Action(Of String)


        Public Sub New(lazyLoadingEnabled As Boolean, connectionStringName As String)
            _cnStringSettings = ConfigurationManager.ConnectionStrings(connectionStringName)
            _lazyLoadingEnabled = lazyLoadingEnabled
            _loggingDelegate = (Sub(val) Console.WriteLine(val))
        End Sub

        Public Sub New(lazyLoadingEnabled As Boolean, connectionStringName As String, loggingDelegate As Action(Of String))
            _cnStringSettings = ConfigurationManager.ConnectionStrings(connectionStringName)
            _lazyLoadingEnabled = lazyLoadingEnabled
            _loggingDelegate = loggingDelegate
        End Sub

        ''' <summary>
        ''' Creates a new DbContext.  This maps back to the default generated context type.  This could be overridden with a type parameter or specifying a context on the repository. "/>.
        ''' </summary>
        ''' <returns></returns>
        Public Function BuildDbContext(Optional modelType As Type = Nothing) As T Implements IDbContextBuilder(Of T).BuildDbContext
            Dim ctx As ObjectContext

            If modelType Is Nothing Then
                If _cnStringSettings Is Nothing Then
                    ctx = DirectCast(New Model.Reference_JohnEntities, IObjectContextAdapter).ObjectContext
                Else
                    ctx = DirectCast(New Model.Reference_JohnEntities(_cnStringSettings.Name), IObjectContextAdapter).ObjectContext
                End If
            Else
                ctx = DirectCast(Activator.CreateInstance(modelType), IObjectContextAdapter).ObjectContext
            End If
            'Dim ctx As ObjectContext = DirectCast(New Model.ExtendedSimpleEntities, IObjectContextAdapter).ObjectContext

            ctx.ContextOptions.LazyLoadingEnabled = Me._lazyLoadingEnabled

            Dim _object = DirectCast(New DbContext(ctx, True), T)
            _object.Database.Log = _loggingDelegate

            Return _object 'DirectCast(New DbContext(ctx, True), T)
        End Function


    End Class
End Namespace