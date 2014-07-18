Public Module MappingConfig

    Public Sub RegisterDomainMapping()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.SearchResult, Reference.John.UI.Model.SearchResult)()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.FormSimpleZero, Reference.John.UI.Model.ViewFormSimpleZero)()
    End Sub

End Module

