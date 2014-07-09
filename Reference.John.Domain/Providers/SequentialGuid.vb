Namespace Providers

    Public Class SequentialGuid

        Public Property SequenceStartDate() As DateTime
            Get
                Return _SequenceStartDate
            End Get
            Private Set(value As DateTime)
                _SequenceStartDate = value
            End Set
        End Property
        Private _SequenceStartDate As DateTime
        Public Property SequenceEndDate() As DateTime
            Get
                Return _SequenceEndDate
            End Get
            Private Set(value As DateTime)
                _SequenceEndDate = value
            End Set
        End Property

        Private _SequenceEndDate As DateTime

        Private Const NumberOfBytes As Integer = 6
        Private Const PermutationsOfAByte As Integer = 256
        Private ReadOnly _maximumPermutations As Long = CLng(Math.Pow(PermutationsOfAByte, NumberOfBytes))
        Private _lastSequence As Long

        Public Sub New(sequenceStart As DateTime, sequenceEnd As DateTime)
            SequenceStartDate = sequenceStart
            SequenceEndDate = sequenceEnd
        End Sub

        Public Sub New()
            Me.New(New DateTime(2011, 10, 15), New DateTime(2100, 1, 1))
        End Sub

        Private Shared ReadOnly InstanceField As New Lazy(Of SequentialGuid)(Function() New SequentialGuid())
        Friend Shared ReadOnly Property Instance() As SequentialGuid
            Get
                Return InstanceField.Value
            End Get
        End Property

        Public Shared Function NewGuid() As Guid
            Return Instance.GetGuid()
        End Function

        Public ReadOnly Property TimePerSequence() As TimeSpan
            Get
                Dim ticksPerSequence = TotalPeriod.Ticks / _maximumPermutations
                Dim result = New TimeSpan(ticksPerSequence)
                Return result
            End Get
        End Property

        Public ReadOnly Property TotalPeriod() As TimeSpan
            Get
                Dim result = SequenceEndDate - SequenceStartDate
                Return result
            End Get
        End Property

        Private Function GetCurrentSequence(value As DateTime) As Long
            Dim ticksUntilNow = value.Ticks - SequenceStartDate.Ticks
            Dim result = (CDec(ticksUntilNow) / TotalPeriod.Ticks * _maximumPermutations - 1)
            Return CLng(result)
        End Function

        Public Function GetGuid() As Guid
            Return GetGuid(DateTime.Now)
        End Function

        Private ReadOnly _synchronizationObject As New Object()
        Friend Function GetGuid(now As DateTime) As Guid
            If now < SequenceStartDate OrElse now > SequenceEndDate Then
                ' Outside the range, use regular Guid
                Return Guid.NewGuid()
            End If

            Dim sequence = GetCurrentSequence(now)
            Return GetGuid(sequence)
        End Function

        Friend Function GetGuid(sequence As Long) As Guid
            SyncLock _synchronizationObject
                If sequence <= _lastSequence Then
                    ' Prevent double sequence on same server
                    sequence = _lastSequence + 1
                End If
                _lastSequence = sequence
            End SyncLock

            Dim sequenceBytes = GetSequenceBytes(sequence)
            Dim guidBytes = GetGuidBytes()
            Dim totalBytes = guidBytes.Concat(sequenceBytes).ToArray()
            Dim result = New Guid(totalBytes)
            Return result
        End Function

        Private Function GetSequenceBytes(sequence As Long) As IEnumerable(Of Byte)
            Dim sequenceBytes = BitConverter.GetBytes(sequence)
            Dim sequenceBytesLongEnough = sequenceBytes.Concat(New Byte(NumberOfBytes - 1) {})
            Dim result = sequenceBytesLongEnough.Take(NumberOfBytes).Reverse()
            Return result
        End Function

        Private Function GetGuidBytes() As IEnumerable(Of Byte)
            Dim result = Guid.NewGuid().ToByteArray().Take(10).ToArray()
            Return result
        End Function
    End Class

End Namespace