Partial Class FileStore

    Public Sub New()
        ClientToken = Providers.SequentialGuid.NewGuid
    End Sub

End Class
