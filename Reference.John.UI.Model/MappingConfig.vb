Public Module MappingConfig

    Public Sub RegisterDomainMapping()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.SearchResult, Reference.John.UI.Model.SearchResult)()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.FormSimpleZero, Reference.John.UI.Model.ViewFormSimpleZero)()
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.GenderOptionList, Reference.John.UI.Model.OptionList)().ForMember(Function(dest) dest.Id, Sub(opt) opt.MapFrom(Function(src) src.GenderId))
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.RaceOptionList, Reference.John.UI.Model.OptionList)().ForMember(Function(dest) dest.Id, Sub(opt) opt.MapFrom(Function(src) src.RaceId))
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.RegionOptionList, Reference.John.UI.Model.OptionList)().ForMember(Function(dest) dest.Id, Sub(opt) opt.MapFrom(Function(src) src.RegionId))
        AutoMapper.Mapper.CreateMap(Of Reference.John.Domain.EthnicityOptionList, Reference.John.UI.Model.OptionList)().ForMember(Function(dest) dest.Id, Sub(opt) opt.MapFrom(Function(src) src.EthnicityId))

        'AutoMapper.Mapper.AssertConfigurationIsValid()
    End Sub

End Module

