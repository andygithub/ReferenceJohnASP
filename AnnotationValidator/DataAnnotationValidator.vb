
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DataAnnotationValidator
    Public Class DataAnnotationValidator
        Inherits BaseValidator

        Private Shared template As String = "<span class=""field-validation-error"" id=""{0}""  data-val-evaluationfunction=""{1}"" data-val=""true"" data-val-errormessage=""{2}"" data-val-controltovalidate=""{3}"" {4} {5}>{6}</span>"
        Private spanString As String = String.Empty

        Public Property TypeName() As String
        Public Property TypeProperty() As String
        Public Property TypeAssembly() As String


        Protected Overrides Sub OnPreRender(e As EventArgs)
            MyBase.OnPreRender(e)

            Dim c As Control = Me.Parent.FindControl(Me.ControlToValidate)
            Dim type__1 As Type = Type.[GetType](Convert.ToString(Me.TypeName & Convert.ToString(",")) & TypeAssembly, True)
            Dim [property] As PropertyInfo = type__1.GetProperty(Me.TypeProperty)
            'set up appropriate defaults to be overwritten below
            Dim message As String = String.Empty
            Dim display As String = "style=""display: none;"" data-val-display=""None"""

            'loop through each validation attribute, read it and create the appropriate span
            For Each vat As ValidationAttribute In [property].GetCustomAttributes(GetType(ValidationAttribute), True).OfType(Of ValidationAttribute)()
                'in case the resource properties of the attribute are set check for that and pull it into the errormessage property so the code will be consistent.
                If Not vat.ErrorMessageResourceType Is Nothing Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Reference.John.Resources." & vat.ErrorMessageResourceType.Name, vat.ErrorMessageResourceType.Assembly)
                    vat.ErrorMessage = temp.GetString(vat.ErrorMessageResourceName)
                End If

                If vat.ErrorMessage IsNot Nothing AndAlso vat.ErrorMessage.Contains("{0}") AndAlso vat.GetType Is GetType(RequiredAttribute) Then
                    'grab the property name resource value.  hard cording the resource name here rather than loop through the attributes to find the display attribute and property.
                    Dim tempmanager As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Reference.John.Resources.Names", vat.ErrorMessageResourceType.Assembly)
                    Dim displayName As String = tempmanager.GetString([property].Name)
                    vat.ErrorMessage = String.Format(vat.ErrorMessage, displayName)
                End If
                If vat.ErrorMessage IsNot Nothing AndAlso vat.ErrorMessage.Contains("{1}") AndAlso vat.GetType Is GetType(StringLengthAttribute) Then
                    'grab the property name resource value.  hard cording the resource name here rather than loop through the attributes to find the display attribute and property.
                    Dim tempmanager As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Reference.John.Resources.Names", vat.ErrorMessageResourceType.Assembly)
                    Dim displayName As String = tempmanager.GetString([property].Name)
                    vat.ErrorMessage = String.Format(vat.ErrorMessage, displayName, DirectCast(vat, StringLengthAttribute).MaximumLength)
                End If

                If Me.Display <> ValidatorDisplay.None Then
                    message = If((Me.Text <> String.Empty), Me.Text, vat.ErrorMessage)
                    display = If((Me.Display = ValidatorDisplay.Dynamic), "style=""display: none;"" data-val-display=""Dynamic""", "style=""visibility: hidden;""")
                End If
                Select Case vat.[GetType]().Name
                    Case "RequiredAttribute"
                        spanString += String.Format(DataAnnotationValidator.template, Me.ClientID, "RequiredFieldValidatorEvaluateIsValid", vat.ErrorMessage, c.ClientID, "data-val-initialvalue=""""", display, message)
                        Exit Select
                    Case "EmailAddressAttribute"
                        BuildRegularExpression(Me.ClientID, vat.ErrorMessage, c.ClientID, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", display, message)
                        Exit Select
                    Case "UrlAttribute"
                        BuildRegularExpression(Me.ClientID, vat.ErrorMessage, c.ClientID, "http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", display, message)
                        Exit Select
                    Case "StringLengthAttribute"
                        Dim a As StringLengthAttribute = DirectCast(vat, StringLengthAttribute)
                        If String.IsNullOrEmpty(vat.ErrorMessage) Then
                            vat.ErrorMessage = "must not exceed " + a.MaximumLength + " characters"
                            If a.MinimumLength > 0 Then
                                vat.ErrorMessage += " or have fewer than " + a.MinimumLength + " characters"
                            End If
                        End If
                        Dim reg As String = "^.{" + String.Format("{0},{1}", a.MinimumLength, a.MaximumLength) + "}$"
                        BuildRegularExpression(Me.ClientID, vat.ErrorMessage, c.ClientID, reg, display, message)
                        Exit Select
                    Case "MaxLengthAttribute"
                        Dim m As MaxLengthAttribute = DirectCast(vat, MaxLengthAttribute)
                        If String.IsNullOrEmpty(vat.ErrorMessage) Then
                            vat.ErrorMessage = "must not exceed " + m.Length + " characters"
                        End If
                        Dim r As String = "^.{" + String.Format(",{0}", m.Length) + "}$"
                        BuildRegularExpression(Me.ClientID, vat.ErrorMessage, c.ClientID, r, display, message)
                        Exit Select
                    Case "RegularExpressionAttribute"
                        Dim rea As RegularExpressionAttribute = DirectCast(vat, RegularExpressionAttribute)
                        BuildRegularExpression(Me.ClientID, vat.ErrorMessage, c.ClientID, rea.Pattern, display, message)
                        Exit Select
                End Select
            Next

        End Sub

        Private Sub BuildRegularExpression(id As String, errorMessage As String, clientId As String, expression As String, display As String, message As String)
            spanString += String.Format(DataAnnotationValidator.template, id, "RegularExpressionValidatorEvaluateIsValid", errorMessage, clientId, (Convert.ToString("data-val-validationexpression=""") & expression) + """", display, message)
        End Sub

        Protected Overrides Sub Render(writer As HtmlTextWriter)
            writer.Write(spanString)
        End Sub

        Protected Overrides Function EvaluateIsValid() As Boolean
            Return True
        End Function
    End Class
End Namespace

