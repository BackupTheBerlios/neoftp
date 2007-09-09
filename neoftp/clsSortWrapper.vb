' An instance of the SortWrapper class is created for 
' each item and added to the ArrayList for sorting.
Public Class SortWrapper
    Friend sortItem As ListViewItem
    Friend sortColumn As Integer



    ' A SortWrapper requires the item and the index of the clicked column.
    Public Sub New(ByVal Item As ListViewItem, ByVal iColumn As Integer)
        sortItem = Item
        sortColumn = iColumn
    End Sub

    ' Text property for getting the text of an item.
    Public ReadOnly Property [Text]() As String
        Get
            Return sortItem.SubItems(sortColumn).Text
        End Get
    End Property

    ' Implementation of the IComparer 
    ' interface for sorting ArrayList items.
    Public Class SortComparer
        Implements IComparer
        Private ascending As Boolean


        ' Constructor requires the sort order;
        ' true if ascending, otherwise descending.
        Public Sub New(ByVal asc As Boolean)
            Me.ascending = asc
        End Sub


        ' Implemnentation of the IComparer:Compare 
        ' method for comparing two objects.
        Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim xItem As SortWrapper = CType(x, SortWrapper)
            Dim yItem As SortWrapper = CType(y, SortWrapper)


            Dim xText As String = xItem.sortItem.SubItems(xItem.sortColumn).Text
            Dim yText As String = yItem.sortItem.SubItems(yItem.sortColumn).Text

            If xItem.sortColumn = 1 Then
                Dim XVal As Double
                Dim YVal As Double
                Try
                    YVal = Val(yText)
                Catch ex As Exception
                End Try
                Try
                    XVal = Val(xText)
                Catch ex As Exception
                End Try
                Return XVal.CompareTo(YVal) * IIf(Me.ascending, 1, -1)
            Else
                Return xText.CompareTo(yText) * IIf(Me.ascending, 1, -1)
            End If

        End Function
    End Class
End Class