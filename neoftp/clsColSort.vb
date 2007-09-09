Public Class ColHeader
    Inherits ColumnHeader
    Public ascending As Boolean

    Public Sub New(ByVal [text] As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal asc As Boolean)
        Me.Text = [text]
        Me.Width = width
        Me.TextAlign = align
        Me.ascending = asc
    End Sub
End Class

