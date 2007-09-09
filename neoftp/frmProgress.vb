Imports p7.controls

Public Class frmProgress
    Inherits System.Windows.Forms.Form
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Private bpen As New Pen(Color.Black)
    Public ProgressBar1 As New p7_ProgBar

    Private Declare Function QueryPerformanceFrequency Lib "Coredll" Alias "QueryPerformanceFrequency" (ByRef lpFrequency As Int64) As Integer
    Private Declare Function QueryPerformanceCounter Lib "Coredll" Alias "QueryPerformanceCounter" (ByRef lpPerformanceCount As Int64) As Integer

    Public Enum WorkingMode
        Download
        UpLoad
    End Enum
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.ProgressBar1.Parent = Me
        Me.ProgressBar1.Bounds = New Rectangle(8, 100, 224, 24)
        Me.ProgressBar1.ValueStyle = p7_ProgBar.ValueStyles.Percent
        Me.ProgressBar1.ForeColor = Color.LightGray
        Me.ProgressBar1.GradientColor = Color.Black
        Me.ProgressBar1.FontColor = Color.DarkBlue
        Me.ProgressBar1.BackColor = Color.White
        Me.ProgressBar1.useGradient = True
        Me.ProgressBar1.DrawStyle = p7_ProgBar.DrawingStyles.Rectangle
        Me.ProgressBar1.Orientation = p7_ProgBar.OrientationValues.horizontal_L2R
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblSpeed As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents lblTO As System.Windows.Forms.Label
    Friend WithEvents lblProgInBytes As System.Windows.Forms.Label
    Friend WithEvents lblEstTime As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblFileName = New System.Windows.Forms.Label
        Me.lblSpeed = New System.Windows.Forms.Label
        Me.lblTO = New System.Windows.Forms.Label
        Me.lblProgInBytes = New System.Windows.Forms.Label
        Me.lblEstTime = New System.Windows.Forms.Label
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(152, 184)
        Me.btnCancel.Size = New System.Drawing.Size(80, 24)
        Me.btnCancel.Text = "Cancel"
        '
        'lblFileName
        '
        Me.lblFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblFileName.Location = New System.Drawing.Point(8, 0)
        Me.lblFileName.Size = New System.Drawing.Size(224, 48)
        Me.lblFileName.Text = "Label1"
        '
        'lblSpeed
        '
        Me.lblSpeed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.lblSpeed.Location = New System.Drawing.Point(120, 152)
        Me.lblSpeed.Size = New System.Drawing.Size(112, 24)
        Me.lblSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTO
        '
        Me.lblTO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTO.Location = New System.Drawing.Point(8, 56)
        Me.lblTO.Size = New System.Drawing.Size(224, 40)
        Me.lblTO.Text = "Label1"
        '
        'lblProgInBytes
        '
        Me.lblProgInBytes.Location = New System.Drawing.Point(8, 136)
        Me.lblProgInBytes.Size = New System.Drawing.Size(104, 72)
        '
        'lblEstTime
        '
        Me.lblEstTime.Location = New System.Drawing.Point(120, 136)
        Me.lblEstTime.Size = New System.Drawing.Size(112, 16)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        '
        'frmProgress
        '
        Me.ClientSize = New System.Drawing.Size(240, 215)
        Me.Controls.Add(Me.lblEstTime)
        Me.Controls.Add(Me.lblProgInBytes)
        Me.Controls.Add(Me.lblTO)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.btnCancel)
        Me.Location = New System.Drawing.Point(0, 100)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = "Progress"

    End Sub

#End Region

    Friend Event OpCanceled()

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawRectangle(bpen, 0, 0, Me.Width - 1, Me.Height - 1)
        MyBase.OnPaint(e)
    End Sub

    Friend Sub getNextBytes(ByVal cnt As Long)

        Dim currentSpeed As Single

        Dim T As New TimeSpan
        Dim T2 As New TimeSpan


        Dim time_ms As System.Int64
        Dim nowCounter As System.Int64 = 0
        Static Dim lastCounter As System.Int64

        Static lastTime As DateTime

        Dim remainingBytes As Long
        Dim remainingSeconds As Long

        Me.ProgressBar1.Value += cnt
        Me.lblProgInBytes.Text = formatBytes(Me.ProgressBar1.Value) & " / " & formatBytes(Me.ProgressBar1.MaxValue)

        Try

            Dim freq As System.Int64 = 0

            If QueryPerformanceFrequency(freq) <> 0 Then

                QueryPerformanceCounter(nowCounter)
                time_ms = (nowCounter - lastCounter) * 1000 / freq
                lastCounter = nowCounter

                'TODO: check if this time is correct
                T = TimeSpan.FromMilliseconds(time_ms)

                'T2 = Date.Now.Subtract(lastTime)
                'lastTime = Date.Now

                'Debug.WriteLine(T2.Milliseconds + " " + T.Milliseconds)

            Else

                T = Date.Now.Subtract(lastTime)
                lastTime = Date.Now

            End If

            If T.TotalMilliseconds > 0 Then

                Debug.WriteLine(T.TotalMilliseconds.ToString())

                currentSpeed = cnt / T.TotalMilliseconds * 1000.0 ' in Bytes p Sec

                remainingBytes = (Me.ProgressBar1.MaxValue - Me.ProgressBar1.Value)
                remainingSeconds = remainingBytes / currentSpeed 'in Seconds

                Me.lblEstTime.Text = TimeSpan.FromSeconds(remainingSeconds).ToString()
                Me.lblSpeed.Text = formatBytes(currentSpeed) + "/s"

            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try
    End Sub

    Private Function formatBytes(ByVal value As Long) As String

        Const GB As String = "GB"
        Const MB As String = "MB"
        Const KB As String = "KB"
        Const BYTES As String = "B"

        Const KB_FACTOR As Integer = 1024
        Const MB_FACTOR As Integer = KB_FACTOR * 1024
        Const GB_FACTOR As Integer = MB_FACTOR * 1024

        Dim factor As Integer = 1
        Dim unit As String = BYTES

        Try
            Select Case value
                Case Is > GB_FACTOR
                    factor = GB_FACTOR
                    unit = GB
                Case Is > MB_FACTOR
                    factor = MB_FACTOR
                    unit = MB
                Case Is > KB_FACTOR
                    factor = KB_FACTOR
                    unit = KB
            End Select

            formatBytes = (value \ factor).ToString + unit

        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try

    End Function

    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        Dim antw As DialogResult
        antw = MsgBox("Cancel Operation ?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
        If antw = DialogResult.No Then

        Else
            RaiseEvent OpCanceled()
            Me.Hide()
        End If
        e.Cancel = True
    End Sub

    Private Sub frmProgress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
