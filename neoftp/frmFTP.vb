
Public Class frmFTP
    Inherits System.Windows.Forms.Form
    Private WithEvents TabControl1 As System.Windows.Forms.TabControl
    Private WithEvents tabLog As System.Windows.Forms.TabPage
    Private WithEvents txtLog As System.Windows.Forms.TextBox
    Private WithEvents cnMenLog As System.Windows.Forms.ContextMenu
    Private FName As String = theApp.AppPath & "\FTP_Hosts.lst"
    Friend WithEvents FTP As New p7.networking.p7FTP
    Private WithEvents frmProgress As New frmProgress
    Private LocalPath As String = theApp.AppPath

    Private Const OPSTRDOWN As String = "DOWNLOAD"
    Private Const OPSTRUPL As String = "UPLOAD"
    Private Const STDNEWDIRNAME = "NeoDir"
    Private myLogin As p7.Networking.p7FTP.LoginStruct

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal LOG As p7.Networking.p7FTP.LoginStruct)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.lstRemoteFilez.Columns.Add(New ColHeader("Name", 120, HorizontalAlignment.Left, True))
        Me.lstRemoteFilez.Columns.Add(New ColHeader("Size / B", 60, HorizontalAlignment.Left, True))
        Me.lstRemoteFilez.Columns.Add(New ColHeader("Attribs", 55, HorizontalAlignment.Left, True))

        Me.lstLocFilez.Columns.Add(New ColHeader("Name", 120, HorizontalAlignment.Left, True))
        Me.lstLocFilez.Columns.Add(New ColHeader("Size / B", 60, HorizontalAlignment.Left, True))
        Me.lstLocFilez.Columns.Add(New ColHeader("Date", 55, HorizontalAlignment.Left, True))
        Me.tabRemFiles.Enabled = False

        Me.myLogin = LOG
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents menClearLog As System.Windows.Forms.MenuItem
    Friend WithEvents menSaveLog As System.Windows.Forms.MenuItem
    Friend WithEvents lstRemoteFilez As System.Windows.Forms.ListView
    Friend WithEvents tabHosts As System.Windows.Forms.TabPage
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtLogin As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents udPort As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents InputPanel1 As Microsoft.WindowsCE.Forms.InputPanel
    Friend WithEvents txtCmd As System.Windows.Forms.TextBox
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents cmbDir As System.Windows.Forms.ComboBox
    Friend WithEvents cnMenRemFilez As System.Windows.Forms.ContextMenu
    Friend WithEvents menDownload As System.Windows.Forms.MenuItem
    Friend WithEvents menConnect As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menDatails As System.Windows.Forms.MenuItem
    Friend WithEvents cmbHosts As System.Windows.Forms.ComboBox
    Friend WithEvents btnDirUp As System.Windows.Forms.Button
    Friend WithEvents btnRoot As System.Windows.Forms.Button
    Friend WithEvents menMkDir As System.Windows.Forms.MenuItem
    Friend WithEvents menRmDir As System.Windows.Forms.MenuItem
    Friend WithEvents menClose As System.Windows.Forms.MenuItem
    Friend WithEvents menAbout As System.Windows.Forms.MenuItem
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents tabRemFiles As System.Windows.Forms.TabPage
    Friend WithEvents tabLocFilez As System.Windows.Forms.TabPage
    Friend WithEvents lstLocFilez As System.Windows.Forms.ListView
    Friend WithEvents treeLocalPath As System.Windows.Forms.TreeView
    Friend WithEvents cnMenLocalFilez As System.Windows.Forms.ContextMenu
    Friend WithEvents menUpload As System.Windows.Forms.MenuItem
    Friend WithEvents menSmallIcons As System.Windows.Forms.MenuItem
    Friend WithEvents menLocDel As System.Windows.Forms.MenuItem
    Friend WithEvents menOperation As System.Windows.Forms.MenuItem
    Friend WithEvents menRemoteRename As System.Windows.Forms.MenuItem
    Friend WithEvents menLocalFileRename As System.Windows.Forms.MenuItem
    Friend WithEvents chkAnonymous As System.Windows.Forms.CheckBox
    Friend WithEvents lblPass As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents cnMenLocDir As System.Windows.Forms.ContextMenu
    Friend WithEvents menLocDelDir As System.Windows.Forms.MenuItem
    Friend WithEvents menLocalExec As System.Windows.Forms.MenuItem
    Friend WithEvents menLocMkDir As System.Windows.Forms.MenuItem
    Friend WithEvents menLocDirRename As System.Windows.Forms.MenuItem
    Friend WithEvents lblremCurDir As System.Windows.Forms.Label
    Friend WithEvents menRemreloadDir As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmFTP))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabHosts = New System.Windows.Forms.TabPage
        Me.chkAnonymous = New System.Windows.Forms.CheckBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblInfo = New System.Windows.Forms.Label
        Me.lblPass = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPass = New System.Windows.Forms.TextBox
        Me.txtLogin = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.udPort = New System.Windows.Forms.NumericUpDown
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbHosts = New System.Windows.Forms.ComboBox
        Me.tabLocFilez = New System.Windows.Forms.TabPage
        Me.treeLocalPath = New System.Windows.Forms.TreeView
        Me.cnMenLocDir = New System.Windows.Forms.ContextMenu
        Me.menLocDelDir = New System.Windows.Forms.MenuItem
        Me.menLocMkDir = New System.Windows.Forms.MenuItem
        Me.menLocDirRename = New System.Windows.Forms.MenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList
        Me.lstLocFilez = New System.Windows.Forms.ListView
        Me.cnMenLocalFilez = New System.Windows.Forms.ContextMenu
        Me.menUpload = New System.Windows.Forms.MenuItem
        Me.menLocDel = New System.Windows.Forms.MenuItem
        Me.menLocalFileRename = New System.Windows.Forms.MenuItem
        Me.menLocalExec = New System.Windows.Forms.MenuItem
        Me.tabRemFiles = New System.Windows.Forms.TabPage
        Me.lblremCurDir = New System.Windows.Forms.Label
        Me.btnRoot = New System.Windows.Forms.Button
        Me.btnDirUp = New System.Windows.Forms.Button
        Me.cmbDir = New System.Windows.Forms.ComboBox
        Me.lstRemoteFilez = New System.Windows.Forms.ListView
        Me.cnMenRemFilez = New System.Windows.Forms.ContextMenu
        Me.menDownload = New System.Windows.Forms.MenuItem
        Me.menMkDir = New System.Windows.Forms.MenuItem
        Me.menRmDir = New System.Windows.Forms.MenuItem
        Me.menRemoteRename = New System.Windows.Forms.MenuItem
        Me.menRemreloadDir = New System.Windows.Forms.MenuItem
        Me.tabLog = New System.Windows.Forms.TabPage
        Me.btnSend = New System.Windows.Forms.Button
        Me.txtCmd = New System.Windows.Forms.TextBox
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.cnMenLog = New System.Windows.Forms.ContextMenu
        Me.menClearLog = New System.Windows.Forms.MenuItem
        Me.menSaveLog = New System.Windows.Forms.MenuItem
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.menClose = New System.Windows.Forms.MenuItem
        Me.menConnect = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.menSmallIcons = New System.Windows.Forms.MenuItem
        Me.menDatails = New System.Windows.Forms.MenuItem
        Me.menOperation = New System.Windows.Forms.MenuItem
        Me.menAbout = New System.Windows.Forms.MenuItem
        Me.InputPanel1 = New Microsoft.WindowsCE.Forms.InputPanel
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabHosts)
        Me.TabControl1.Controls.Add(Me.tabRemFiles)
        Me.TabControl1.Controls.Add(Me.tabLocFilez)
        Me.TabControl1.Controls.Add(Me.tabLog)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(240, 272)
        '
        'tabHosts
        '
        Me.tabHosts.Controls.Add(Me.chkAnonymous)
        Me.tabHosts.Controls.Add(Me.btnConnect)
        Me.tabHosts.Controls.Add(Me.btnSave)
        Me.tabHosts.Controls.Add(Me.lblInfo)
        Me.tabHosts.Controls.Add(Me.lblPass)
        Me.tabHosts.Controls.Add(Me.Label4)
        Me.tabHosts.Controls.Add(Me.txtPass)
        Me.tabHosts.Controls.Add(Me.txtLogin)
        Me.tabHosts.Controls.Add(Me.Label3)
        Me.tabHosts.Controls.Add(Me.Label2)
        Me.tabHosts.Controls.Add(Me.udPort)
        Me.tabHosts.Controls.Add(Me.txtServer)
        Me.tabHosts.Controls.Add(Me.Label1)
        Me.tabHosts.Controls.Add(Me.cmbHosts)
        Me.tabHosts.Location = New System.Drawing.Point(4, 4)
        Me.tabHosts.Size = New System.Drawing.Size(232, 247)
        Me.tabHosts.Text = "Hosts"
        '
        'chkAnonymous
        '
        Me.chkAnonymous.Checked = True
        Me.chkAnonymous.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAnonymous.Location = New System.Drawing.Point(136, 102)
        Me.chkAnonymous.Size = New System.Drawing.Size(88, 16)
        Me.chkAnonymous.Text = "Anonymous"
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(72, 216)
        Me.btnConnect.Size = New System.Drawing.Size(80, 24)
        Me.btnConnect.Text = "Connect"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(136, 136)
        Me.btnSave.Size = New System.Drawing.Size(88, 24)
        Me.btnSave.Text = "Save"
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.lblInfo.Location = New System.Drawing.Point(16, 176)
        Me.lblInfo.Size = New System.Drawing.Size(200, 32)
        Me.lblInfo.Text = "neoFTP ver 0.9.1 (c) 2004 - 2007 Milosz Weckowski"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPass
        '
        Me.lblPass.Location = New System.Drawing.Point(4, 123)
        Me.lblPass.Size = New System.Drawing.Size(128, 16)
        Me.lblPass.Text = "Password"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(4, 83)
        Me.Label4.Size = New System.Drawing.Size(128, 16)
        Me.Label4.Text = "Login"
        '
        'txtPass
        '
        Me.txtPass.Enabled = False
        Me.txtPass.Location = New System.Drawing.Point(4, 139)
        Me.txtPass.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPass.Size = New System.Drawing.Size(128, 18)
        Me.txtPass.Text = "neoFTP@playseven.com"
        '
        'txtLogin
        '
        Me.txtLogin.Enabled = False
        Me.txtLogin.Location = New System.Drawing.Point(4, 99)
        Me.txtLogin.Size = New System.Drawing.Size(128, 18)
        Me.txtLogin.Text = "anonymous"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(164, 43)
        Me.Label3.Size = New System.Drawing.Size(52, 16)
        Me.Label3.Text = "Port"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 43)
        Me.Label2.Size = New System.Drawing.Size(120, 16)
        Me.Label2.Text = "Server"
        '
        'udPort
        '
        Me.udPort.Location = New System.Drawing.Point(164, 59)
        Me.udPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.udPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udPort.Size = New System.Drawing.Size(64, 18)
        Me.udPort.Value = New Decimal(New Integer() {21, 0, 0, 0})
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(4, 59)
        Me.txtServer.Size = New System.Drawing.Size(152, 18)
        Me.txtServer.Text = "192.168.1.10"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 3)
        Me.Label1.Size = New System.Drawing.Size(184, 16)
        Me.Label1.Text = "Connection Name"
        '
        'cmbHosts
        '
        Me.cmbHosts.Location = New System.Drawing.Point(4, 19)
        Me.cmbHosts.Size = New System.Drawing.Size(224, 20)
        '
        'tabLocFilez
        '
        Me.tabLocFilez.Controls.Add(Me.treeLocalPath)
        Me.tabLocFilez.Controls.Add(Me.lstLocFilez)
        Me.tabLocFilez.Location = New System.Drawing.Point(4, 4)
        Me.tabLocFilez.Size = New System.Drawing.Size(232, 247)
        Me.tabLocFilez.Text = "Local"
        '
        'treeLocalPath
        '
        Me.treeLocalPath.ContextMenu = Me.cnMenLocDir
        Me.treeLocalPath.ImageIndex = 1
        Me.treeLocalPath.ImageList = Me.ImageList1
        Me.treeLocalPath.Size = New System.Drawing.Size(232, 112)
        '
        'cnMenLocDir
        '
        Me.cnMenLocDir.MenuItems.Add(Me.menLocDelDir)
        Me.cnMenLocDir.MenuItems.Add(Me.menLocMkDir)
        Me.cnMenLocDir.MenuItems.Add(Me.menLocDirRename)
        '
        'menLocDelDir
        '
        Me.menLocDelDir.Text = "Delete"
        '
        'menLocMkDir
        '
        Me.menLocMkDir.Text = "MakeDir"
        '
        'menLocDirRename
        '
        Me.menLocDirRename.Text = "Rename"
        '
        'ImageList1
        '
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource1"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource2"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource3"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource4"), System.Drawing.Image))
        Me.ImageList1.Images.Add(CType(resources.GetObject("resource5"), System.Drawing.Image))
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        '
        'lstLocFilez
        '
        Me.lstLocFilez.ContextMenu = Me.cnMenLocalFilez
        Me.lstLocFilez.FullRowSelect = True
        Me.lstLocFilez.Location = New System.Drawing.Point(0, 112)
        Me.lstLocFilez.Size = New System.Drawing.Size(232, 128)
        Me.lstLocFilez.SmallImageList = Me.ImageList1
        Me.lstLocFilez.View = System.Windows.Forms.View.Details
        '
        'cnMenLocalFilez
        '
        Me.cnMenLocalFilez.MenuItems.Add(Me.menUpload)
        Me.cnMenLocalFilez.MenuItems.Add(Me.menLocDel)
        Me.cnMenLocalFilez.MenuItems.Add(Me.menLocalFileRename)
        Me.cnMenLocalFilez.MenuItems.Add(Me.menLocalExec)
        '
        'menUpload
        '
        Me.menUpload.Text = "Upload"
        '
        'menLocDel
        '
        Me.menLocDel.Text = "Delete"
        '
        'menLocalFileRename
        '
        Me.menLocalFileRename.Text = "Rename"
        '
        'menLocalExec
        '
        Me.menLocalExec.Text = "Execute"
        '
        'tabRemFiles
        '
        Me.tabRemFiles.Controls.Add(Me.lblremCurDir)
        Me.tabRemFiles.Controls.Add(Me.btnRoot)
        Me.tabRemFiles.Controls.Add(Me.btnDirUp)
        Me.tabRemFiles.Controls.Add(Me.cmbDir)
        Me.tabRemFiles.Controls.Add(Me.lstRemoteFilez)
        Me.tabRemFiles.Location = New System.Drawing.Point(4, 4)
        Me.tabRemFiles.Size = New System.Drawing.Size(232, 247)
        Me.tabRemFiles.Text = "Remote"
        '
        'lblremCurDir
        '
        Me.lblremCurDir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.lblremCurDir.Location = New System.Drawing.Point(2, 2)
        Me.lblremCurDir.Size = New System.Drawing.Size(173, 16)
        '
        'btnRoot
        '
        Me.btnRoot.Location = New System.Drawing.Point(192, 0)
        Me.btnRoot.Size = New System.Drawing.Size(20, 20)
        Me.btnRoot.Text = "/"
        '
        'btnDirUp
        '
        Me.btnDirUp.Location = New System.Drawing.Point(212, 0)
        Me.btnDirUp.Size = New System.Drawing.Size(20, 20)
        Me.btnDirUp.Text = ".."
        '
        'cmbDir
        '
        Me.cmbDir.Location = New System.Drawing.Point(1, 0)
        Me.cmbDir.Size = New System.Drawing.Size(192, 20)
        '
        'lstRemoteFilez
        '
        Me.lstRemoteFilez.ContextMenu = Me.cnMenRemFilez
        Me.lstRemoteFilez.FullRowSelect = True
        Me.lstRemoteFilez.Location = New System.Drawing.Point(0, 24)
        Me.lstRemoteFilez.Size = New System.Drawing.Size(232, 224)
        Me.lstRemoteFilez.SmallImageList = Me.ImageList1
        Me.lstRemoteFilez.View = System.Windows.Forms.View.Details
        '
        'cnMenRemFilez
        '
        Me.cnMenRemFilez.MenuItems.Add(Me.menDownload)
        Me.cnMenRemFilez.MenuItems.Add(Me.menMkDir)
        Me.cnMenRemFilez.MenuItems.Add(Me.menRmDir)
        Me.cnMenRemFilez.MenuItems.Add(Me.menRemoteRename)
        Me.cnMenRemFilez.MenuItems.Add(Me.menRemreloadDir)
        '
        'menDownload
        '
        Me.menDownload.Text = "Download"
        '
        'menMkDir
        '
        Me.menMkDir.Text = "MakeDir"
        '
        'menRmDir
        '
        Me.menRmDir.Text = "Delete"
        '
        'menRemoteRename
        '
        Me.menRemoteRename.Text = "Rename"
        '
        'menRemreloadDir
        '
        Me.menRemreloadDir.Text = "Reload Dir"
        '
        'tabLog
        '
        Me.tabLog.Controls.Add(Me.btnSend)
        Me.tabLog.Controls.Add(Me.txtCmd)
        Me.tabLog.Controls.Add(Me.txtLog)
        Me.tabLog.Location = New System.Drawing.Point(4, 4)
        Me.tabLog.Size = New System.Drawing.Size(232, 247)
        Me.tabLog.Text = "Log"
        '
        'btnSend
        '
        Me.btnSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.btnSend.Location = New System.Drawing.Point(192, 3)
        Me.btnSend.Size = New System.Drawing.Size(40, 20)
        Me.btnSend.Text = "Send"
        '
        'txtCmd
        '
        Me.txtCmd.Location = New System.Drawing.Point(1, 3)
        Me.txtCmd.Size = New System.Drawing.Size(192, 18)
        Me.txtCmd.Text = "stat"
        '
        'txtLog
        '
        Me.txtLog.ContextMenu = Me.cnMenLog
        Me.txtLog.Location = New System.Drawing.Point(0, 24)
        Me.txtLog.Multiline = True
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(232, 224)
        Me.txtLog.Text = "neoFTP"
        '
        'cnMenLog
        '
        Me.cnMenLog.MenuItems.Add(Me.menClearLog)
        Me.cnMenLog.MenuItems.Add(Me.menSaveLog)
        '
        'menClearLog
        '
        Me.menClearLog.Text = "Clear"
        '
        'menSaveLog
        '
        Me.menSaveLog.Text = "Save"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.Add(Me.menClose)
        Me.MainMenu1.MenuItems.Add(Me.menConnect)
        Me.MainMenu1.MenuItems.Add(Me.MenuItem1)
        Me.MainMenu1.MenuItems.Add(Me.menOperation)
        Me.MainMenu1.MenuItems.Add(Me.menAbout)
        '
        'menClose
        '
        Me.menClose.Text = "X"
        '
        'menConnect
        '
        Me.menConnect.Text = "Connect"
        '
        'MenuItem1
        '
        Me.MenuItem1.MenuItems.Add(Me.menSmallIcons)
        Me.MenuItem1.MenuItems.Add(Me.menDatails)
        Me.MenuItem1.Text = "View"
        '
        'menSmallIcons
        '
        Me.menSmallIcons.Text = "Small Icons"
        '
        'menDatails
        '
        Me.menDatails.Checked = True
        Me.menDatails.Text = "Details"
        '
        'menOperation
        '
        Me.menOperation.Enabled = False
        Me.menOperation.Text = "_"
        '
        'menAbout
        '
        Me.menAbout.Text = "About"
        '
        'frmFTP
        '
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular)
        Me.Menu = Me.MainMenu1
        Me.Text = "neoFTP"

    End Sub

