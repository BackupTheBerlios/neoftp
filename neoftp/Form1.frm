VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Beispiel für die Crypto-API"
   ClientHeight    =   2745
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4680
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2745
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.Frame Frame2 
      Caption         =   "Entschlüsseln"
      Height          =   1215
      Left            =   120
      TabIndex        =   3
      Top             =   1440
      Width           =   4455
      Begin VB.TextBox Text4 
         Height          =   285
         Left            =   2640
         TabIndex        =   7
         Text            =   "Passwort"
         Top             =   360
         Width           =   1695
      End
      Begin VB.CommandButton Command2 
         Caption         =   "&Decrypt"
         Height          =   375
         Left            =   120
         TabIndex        =   5
         Top             =   720
         Width           =   4215
      End
      Begin VB.TextBox Text2 
         Height          =   285
         Left            =   120
         TabIndex        =   4
         Top             =   360
         Width           =   2415
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Verschlüsseln"
      Height          =   1215
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   4455
      Begin VB.TextBox Text3 
         Height          =   285
         Left            =   2640
         TabIndex        =   6
         Text            =   "Passwort"
         Top             =   360
         Width           =   1695
      End
      Begin VB.CommandButton Command1 
         Caption         =   "&Encrypt"
         Height          =   375
         Left            =   120
         TabIndex        =   2
         Top             =   720
         Width           =   4215
      End
      Begin VB.TextBox Text1 
         Height          =   285
         Left            =   120
         TabIndex        =   1
         Top             =   360
         Width           =   2415
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim objCrypt As Class1

Private Sub Command1_Click()
    Text2 = objCrypt.EncryptString(Text1, Text3)
    
End Sub

Private Sub Command2_Click()
    Text1 = objCrypt.DecryptString(Text2, Text4)
End Sub

Private Sub Form_Load()
    Set objCrypt = New Class1
    objCrypt.StartSession
End Sub

Private Sub Form_Unload(Cancel As Integer)
    objCrypt.EndSession
End Sub
