Public Module TypeExtensions

    <System.Runtime.CompilerServices.Extension> _
    Public Function IsConcrete(type As Type) As Boolean
        Return Not type.IsAbstract AndAlso Not type.IsInterface
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function CanBeCreated(type As Type) As Boolean
        Return type.IsConcrete() AndAlso type.GetConstructors().Length > 0
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function CanBeCastTo(typeFrom As Type, typeTo As Type) As Boolean
        If typeFrom Is Nothing Then
            Return False
        End If

        If typeFrom.IsInterface OrElse typeFrom.IsAbstract Then
            Return False
        End If

        If IsOpenGeneric(typeFrom) Then
            Return False
        End If

        Return typeTo.IsAssignableFrom(typeFrom)
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function IsOpenGeneric(type As Type) As Boolean
        Return type.IsGenericTypeDefinition OrElse type.ContainsGenericParameters
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function IsInNamespace(type As Type, [namespace] As String) As Boolean
        If type.[Namespace] IsNot Nothing Then
            Return type.[Namespace].StartsWith([namespace])
        End If

        Return False
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function ImplementsInterface(type As Type, [implements] As Type) As Boolean
        Return [implements].IsInterface AndAlso type.GetInterface([implements].Name) IsNot Nothing
    End Function

    <System.Runtime.CompilerServices.Extension> _
    Public Function ImplementsInterfaceTemplate(type As Type, templateType As Type) As Boolean
        If Not type.IsConcrete() Then
            Return False
        End If

        Return type.GetInterfaces().Any(Function(interfaceType) interfaceType.IsGenericType AndAlso interfaceType.GetGenericTypeDefinition() = templateType)
    End Function

End Module