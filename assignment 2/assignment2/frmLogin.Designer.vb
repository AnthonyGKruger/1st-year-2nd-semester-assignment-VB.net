<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.grpCredentials = New System.Windows.Forms.GroupBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblLoginTitle = New System.Windows.Forms.Label()
        Me.rdbStudent = New System.Windows.Forms.RadioButton()
        Me.rdbEmp = New System.Windows.Forms.RadioButton()
        Me.rdbAdmin = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grpCredentials.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(24, 234)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(199, 77)
        Me.btnLogin.TabIndex = 5
        Me.btnLogin.Text = "&Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(302, 234)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(199, 77)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(222, 60)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(279, 31)
        Me.txtUsername.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(222, 115)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(279, 31)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'grpCredentials
        '
        Me.grpCredentials.Controls.Add(Me.rdbAdmin)
        Me.grpCredentials.Controls.Add(Me.rdbEmp)
        Me.grpCredentials.Controls.Add(Me.rdbStudent)
        Me.grpCredentials.Controls.Add(Me.lblPassword)
        Me.grpCredentials.Controls.Add(Me.lblUsername)
        Me.grpCredentials.Controls.Add(Me.txtUsername)
        Me.grpCredentials.Controls.Add(Me.txtPassword)
        Me.grpCredentials.Controls.Add(Me.btnLogin)
        Me.grpCredentials.Controls.Add(Me.btnCancel)
        Me.grpCredentials.Location = New System.Drawing.Point(540, 169)
        Me.grpCredentials.Name = "grpCredentials"
        Me.grpCredentials.Size = New System.Drawing.Size(532, 335)
        Me.grpCredentials.TabIndex = 7
        Me.grpCredentials.TabStop = False
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(23, 121)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(112, 25)
        Me.lblPassword.TabIndex = 10
        Me.lblPassword.Text = "Password:"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(19, 66)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(116, 25)
        Me.lblUsername.TabIndex = 8
        Me.lblUsername.Text = "Username:"
        '
        'lblLoginTitle
        '
        Me.lblLoginTitle.AutoSize = True
        Me.lblLoginTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoginTitle.Location = New System.Drawing.Point(141, 50)
        Me.lblLoginTitle.Name = "lblLoginTitle"
        Me.lblLoginTitle.Size = New System.Drawing.Size(837, 63)
        Me.lblLoginTitle.TabIndex = 9
        Me.lblLoginTitle.Text = "School management system login"
        '
        'rdbStudent
        '
        Me.rdbStudent.AutoSize = True
        Me.rdbStudent.Checked = True
        Me.rdbStudent.Location = New System.Drawing.Point(28, 181)
        Me.rdbStudent.Name = "rdbStudent"
        Me.rdbStudent.Size = New System.Drawing.Size(117, 29)
        Me.rdbStudent.TabIndex = 2
        Me.rdbStudent.TabStop = True
        Me.rdbStudent.Text = "Student"
        Me.rdbStudent.UseVisualStyleBackColor = True
        '
        'rdbEmp
        '
        Me.rdbEmp.AutoSize = True
        Me.rdbEmp.Location = New System.Drawing.Point(178, 181)
        Me.rdbEmp.Name = "rdbEmp"
        Me.rdbEmp.Size = New System.Drawing.Size(138, 29)
        Me.rdbEmp.TabIndex = 3
        Me.rdbEmp.Text = "Employee"
        Me.rdbEmp.UseVisualStyleBackColor = True
        '
        'rdbAdmin
        '
        Me.rdbAdmin.AutoSize = True
        Me.rdbAdmin.Location = New System.Drawing.Point(340, 181)
        Me.rdbAdmin.Name = "rdbAdmin"
        Me.rdbAdmin.Size = New System.Drawing.Size(103, 29)
        Me.rdbAdmin.TabIndex = 4
        Me.rdbAdmin.TabStop = True
        Me.rdbAdmin.Text = "Admin"
        Me.rdbAdmin.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.assignment2.My.Resources.Resources.loginPic
        Me.PictureBox1.Location = New System.Drawing.Point(68, 169)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(348, 325)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1139, 549)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblLoginTitle)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.grpCredentials)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login "
        Me.grpCredentials.ResumeLayout(False)
        Me.grpCredentials.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnLogin As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents grpCredentials As GroupBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblLoginTitle As Label
    Friend WithEvents rdbEmp As RadioButton
    Friend WithEvents rdbStudent As RadioButton
    Friend WithEvents rdbAdmin As RadioButton
End Class
