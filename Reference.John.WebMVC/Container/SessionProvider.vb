'Public Class SessionProvider
'    Implements ISessionProvider

'    Dim _logger As Reference.John.Core.Logging.ILogger
'    Dim _sessionRepository As ISessionRepository
'    Dim _sessionExtendedRepository As ISessionExtendedRepository
'    Dim _instance As ISessionInstance
'    Dim _IsUpdatedSessionInstance As Boolean

'    Sub New(logger As Reference.John.Core.Logging.ILogger, sessionRepository As ISessionRepository, sessionExtendedRepository As ISessionExtendedRepository)
'        If logger Is Nothing Then Throw New ArgumentNullException("logger")
'        If sessionRepository Is Nothing Then Throw New ArgumentNullException("sessionRepository")
'        If sessionExtendedRepository Is Nothing Then Throw New ArgumentNullException("sessionExtendedRepository")
'        _logger = logger
'        _sessionRepository = sessionRepository
'        _sessionExtendedRepository = sessionExtendedRepository
'        SessionId = Guid.NewGuid
'        _logger.Debug("Session Id set - {0}", SessionId)
'    End Sub

'    Public Sub New(logger As Reference.John.Core.Logging.ILogger, sessionRepository As ISessionRepository, sessionExtendedRepository As ISessionExtendedRepository, userId As String)
'        If logger Is Nothing Then Throw New ArgumentNullException("logger")
'        If sessionRepository Is Nothing Then Throw New ArgumentNullException("sessionRepository")
'        If sessionExtendedRepository Is Nothing Then Throw New ArgumentNullException("sessionExtendedRepository")
'        _logger = logger
'        _sessionRepository = sessionRepository
'        SessionId = Guid.NewGuid
'        _sessionExtendedRepository = sessionExtendedRepository
'        _logger.Debug("Session Id set - {0}", SessionId)
'        Me.UserId = userId
'    End Sub

'    Public Function LoadExtendedItem(key As String) As Object Implements ISessionProvider.LoadExtendedItem
'        _logger.Debug("LoadExtendedItem - {0}", key)
'        'load implementation to the database here
'        Return _sessionExtendedRepository.LoadExtendedItem(key, UserId)
'    End Function

'    Public Sub LoadInstance() Implements ISessionProvider.LoadInstance
'        'attempt to load session from store
'        'if attempt is nothing then
'        Dim item As ISessionInstance = _sessionRepository.Load(UserId)
'        If item Is Nothing Then
'            _logger.Debug("Loading Session Instance existing session not found.")
'            IsNewSessionInstance = True
'            _instance = New SessionInstance
'        Else
'            _logger.Debug("Loading Session Instance existing session found.")
'            _instance = item
'        End If
'    End Sub

'    Public Sub SaveExtendedItem(key As String, item As Object) Implements ISessionProvider.SaveExtendedItem
'        _logger.Debug("SaveExtendedItem - {0}", key)
'        'save implementation to the database here
'        _sessionExtendedRepository.SaveExtendedItem(key, UserId, item)
'    End Sub

'    Public Sub SaveInstance() Implements ISessionProvider.SaveInstance
'        _sessionRepository.Save(UserId, _instance)
'    End Sub

'    Public Property Instance As ISessionInstance Implements ISessionProvider.Instance
'        Get
'            'if null then attempt to load using load instance method
'            If _instance Is Nothing Then
'                LoadInstance()
'            End If
'            Return _instance
'        End Get
'        Set(value As ISessionInstance)
'            'assume that thie set of the update to true would not be enough and that there would need to be a flag bubbled up from the instance class.
'            IsUpdatedSessionInstance = True
'            _instance = value
'        End Set
'    End Property

'    Public Property IsNewSessionInstance As Boolean Implements ISessionProvider.IsNewSessionInstance
'    Public Property IsUpdatedSessionInstance As Boolean Implements ISessionProvider.IsUpdatedSessionInstance
'        Get
'            If _IsUpdatedSessionInstance Then Return True
'            'don't want to spend the time loading the instance or init it if it wasn't used.  Expect this property to only by used by the save process
'            If _instance Is Nothing Then Return False
'            If Instance.IsSessionInstanceChanged Then Return True
'            Return False
'        End Get
'        Set(value As Boolean)
'            _IsUpdatedSessionInstance = value
'        End Set
'    End Property

