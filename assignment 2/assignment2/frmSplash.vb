Public Class frmSplash
    Private Sub FrmSplash_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Timer2.Start()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If ProgressBar1.Value = 100 Then
            Timer2.Stop()
            frmStudentManagement.Show()
            Me.Hide()
        Else
            ProgressBar1.Value += 1
        End If














    End Sub
End Class