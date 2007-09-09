Public Class frmDwnldQuest
    Inherits System.Windows.Forms.Form
    Friend WithEvents btnOverWrite As System.Windows.Forms.Button
    Friend WithEvents btnResume As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Private bpen As New Pen(Color.Black)

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnOverWrite = New System.Windows.Forms.Button
        Me.btnResume = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblInfo = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        '
        'btnOverWrite
        '
        Me.btnOverWrite.Location = New System.Drawing.Point(8, 99)
        Me.btnOverWrite.Size = New System.Drawing.Size(65, 20)
        Me.btnOverWrite.Text = "Overwrite"
        '
        'btnResume
        '
        Me.btnResume.Location = New System.Drawing.Point(88, 99)
        Me.btnResume.Size = New System.Drawing.Size(65, 20)
        Me.btnResume.Text = "Resume"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(168, 99)
        Me.btnCancel.Size = New System.Drawing.Size(65, 20)
        Me.btnCancel.Text = "Cancel"
        '
        'lblInfo
        '
        Me.lblInfo.Location = New System.Drawing.Point(8, 75)
        Me.lblInfo.Size = New System.Drawing.Size(224, 16)
        Me.lblInfo.Text = "File already exists. Overwrite ?"
        '
        'lblFilePath
        '
        Me.lblFilePath.Location = New System.Drawing.Point(8, 24)
        Me.lblFilePath.Size = New System.Drawing.Size(224, 48)
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Black
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(240, 20)
        Me.TextBox1.Text = "Overwrite or Resume ?"
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        '
        'frmDwnldQuest
        '
        Me.ClientSize = New System.Drawing.Size(240, 128)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.lblFilePath)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnResume)
        Me.Controls.Add(Me.btnOverWrite)
        Me.Location = New System.Drawing.Point(0, 100)
        Me.Text = "frmDwnldQuest"

    End Sub

#End Region
    Public Enum DialogAnswer
        OverWrite
        [Resume]
        Cancel
    End Enum
    Public Answer As DialogAnswer
    Private Sub btnOverWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOverWrite.Click
        Answer = DialogAnswer.OverWrite
        Me.Close()
    End Sub

    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        Answer = DialogAnswer.Resume
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Answer = DialogAnswer.Cancel
        Me.Close()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawRectangle(bpen, 0, 0, Me.Width - 1, Me.Height - 1)
        MyBase.OnPaint(e)
    End Sub

    Private Sub frmDwnldQuest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
