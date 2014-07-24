Imports System.Data.Entity.Infrastructure.Interception

Namespace Container

    Public Class StamperInterceptor
        Implements IDbCommandInterceptor

        Public Sub NonQueryExecuted(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Integer)) Implements IDbCommandInterceptor.NonQueryExecuted

        End Sub

        Public Sub NonQueryExecuting(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Integer)) Implements IDbCommandInterceptor.NonQueryExecuting

        End Sub

        Public Sub ReaderExecuted(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Common.DbDataReader)) Implements IDbCommandInterceptor.ReaderExecuted
            Debug.WriteLine("adfasdf")
        End Sub

        Public Sub ReaderExecuting(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Common.DbDataReader)) Implements IDbCommandInterceptor.ReaderExecuting
            Debug.WriteLine("adfasdf")

            command.CommandText = "/* hardcoded valueadsflkj adsfl;akdsjf  */" & Environment.NewLine & command.CommandText
        End Sub

        Public Sub ScalarExecuted(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Object)) Implements IDbCommandInterceptor.ScalarExecuted

        End Sub

        Public Sub ScalarExecuting(command As Common.DbCommand, interceptionContext As DbCommandInterceptionContext(Of Object)) Implements IDbCommandInterceptor.ScalarExecuting

        End Sub

    End Class

End Namespace