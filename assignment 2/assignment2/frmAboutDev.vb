Public Class frmAboutDev
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Me.Hide()

    End Sub

    Private Sub FrmAboutDev_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        frmStudentManagement.aboutDevShown = True

    End Sub
End Class