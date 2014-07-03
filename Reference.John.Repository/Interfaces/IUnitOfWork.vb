Imports System.Data

Namespace Infrastructure
    Public Interface IUnitOfWork
        Inherits IDisposable
        ReadOnly Property IsInTransaction() As Boolean

        Sub SaveChanges()

        Sub SaveChanges(saveOptions As System.Data.Entity.Core.Objects.SaveOptions)

        Sub BeginTransaction()

        Sub BeginTransaction(isolationLevel As IsolationLevel)

        Sub RollBackTransaction()

        Sub CommitTransaction()
    End Interface
End Namespace