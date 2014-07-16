Imports System.Reflection

<TestClass()> Public Class TypeExtensionFixture

    <TestMethod()> Public Sub InitializeObjects()
        Assembly.GetAssembly(GetType(Reference.John.Infrastructure.TypeExtensions)).GetTypes().Where(Function(x) Not x.IsInterface AndAlso Not x.IsAbstract).ToList.ForEach(Sub(x)
                                                                                                                                                                                Console.WriteLine(x.FullName)
                                                                                                                                                                                Try
                                                                                                                                                                                    Dim obj As Object = Activator.CreateInstance(x)
                                                                                                                                                                                    Assert.IsNotNull(obj)
                                                                                                                                                                                Catch ex As MissingMethodException
                                                                                                                                                                                Catch ex As Reflection.TargetInvocationException
                                                                                                                                                                                End Try
                                                                                                                                                                            End Sub)
    End Sub

End Class
