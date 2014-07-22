Public Class OptionListSet

    Public Property GenderList As IEnumerable(Of OptionList)
    Public Property RaceList As IEnumerable(Of OptionList)
    Public Property RegionList As IEnumerable(Of OptionList)
    Public Property EthnicityList As IEnumerable(Of OptionList)

End Class

Public Class OptionList

    Public Property Id As Integer
    Public Property Name As String

End Class