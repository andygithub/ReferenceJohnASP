Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports System.Linq.Expressions
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace Extensions

    Public Module HtmlHelperExtensions

        Private Const _Comma As String = ","

        'Private Const Nbsp As String = "&nbsp;"
        'Private Const SelectedAttribute As String = " selected='selected'"

        '<Extension()> _
        'Public Function NbspIfEmpty(helper As HtmlHelper, value As String) As MvcHtmlString
        '    Return New MvcHtmlString(If(String.IsNullOrEmpty(value), Nbsp, value))
        'End Function

        <Extension()>
        Public Function TrendIcon(Of TModel, TValue)(html As HtmlHelper(Of TModel), expression As Linq.Expressions.Expression(Of Func(Of TModel, TValue))) As MvcHtmlString

            Throw New NotImplementedException
            'expression.
            'If expression Is Nothing Then Throw (New ArgumentNullException("expression"))
            'Dim result = Nothing
            'Try
            '    Dim deleg As Func(Of TModel, TValue) = expression.Compile()
            '    result = deleg(html.ViewData.Model)
            'Catch
            'End Try
            'Dim _item As Integer
            'Dim _trendRepository As ITrendRepository = New Repository.TrendRepository
            'If Integer.TryParse(result, _item) Then
            '    Return New MvcHtmlString(_trendRepository.GetTrendIcon(_item))
            'End If
            'Return New MvcHtmlString(_trendRepository.GetTrendIcon(-1))

        End Function

        '<Extension()> _
        'Public Function SelectedIfMatch(helper As HtmlHelper, expected As Object, actual As Object) As MvcHtmlString
        '    Return New MvcHtmlString(If(Equals(expected, actual), SelectedAttribute, String.Empty))
        'End Function

        <Extension()>
        Public Function ToDelimitedText(Of T)(objects As IEnumerable(Of T)) As String
            ' Get list of object properties
            Dim _out As New Text.StringBuilder
            Dim properties = GetType(T).GetProperties().Where(Function(x) x.PropertyType Is GetType(String) OrElse x.PropertyType Is GetType(DateTime) OrElse x.PropertyType Is GetType(Integer) _
                                                                  OrElse x.PropertyType Is GetType(Guid) OrElse x.PropertyType Is GetType(Nullable(Of Integer)))
            properties.ToList.ForEach(Sub(x)
                                          Dim attr = x.GetAttribute(Of DisplayAttribute)(False)
                                          If attr Is Nothing Then
                                              _out.Append(x.Name.Replace("_", " ") & _Comma)
                                          Else
                                              _out.Append(attr.GetName & _Comma)
                                          End If
                                      End Sub)

            _out.Remove(_out.Length - 1, 1)
            For i As Integer = 0 To objects.Count() - 1
                _out.AppendLine()
                For j As Integer = 0 To properties.Count() - 1
                    Dim test = properties(j).GetType
                    Dim _val = properties(j).GetValue(objects.ElementAt(i), Nothing)
                    If _val.GetType = GetType(Guid) Then
                        _out.Append(_val.ToString & _Comma)
                    Else
                        If String.IsNullOrEmpty(_val) Then _val = String.Empty
                        _out.Append(_val & _Comma)
                    End If

                    '_out.Append(CType(properties(j).GetValue(objects.ElementAt(i), Nothing), String) & _Comma)

                Next
                _out.Remove(_out.Length - 1, 1)
            Next

            Return _out.ToString
        End Function

        <Extension()>
        Public Function GetAttribute(Of T As Attribute)(member As MemberInfo, isRequired As Boolean) As T
            Dim attribute = member.GetCustomAttributes(GetType(T), False).SingleOrDefault()

            If attribute Is Nothing AndAlso isRequired Then
                Throw New ArgumentException(String.Format(Globalization.CultureInfo.InvariantCulture, "The {0} attribute must be defined on member {1}", GetType(T).Name, member.Name))
            End If

            Return DirectCast(attribute, T)
        End Function

        <Extension()>
        Public Function GetDisplayName(Of TModel, TProperty)(model As TModel, expression As Expression(Of Func(Of TModel, TProperty))) As String
            Return ModelMetadata.FromLambdaExpression(Of TModel, TProperty)(expression, New ViewDataDictionary(Of TModel)(model)).DisplayName
        End Function


        '<Extension()> _
        'Public Function GetPropertyDisplayName(Of T)(propertyExpression As Expression(Of Func(Of T, Object))) As String
        '    Dim memberInfo = GetPropertyInformation(propertyExpression.Body)
        '    If memberInfo Is Nothing Then
        '        Throw New ArgumentException("No property reference expression was found.", "propertyExpression")
        '    End If

        '    Dim attr = memberInfo.GetAttribute(Of DisplayNameAttribute)(False)
        '    If attr Is Nothing Then
        '        Return memberInfo.Name
        '    End If

        '    Return attr.DisplayName
        'End Function

        '<Extension()> _
        'Public Function GetPropertyInformation(propertyExpression As Expression) As MemberInfo
        '    Debug.Assert(propertyExpression IsNot Nothing, "propertyExpression != null")
        '    Dim memberExpr As MemberExpression = TryCast(propertyExpression, MemberExpression)
        '    If memberExpr Is Nothing Then
        '        Dim unaryExpr As UnaryExpression = TryCast(propertyExpression, UnaryExpression)
        '        If unaryExpr IsNot Nothing AndAlso unaryExpr.NodeType = ExpressionType.Convert Then
        '            memberExpr = TryCast(unaryExpr.Operand, MemberExpression)
        '        End If
        '    End If

        '    If memberExpr IsNot Nothing AndAlso memberExpr.Member.MemberType = MemberTypes.[Property] Then
        '        Return memberExpr.Member
        '    End If

        '    Return Nothing
        'End Function

    End Module


End Namespace