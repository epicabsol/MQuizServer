﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmdStartStop = New Xenon.XenonButton()
        Me.txtLog = New Xenon.XenonTextBox()
        Me.cmdAdd = New Xenon.XenonButton()
        Me.cmdRemoveImage = New Xenon.XenonButton()
        Me.cmdSetPassword = New Xenon.XenonButton()
        Me.lstImages = New Xenon.XenonListBox()
        Me.tmrRefreshData = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = Global.MQuizServer.My.Resources.Resources.offline_button_x128
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(286, 128)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'cmdStartStop
        '
        Me.cmdStartStop.Image = Nothing
        Me.cmdStartStop.LayeringHost = Nothing
        Me.cmdStartStop.Location = New System.Drawing.Point(12, 146)
        Me.cmdStartStop.MouseOver = False
        Me.cmdStartStop.Name = "cmdStartStop"
        Me.cmdStartStop.Size = New System.Drawing.Size(286, 38)
        Me.cmdStartStop.TabIndex = 1
        Me.cmdStartStop.Text = "Start"
        Me.cmdStartStop.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLog.Location = New System.Drawing.Point(304, 12)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(600, 333)
        Me.txtLog.TabIndex = 2
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Image = Nothing
        Me.cmdAdd.LayeringHost = Nothing
        Me.cmdAdd.Location = New System.Drawing.Point(12, 325)
        Me.cmdAdd.MouseOver = False
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(71, 20)
        Me.cmdAdd.TabIndex = 4
        Me.cmdAdd.Text = "Add..."
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdRemoveImage
        '
        Me.cmdRemoveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveImage.Image = Nothing
        Me.cmdRemoveImage.LayeringHost = Nothing
        Me.cmdRemoveImage.Location = New System.Drawing.Point(227, 325)
        Me.cmdRemoveImage.MouseOver = False
        Me.cmdRemoveImage.Name = "cmdRemoveImage"
        Me.cmdRemoveImage.Size = New System.Drawing.Size(71, 20)
        Me.cmdRemoveImage.TabIndex = 5
        Me.cmdRemoveImage.Text = "Remove"
        Me.cmdRemoveImage.UseVisualStyleBackColor = True
        '
        'cmdSetPassword
        '
        Me.cmdSetPassword.Image = Nothing
        Me.cmdSetPassword.LayeringHost = Nothing
        Me.cmdSetPassword.Location = New System.Drawing.Point(12, 190)
        Me.cmdSetPassword.MouseOver = False
        Me.cmdSetPassword.Name = "cmdSetPassword"
        Me.cmdSetPassword.Size = New System.Drawing.Size(286, 20)
        Me.cmdSetPassword.TabIndex = 6
        Me.cmdSetPassword.Text = "Set Teacher Password..."
        Me.cmdSetPassword.UseVisualStyleBackColor = True
        '
        'lstImages
        '
        Me.lstImages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstImages.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer), CType(CType(12, Byte), Integer))
        Me.lstImages.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstImages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstImages.FormattingEnabled = True
        Me.lstImages.IntegralHeight = False
        Me.lstImages.Location = New System.Drawing.Point(12, 216)
        Me.lstImages.Name = "lstImages"
        Me.lstImages.Size = New System.Drawing.Size(286, 103)
        Me.lstImages.TabIndex = 7
        '
        'tmrRefreshData
        '
        Me.tmrRefreshData.Enabled = True
        Me.tmrRefreshData.Interval = 300
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(916, 357)
        Me.Controls.Add(Me.lstImages)
        Me.Controls.Add(Me.cmdSetPassword)
        Me.Controls.Add(Me.cmdRemoveImage)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.cmdStartStop)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "MQuiz Server"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmdStartStop As Xenon.XenonButton
    Friend WithEvents txtLog As Xenon.XenonTextBox
    Friend WithEvents cmdAdd As Xenon.XenonButton
    Friend WithEvents cmdRemoveImage As Xenon.XenonButton
    Friend WithEvents cmdSetPassword As Xenon.XenonButton
    Friend WithEvents lstImages As Xenon.XenonListBox
    Friend WithEvents tmrRefreshData As System.Windows.Forms.Timer

End Class