'    ''' <summary>
'    ''' This id should be unique for the length of a request.  If session is saved then the unique user id should be used.
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property SessionId As Guid Implements ISessionProvider.SessionId

'    ''' <summary>
'    ''' Unique identifer of a user that is used to uniquely identify stored information in the data store
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property UserId As String Implements ISessionProvider.UserId

'End Class

'Public Class SessionInstance
'    Implements ISessionInstance

'    Public Property ComplaintId As Long? Implements ISessionInstance.ComplaintId
'        Get
'            Return _ComplaintId
'        End Get
'        Set(value As Long?)
'            _IsSessionInstanceChanged = True
'            _ComplaintId = value
'        End Set
'    End Property

'    Public Property CurrentOrganizationName As String Implements ISessionInstance.CurrentOrganizationName
'        Get
'            Return _CurrentOrganizationName
'        End Get
'        Set(value As String)
'            _IsSessionInstanceChanged = True
'            _CurrentOrganizationName = value
'        End Set
'    End Property

'    Public Property IncidentId As Long? Implements ISessionInstance.IncidentId
'        Get
'            Return _IncidentId
'        End Get
'        Set(value As Long?)
'            _IsSessionInstanceChanged = True
'            _IncidentId = value
'        End Set
'    End Property

'    Public Property UserName As String Implements ISessionInstance.UserName
'        Get
'            Return _UserName
'        End Get
'        Set(value As String)
'            _IsSessionInstanceChanged = True
'            _UserName = value
'        End Set
'    End Property

'    Public ReadOnly Property IsSessionInstanceChanged As Boolean Implements ISessionInstance.IsSessionInstanceChanged
'        Get
'            Return _IsSessionInstanceChanged
'        End Get
'    End Property

'    Dim _IsSessionInstanceChanged As Boolean
'    Dim _ComplaintId As Long?
'    Dim _CurrentOrganizationName As String
'    Dim _IncidentId As Long?
'    Dim _UserName As String

'End Class

'Public Class SessionRepository
'    Implements ISessionRepository

'    Public Function Load(userId As String) As ISessionInstance Implements ISessionRepository.Load
'        Return New SessionInstance With {.ComplaintId = -200, .CurrentOrganizationName = "temp_org_name", .IncidentId = -300, .UserName = "adfadf_afad"}
'    End Function

'    Public Sub Save(userId As String, item As ISessionInstance) Implements ISessionRepository.Save
'        'save process goes here
'    End Sub

'End Class

'Public Class SessionExtendedRepository
'    Implements ISessionExtendedRepository

'    Public Function LoadExtendedItem(key As String, userId As String) As Object Implements ISessionExtendedRepository.LoadExtendedItem
'        Throw New NotImplementedException
'    End Function

'    Public Sub SaveExtendedItem(key As String, userId As String, item As Object) Implements ISessionExtendedRepository.SaveExtendedItem
'        Throw New NotImplementedException
'    End Sub

'End Class

'Public Interface ISessionProvider
'    Property SessionId As Guid
'    Property UserId As String

'    Property Instance As ISessionInstance

'    Sub LoadInstance()
'    Sub SaveInstance()

'    Function LoadExtendedItem(key As String) As Object
'    Sub SaveExtendedItem(key As String, item As Object)

'    Property IsNewSessionInstance As Boolean
'    Property IsUpdatedSessionInstance As Boolean
'End Interface

'Public Interface ISessionInstance

'    Property IncidentId As Long?
'    Property ComplaintId As Long?
'    Property CurrentOrganizationName As String
'    Property UserName As String

'    ReadOnly Property IsSessionInstanceChanged As Boolean
'End Interface

'Public Interface ISessionRepository
'    Function Load(userId As String) As ISessionInstance

'    Sub Save(userId As String, item As ISessionInstance)

'End Interface

'Public Interface ISessionExtendedRepository
'    Function LoadExtendedItem(key As String, userId As String) As Object

'    Sub SaveExtendedItem(key As String, userId As String, item As Object)

'End Interface