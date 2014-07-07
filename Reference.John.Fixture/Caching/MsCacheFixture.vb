Imports System.Runtime.caching

<TestClass()> Public Class MsCacheFixture

    <TestMethod()> Public Sub SimpleAddRemoveFromFrameworkCache()
        Dim cache As ObjectCache = MemoryCache.[Default]
        Const key As String = "key"
        Assert.IsNull(cache.Get(key))
        Dim _item As New CacheItem(key, "adfdsfas")
        Dim _policy As New CacheItemPolicy
        _policy.SlidingExpiration = New TimeSpan(0, 0, 5)
        cache.Add(_item, _policy)
        Assert.IsNotNull(cache.Get(key))
        Threading.Thread.Sleep(6000)
        Assert.IsNull(cache.Get(key))
    End Sub

    <TestMethod()> Public Sub CacheMissDueToSlidingExpiration()
        Dim cache As ObjectCache = MemoryCache.[Default]
        Const key As String = "key"
        Assert.IsNull(cache.Get(key))
        Dim _item As New CacheItem(key, "adfdsfas")
        Dim _policy As New CacheItemPolicy
        _policy.SlidingExpiration = New TimeSpan(0, 0, 5)
        cache.Add(_item, _policy)
        Assert.IsNotNull(cache.Get(key))
        Threading.Thread.Sleep(3000)
        Assert.IsNotNull(cache.Get(key))
        Threading.Thread.Sleep(3000)
        Assert.IsNotNull(cache.Get(key))
        Threading.Thread.Sleep(6000)
        Assert.IsNull(cache.Get(key))
    End Sub

End Class
