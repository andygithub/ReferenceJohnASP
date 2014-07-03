Namespace Cache
    ''' <summary>
    ''' Class that will contain the informaton about the currently executing method command. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CacheCommand

        ''' <summary>
        ''' Constructor that will take the IMethodInvocation and map it into properties.
        ''' </summary>
        ''' <param name="input"></param>
        ''' <remarks></remarks>
        Sub New(input As Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation)
            FullMethodName = input.MethodBase.DeclaringType.FullName & "." & input.MethodBase.Name
            Parameters = input.Arguments
        End Sub

        ''' <summary>
        ''' Full type method name of the executing name.  This value is used when looking up a configured command definition.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FullMethodName As String

        ''' <summary>
        ''' Set of parameters that have been passed to the command.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Parameters As Microsoft.Practices.Unity.InterceptionExtension.IParameterCollection

        ''' <summary>
        ''' Takes the existing property values and builds a cache key.  THe parameter values will be included as well.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCacheKey() As String
            Dim methodParameters As String = FullMethodName
            'attach paramater names and values as json blob
            If Parameters IsNot Nothing AndAlso Parameters.Count > 0 Then
                Dim _param As String = String.Empty
                For i As Integer = 0 To Parameters.Count - 1
                    _param = Parameters.ParameterName(i) & Newtonsoft.Json.JsonConvert.SerializeObject(Parameters.Item(i), Newtonsoft.Json.Formatting.None)
                Next
                methodParameters = methodParameters & _param
            End If
            'Return methodParameters
            Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(methodParameters)
            Return Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(bytes))
        End Function

    End Class

End Namespace