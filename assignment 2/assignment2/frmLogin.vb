
Imports System.Data
Imports System.Data.OleDb
Public Class frmLogin

    Public username, password, userType As String


    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AcceptButton = btnLogin

        txtUsername.Focus()

    End Sub

    Private Sub RdbStudent_CheckedChanged(sender As Object, e As EventArgs) Handles rdbStudent.CheckedChanged
        If rdbEmp.Checked = True Then
            rdbEmp.Checked = False
            rdbAdmin.Checked = False
            rdbStudent.Checked = True
        End If
    End Sub

    Private Sub RdbEmp_CheckedChanged(sender As Object, e As EventArgs) Handles rdbEmp.CheckedChanged
        If rdbStudent.Checked = True Then
            rdbStudent.Checked = False
            rdbAdmin.Checked = False
            rdbEmp.Checked = True
        End If
    End Sub

    Private Sub RdbAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAdmin.CheckedChanged
        If rdbAdmin.Checked = True Then
            rdbStudent.Checked = False
            rdbAdmin.Checked = True
            rdbEmp.Checked = False
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Application.Exit()

    End Sub


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        username = txtUsername.Text
        password = txtPassword.Text

        If rdbStudent.checked Then
            userType = "student"
        ElseIf rdbEmp.checked Then
            userType = "employee"
        ElseIf rdbAdmin.Checked Then
            userType = "admin"
        End If

        Dim provider As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;"

        Dim dataSource As String = "Data Source = C:\Users\Anthony\iCloudDrive\Documents\Anthony\Richfield\year_1\semester_2\Programming 512\Assignments\assignment 2\assignment.mdb"

        Dim connection As New OleDb.OleDbConnection

        Dim loginCMD As OleDbCommand

        Dim loginValidator As OleDbDataReader

        Dim strSqlCMD As String = "SELECT * FROM informationTable WHERE [Username] = '" & username & "' AND [Password]= '" & password & "'AND [employeeORstudent]= '" & userType & "'"

        Try

            connection.ConnectionString = provider & dataSource

            connection.Open()

            loginCMD = New OleDbCommand(strSqlCMD, connection)

            loginValidator = loginCMD.ExecuteReader

            Try
                If (loginValidator.Read() = True) Then


                    frmSplash.Show()

                    Me.Hide()

                    MessageBox.Show("User " & username & " has successfully logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    connection.Close()

                Else

                    Dim MightCancel As String = MessageBox.Show("Credentials were incorrect, please try again.", "Unsuccessful.", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

                    If MightCancel = DialogResult.Cancel Then

                        Me.Close()
                    Else

                        txtUsername.Focus()

                    End If

                End If

            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            End Try

        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        End Try

    End Sub
End Class
