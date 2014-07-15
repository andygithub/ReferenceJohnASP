Public Module MappingConfig

    Public Sub RegisterDomainMapping()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.SearchResult, Models.SearchResult)()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.FormSimpleZero, Models.ViewFormSimpleZero)()
    End Sub

End Module