#End Region

    Private Sub menClearLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menClearLog.Click
        Me.txtLog.Text = Nothing
    End Sub

    Private Sub menSaveLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menSaveLog.Click
        Cursor.Current = Cursors.WaitCursor

        Dim path As String = theApp.AppPath & "\FTP.log"
        Dim str As New System.IO.FileStream(path, IO.FileMode.Create)
        Dim wrt As New System.IO.StreamWriter(str)
        wrt.Write(Me.txtLog.Text)
        wrt.Close()
        str.Close()
        Cursor.Current = Cursors.Default
        MsgBox("File saved @ " & path)

    End Sub

    Private Sub FTP_FTP_gotResponce(ByVal responce As String) Handles FTP.FTP_gotResponce
        add2Log(responce)
    End Sub

    Private Sub FTP_FTP_CommandSend(ByVal CmdStr As String) Handles FTP.FTP_CommandSend
        add2Log(CmdStr)
    End Sub

    Private Sub add2Log(ByVal str As String)

        Debug.WriteLine(str)
        System.Diagnostics.Debug.WriteLine(str)

        Me.txtLog.Text += vbCrLf & str
        Me.txtLog.SelectionStart = Me.txtLog.Text.Length
        Me.txtLog.ScrollToCaret()
    End Sub

    Private Sub FTP_FTP_Connected() Handles FTP.FTP_ConnectionSuccess
        Const tstr As String = "Disconnect"
        Me.menConnect.Text = tstr
        Me.btnConnect.Text = tstr
        Me.menOperation.Enabled = True
    End Sub

    Private Sub FTP_FTP_DisConnected() Handles FTP.FTP_DisConnected
        Const tstr As String = "Connect"
        Me.btnConnect.Text = tstr
        Me.menConnect.Text = tstr
        Me.lstRemoteFilez.Items.Clear()
        Me.tabRemFiles.Enabled = False
        Me.TabControl1.SelectedIndex = 0
        Me.tabHosts.BringToFront()
        Me.menOperation.Enabled = False
        Me.cmbDir.Items.Clear()
    End Sub

    Private Sub enableSIP(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServer.GotFocus, txtLogin.GotFocus, txtCmd.GotFocus, lblPass.GotFocus
        Me.InputPanel1.Enabled = True
    End Sub

    Private Sub fillDirectoryList(ByVal DirStr As String)

        Me.lstRemoteFilez.Items.Clear()
        If DirStr = "" Then Exit Sub

        Dim i, ii As Long
        Dim A() As String

        Try
            A = Split(DirStr, vbCrLf)
            For i = A.GetLowerBound(0) To A.GetUpperBound(0)
                Dim FI As New FileINFO(A(i))

                Dim LVI As New ListViewItem(FI.Name)

                If FI.isDirectory Then
                    LVI.ImageIndex = 4
                ElseIf FI.Name.EndsWith(".exe") Then
                    LVI.ImageIndex = 2
                Else
                    LVI.ImageIndex = 3
                End If
                Dim Tstr As String = IIf(FI.isDirectory, "[DIR]", FI.Size)
                LVI.SubItems.Add(Tstr)
                LVI.SubItems.Add(FI.Attribs)
                If FI.isDirectory Then
                    Me.lstRemoteFilez.Items.Insert(ii, LVI)
                    ii += 1
                Else
                    Me.lstRemoteFilez.Items.Add(LVI)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.ToString + " in fillDirectoryList()")
        End Try

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Me.FTP.FtpCommand(Me.txtCmd.Text)
    End Sub

    Private Sub Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menConnect.Click, btnConnect.Click
        Connect()
    End Sub

    Private Sub Connect()
        Cursor.Current = Cursors.WaitCursor

        If Me.FTP Is Nothing OrElse Not Me.FTP.Connected Then
            Me.FTP = New p7.Networking.p7FTP(Me.txtServer.Text, Me.udPort.Value)

            Me.tabLog.BringToFront()

            Try
                If Me.FTP.Connect() Then

                    If Me.FTP.IdVerify(Me.txtLogin.Text, Me.txtPass.Text) Then
                        LoadCurDir()
                        Try
                            Dim dirstr As String = Me.FTP.getCurDir
                            If Not Me.cmbDir.Items.Contains(dirstr) Then Me.cmbDir.Items.Add(dirstr)
                            Me.lblremCurDir.Text = dirstr

                            Me.tabRemFiles.Enabled = True
                            Me.TabControl1.SelectedIndex = 1
                            Me.tabRemFiles.BringToFront()

                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Else
                        Me.FTP.Close()
                    End If

                End If
            Catch ex As Exception
                MsgBox("Error while connecting. Please check your Network !" + vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
            End Try


        Else

            Me.FTP.Close()
            Me.tabHosts.BringToFront()

        End If

        Cursor.Current = Cursors.Default
    End Sub
    Private Sub LoadCurDir()
        Try
            Dim TSTR As String = Me.FTP.DirList()

            add2Log(TSTR)

            Me.fillDirectoryList(TSTR)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub menDatails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menDatails.Click
        Me.lstRemoteFilez.View = View.Details
        Me.lstLocFilez.View = View.Details
        Me.menDatails.Checked = True
        Me.menSmallIcons.Checked = Not Me.menDatails.Checked
    End Sub

    Private Sub menSmallIcons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menSmallIcons.Click
        Me.lstRemoteFilez.View = View.SmallIcon
        Me.lstLocFilez.View = View.SmallIcon
        Me.menSmallIcons.Checked = True
        Me.menDatails.Checked = Not Me.menSmallIcons.Checked
    End Sub

    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        Me.FTP.Close()
    End Sub

    Private Sub lstRemoteFilez_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstRemoteFilez.ColumnClick, lstLocFilez.ColumnClick
        Cursor.Current = Cursors.WaitCursor

        ' Create an instance of the ColHeader class. 
        Dim lstView As ListView = DirectCast(sender, ListView)
        Dim clickedCol As ColHeader = DirectCast(lstView.Columns(e.Column), ColHeader)

        ' Set the ascending property to sort in the opposite order.
        clickedCol.ascending = Not clickedCol.ascending

        ' Get the number of items in the list.
        Dim numItems As Integer = lstView.Items.Count

        ' Turn off display while data is repoplulated.
        lstView.BeginUpdate()

        ' Populate an ArrayList with a SortWrapper of each list item.
        Dim SortArray As New ArrayList
        Dim i As Integer
        For i = 0 To numItems - 1
            SortArray.Add(New SortWrapper(lstView.Items(i), e.Column))
        Next i

        ' Sort the elements in the ArrayList using a new instance of the SortComparer
        ' class. The parameters are the starting index, the length of the range to sort,
        ' and the IComparer implementation to use for comparing elements. Note that
        ' the IComparer implementation (SortComparer) requires the sort  
        ' direction for its constructor; true if ascending, othwise false.
        SortArray.Sort(0, SortArray.Count, New SortWrapper.SortComparer(clickedCol.ascending))

        ' Clear the list, and repopulate with the sorted items.
        lstView.Items.Clear()
        Dim z As Integer
        For z = 0 To numItems - 1
            lstView.Items.Add(CType(SortArray(z), SortWrapper).sortItem)
        Next z
        ' Turn display back on.
        lstView.EndUpdate()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub ChangeDir(ByVal DirStr As String)
        Cursor.Current = Cursors.WaitCursor

        If Not Me.FTP Is Nothing Then
            If Me.FTP.Chdir(DirStr) Then

                Dim tstr As String = Me.FTP.getCurDir()

                If Not Me.cmbDir.Items.Contains(tstr) Then
                    Me.cmbDir.Items.Add(tstr)
                End If
                Me.lblremCurDir.Text = tstr
                LoadCurDir()
            End If
        End If

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnDirUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDirUp.Click
        ChangeDir("..")
    End Sub

    Private Sub btnRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRoot.Click
        ChangeDir("/")
    End Sub

    Private Sub lstRemoteFilez_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRemoteFilez.ItemActivate
        Try

            Dim SelectedItem As ListViewItem = Me.lstRemoteFilez.Items(Me.lstRemoteFilez.SelectedIndices.Item(0))
            If Not SelectedItem Is Nothing AndAlso SelectedItem.ImageIndex = 4 Then
                ChangeDir(SelectedItem.Text)
            End If

        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try
    End Sub

    Private Sub cmbDir_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDir.SelectedIndexChanged
        ChangeDir(cmbDir.SelectedItem)
    End Sub

    Private Function getFirstSelectedItem(ByVal lstView As ListView) As ListViewItem
        Try
            Dim FirstSelIndex As Integer = lstView.SelectedIndices.Item(0)
            Dim SelectedItem As ListViewItem = lstView.Items(FirstSelIndex)
            Return SelectedItem
        Catch
            Return Nothing
        End Try
    End Function

    Private Sub menDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menDownload.Click
        Download()
    End Sub
    Private Sub Download()
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstRemoteFilez)
        If Not SelectedItem Is Nothing Then
            If SelectedItem.ImageIndex <> 4 Then

                Dim Save2Path As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\")
                Dim Path As String = Save2Path & SelectedItem.Text

                If System.IO.File.Exists(Path) Then
                    Dim frmQuest As New frmDwnldQuest
                    frmQuest.lblFilePath.Text = Path
                    frmQuest.ShowDialog()
                    If frmQuest.Answer = frmDwnldQuest.DialogAnswer.OverWrite Then
                        FTP_Download(Path, SelectedItem.Text, SelectedItem.SubItems(1).Text)
                    ElseIf frmQuest.Answer = frmDwnldQuest.DialogAnswer.Resume Then
                        Dim FileInfo As New System.IO.FileInfo(Path)
                        FTP_Download(Path, SelectedItem.Text, SelectedItem.SubItems(1).Text, FileInfo.Length)
                    End If
                Else
                    FTP_Download(Path, SelectedItem.Text, SelectedItem.SubItems(1).Text)
                End If
            End If
        End If

    End Sub
    Private Sub FTP_Download(ByVal Path As String, ByVal FileName As String, ByVal Size As Long, Optional ByVal offset As Long = 0)
        InitFrmProgressBar(FileName, Path, Size, frmProgress.WorkingMode.Download, offset)

        Try
            Me.FTP.FtpGetFile(FileName, Path, Size, offset)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        CloseFrmProgressbar()
        Me.FillLocalDirs(Me.treeLocalPath.SelectedNode)
    End Sub

    Private Sub InitFrmProgressBar(ByVal FileName As String, ByVal ToLoc As String, ByVal Size As Long, ByVal Mode As frmProgress.WorkingMode, Optional ByVal OffSet As Long = 0)
        If Size > 0 Then
            Me.frmProgress.lblFileName.Text = IIf(Mode = frmProgress.WorkingMode.Download, "Download ", "Upload ") & FileName
            Me.frmProgress.lblTO.Text = "To: " & ToLoc
            Me.frmProgress.ProgressBar1.MaxValue = Size
            Me.frmProgress.ProgressBar1.Value = OffSet
            Me.frmProgress.Show()
        End If
    End Sub

    Private Sub CloseFrmProgressbar()
        Try
            Me.frmProgress.Hide()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub menMkDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menMkDir.Click
        Dim DirName As String
        DirName = InputBox("Enter the Directory Name", "Directory Name", STDNEWDIRNAME)
        If DirName <> "" Then
            If Me.FTP.MkDir(DirName) Then
                Me.fillDirectoryList((Me.FTP.DirList))
            End If
        End If
    End Sub

    Private Sub menRmDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menRmDir.Click
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstRemoteFilez)

        If Not SelectedItem Is Nothing Then
            Dim antw As DialogResult
            antw = MsgBox("Delete " & SelectedItem.Text & " ?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Delete")
            If antw = DialogResult.No Then Exit Sub

            If Not SelectedItem Is Nothing AndAlso SelectedItem.ImageIndex = 4 Then
                If Me.FTP.RmDir(SelectedItem.Text) Then
                    Me.fillDirectoryList((Me.FTP.DirList))
                End If
            Else
                If Me.FTP.DelFile(SelectedItem.Text) Then
                    Me.fillDirectoryList((Me.FTP.DirList))
                End If
            End If
        End If
    End Sub

    Private Sub menClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menClose.Click
        Me.Close()
    End Sub

    Private Sub menAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menAbout.Click
        MsgBox(Me.lblInfo.Text)
    End Sub

    Private Sub menUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menUpload.Click
        Upload()
    End Sub

    Private Sub Upload()
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstLocFilez)
        If Not SelectedItem Is Nothing Then
            Dim FilePath As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\") & SelectedItem.Text
            Try
                Dim FileInfo As New System.IO.FileInfo(FilePath)
                InitFrmProgressBar(FileInfo.Name, FilePath, FileInfo.Length, frmProgress.WorkingMode.UpLoad)

                Me.FTP.FtpSendFile(FilePath, FileInfo.Length)
                Me.fillDirectoryList((Me.FTP.DirList))
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Me.CloseFrmProgressbar()
            End Try
        End If
    End Sub
    Private Sub frmFTP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadHosts()
        InitLocalPath()
        If Not Me.myLogin.mAddress Is Nothing Then
            Me.txtServer.Text = Me.myLogin.mAddress
            Me.udPort.Value = Me.myLogin.mPort
            If Not Me.myLogin.mLogin Is Nothing Then
                Me.txtLogin.Text = Me.myLogin.mLogin
            End If
            If Not Me.myLogin.mPassword Is Nothing Then
                Me.txtPass.Text = Me.myLogin.mPassword
            End If
            Me.Connect()
        End If
    End Sub
    Private Sub InitLocalPath()
        Dim TreeRoot As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode
        TreeRoot.SelectedImageIndex = 5
        TreeRoot.ImageIndex = 5
        TreeRoot.Text = "\"
        Me.treeLocalPath.Nodes.Add(TreeRoot)
        Me.FillLocalDirs(TreeRoot)
        Me.treeLocalPath.SelectedNode = TreeRoot
        Me.treeLocalPath.ExpandAll()
    End Sub

    Private Sub LoadHosts()
        Cursor.Current = Cursors.WaitCursor

        Dim HostLogin As p7.NetWorking.p7FTP.LoginStruct
        Dim inStr As System.IO.FileStream
        Dim rr As System.IO.BinaryReader
        Try
            inStr = New System.IO.FileStream(FName, IO.FileMode.Open)
            rr = New System.IO.BinaryReader(inStr)
            While rr.PeekChar <> -1

                HostLogin.mAddress = rr.ReadString
                HostLogin.mPort = rr.ReadInt32
                HostLogin.mLogin = rr.ReadString
                HostLogin.mPassword = p7.Crypto.KryptoMod.Encrypt(rr.ReadString, False)
                Me.cmbHosts.Items.Add(HostLogin)
            End While
            Me.cmbHosts.SelectedIndex = 0
        Catch IOEX As System.IO.FileNotFoundException
            System.Diagnostics.Debug.WriteLine(ioex.Message)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        Finally

            Try
                rr.Close()
                inStr.Close()
            Catch IOEX As System.IO.FileNotFoundException
                System.Diagnostics.Debug.WriteLine(ioex.Message)
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine(ex.Message)
            End Try

        End Try
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveHost()
    End Sub
    Private Sub SaveHost()
        Cursor.Current = Cursors.WaitCursor
        Dim HostLG As p7.NetWorking.p7FTP.LoginStruct

        If Not Me.txtServer.Text Is Nothing AndAlso Not Me.txtServer.Text = vbNullString Then

            HostLG.mAddress = Me.txtServer.Text
            HostLG.mPort = Me.udPort.Value
            HostLG.mLogin = Me.txtLogin.Text
            HostLG.mPassword = p7.Crypto.KryptoMod.Encrypt(Me.txtPass.Text, True)

            If Not Me.cmbHosts.Items.Contains(HostLG) Then

                Me.cmbHosts.Items.Add(HostLG)

                Dim i As Object
                Try
                    Dim out As New System.IO.FileStream(FName, IO.FileMode.Create)
                    Dim wrt As New System.IO.BinaryWriter(out)

                    For Each i In Me.cmbHosts.Items
                        HostLG = DirectCast(i, p7.NetWorking.p7FTP.LoginStruct)
                        wrt.Write(HostLG.mAddress)
                        wrt.Write(HostLG.mPort)
                        wrt.Write(HostLG.mLogin)
                        wrt.Write(HostLG.mPassword)

                    Next
                    wrt.Close()
                    out.Close()
                    Me.cmbHosts.SelectedItem = HostLG
                Catch ex As IO.IOException
                    MsgBox(ex.Message)
                Catch ex As Exception

                End Try
            End If
        End If
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub cmbHosts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbHosts.SelectedIndexChanged
        Dim HL As p7.NetWorking.p7FTP.LoginStruct
        HL = DirectCast(Me.cmbHosts.SelectedItem, p7.NetWorking.p7FTP.LoginStruct)

        Me.txtServer.Text = HL.mAddress
        Me.udPort.Value = HL.mPort
        Me.txtLogin.Text = HL.mLogin
        Me.txtPass.Text = HL.mPassword
    End Sub

    Private Sub frmProgress_OpCanceled() Handles frmProgress.OpCanceled
        Me.FTP.CancelOP = True
        'Me.FTP.FtpCommand("ABOR")
    End Sub

    Private Sub FTP_FTP_BytesReceived(ByVal cnt As Long) Handles FTP.FTP_BytesReceived, FTP.FTP_BytesSend
        If Not Me.frmProgress Is Nothing Then
            Me.frmProgress.getNextBytes(cnt)
        End If
    End Sub

    Private Sub FillLocalDirs(ByVal Node As TreeNode)

        Cursor.Current = Cursors.WaitCursor
        'Dim objDIR As System.IO.Directory
        Dim Path As String = Node.FullPath.Replace("\\", "\")
        Dim objDIR As New System.io.DirectoryInfo(Path)
        Dim FSI As System.IO.FileSystemInfo
        Dim i As Integer
        Me.treeLocalPath.BeginUpdate()
        Me.lstLocFilez.BeginUpdate()
        Me.lstLocFilez.Items.Clear()
        If Node.Nodes.Count > 0 Then Node.Nodes.Clear()
        Try
            For Each FSI In objDIR.GetFileSystemInfos()
                If (FSI.Attributes And IO.FileAttributes.Directory) = IO.FileAttributes.Directory Then

                    Dim LVI As New TreeNode(FSI.Name)
                    LVI.ImageIndex = 1
                    Node.Nodes.Add(LVI)
                Else
                    Dim LVI As New ListViewItem(FSI.Name)

                    If FSI.Extension = ".exe" Then
                        LVI.ImageIndex = 2
                    Else
                        LVI.ImageIndex = 3
                    End If

                    LVI.SubItems.Add(New System.IO.FileInfo(FSI.FullName).Length)
                    LVI.SubItems.Add(FSI.LastWriteTime)
                    Me.lstLocFilez.Items.Add(LVI)
                End If
            Next
            Node.Expand()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Me.treeLocalPath.EndUpdate()
        Me.lstLocFilez.EndUpdate()

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub treeLocalPath_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeLocalPath.AfterSelect
        Me.FillLocalDirs(Me.treeLocalPath.SelectedNode)
    End Sub

    Private Sub menLocMkDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocMkDir.Click
        Dim FilePath As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\")
        Dim DIR As System.IO.Directory
        Dim DirName As String
        DirName = InputBox("Enter the Directory Name", "Directory Name", STDNEWDIRNAME)
        If DirName.Trim.Length > 0 Then
            Try
                DIR.CreateDirectory(FilePath & DirName)
                Dim newDirNode As New TreeNode(DirName)
                Me.treeLocalPath.SelectedNode.Nodes.Add(newDirNode)
                Me.treeLocalPath.SelectedNode = newDirNode
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub menLocDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocDel.Click
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstLocFilez)
        If Not SelectedItem Is Nothing Then
            Dim antw As DialogResult
            antw = MsgBox("Delete  " & SelectedItem.Text, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Delete")
            If antw = DialogResult.Yes Then
                Dim FilePath As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\") & SelectedItem.Text
                Dim File As System.IO.File
                Try
                    File.Delete(FilePath)
                    Me.lstLocFilez.Items.Remove(SelectedItem)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub menOperation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menOperation.Click
        If Me.menOperation.Text = Me.OPSTRDOWN Then
            Me.Download()
        ElseIf Me.menOperation.Text = Me.OPSTRUPL Then
            Me.Upload()
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.FTP.Connected Then
            If Me.TabControl1.SelectedIndex = 1 Then
                Me.menOperation.Text = Me.OPSTRDOWN
                Me.menOperation.Enabled = True
            ElseIf Me.TabControl1.SelectedIndex = 2 Then
                Me.menOperation.Text = Me.OPSTRUPL
                Me.menOperation.Enabled = True
            Else
                Me.menOperation.Text = ""
                Me.menOperation.Enabled = False
            End If
        End If
    End Sub

    Private Sub menRemoteRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menRemoteRename.Click
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstRemoteFilez)
        If Not SelectedItem Is Nothing Then
            Dim NewFileName As String = InputBox("Enter the new Name", "Rename", SelectedItem.Text)
            If NewFileName <> "" AndAlso NewFileName <> SelectedItem.Text Then
                If Me.FTP.Rename(SelectedItem.Text, NewFileName) Then
                    SelectedItem.Text = NewFileName
                End If
            End If
        End If
    End Sub

    Private Sub chkAnonymous_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAnonymous.CheckStateChanged
        Me.txtLogin.Enabled = Not Me.chkAnonymous.Checked
        Me.txtPass.Enabled = Not Me.chkAnonymous.Checked
        If Me.chkAnonymous.Checked Then
            Me.txtLogin.Text = "Anonymous"
            Me.txtPass.Text = "neoFTP@playseven.com"
        End If
    End Sub

    Private Sub menLocalFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocalFileRename.Click
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstLocFilez)
        If Not SelectedItem Is Nothing Then
            Dim NewFileName As String = InputBox("Enter the new Filename", "Rename", SelectedItem.Text)
            If NewFileName <> "" AndAlso NewFileName <> SelectedItem.Text Then
                Dim FilePath As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\")
                Dim File As System.IO.File
                Try
                    File.Move(FilePath & SelectedItem.Text, FilePath & NewFileName)
                    SelectedItem.Text = NewFileName
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub menLocDelDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocDelDir.Click
        Dim objDir As System.IO.Directory
        Dim antw As DialogResult
        antw = MsgBox("Delete this Directory and all Subdirectories ?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Delete Dir ?")
        If antw = DialogResult.Yes Then
            Try
                Dim DirPath As String = (Me.treeLocalPath.SelectedNode.FullPath).Replace("\\", "\")
                objDir.Delete(DirPath, True)
                Me.treeLocalPath.SelectedNode = Me.treeLocalPath.SelectedNode.Parent
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub menLocDirRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocDirRename.Click
        Dim objDir As System.IO.Directory
        Dim DirName As String
        Dim DirPath As String = (Me.treeLocalPath.SelectedNode.FullPath).Replace("\\", "\")
        DirName = InputBox("Enter the Directory Name", "Directory Name", DirPath.Substring(DirPath.LastIndexOf("\") + 1))
        If DirName <> "" Then
            Try
                objDir.Move(DirPath, DirPath.Substring(0, DirPath.LastIndexOf("\") + 1) & DirName)
                Me.treeLocalPath.SelectedNode.Text = DirName
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub menLocalExec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menLocalExec.Click
        Dim DirPath As String = (Me.treeLocalPath.SelectedNode.FullPath & "\").Replace("\\", "\")
        Dim SelectedItem As ListViewItem = getFirstSelectedItem(Me.lstLocFilez)
        If Not SelectedItem Is Nothing Then
            Dim FileExec As New AppWrp
            FileExec.Run(DirPath & SelectedItem.Text)
        End If
    End Sub


    Private Sub menRemreloadDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menRemreloadDir.Click
        Me.LoadCurDir()
    End Sub
End Class

'[DllImport("shell32.dll")]
'public static extern IntPtr SHGetFileInfo(string pszPath, uint
'dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

'[StructLayout(LayoutKind.Sequential)]
'public struct SHFILEINFO
'{
'    public IntPtr hIcon;
'    public IntPtr iIcon;
'    public uint dwAttributes;
'    // [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
'    public string szDisplayName;
'    // [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
'    public string szTypeName;
'};
