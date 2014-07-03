Imports System.Reflection

<TestClass()> Public Class InitializeObjects

    <TestMethod()> Public Sub InitializeObjects()
        Assembly.GetAssembly(GetType(Reference.John.Domain.Address)).GetTypes().ToList.ForEach(Sub(x)
                                                                                                   Dim obj As Object = Activator.CreateInstance(x)
                                                                                                   Console.WriteLine(x.FullName)
                                                                                                   Assert.IsNotNull(obj)
                                                                                               End Sub)
    End Sub

End Class
