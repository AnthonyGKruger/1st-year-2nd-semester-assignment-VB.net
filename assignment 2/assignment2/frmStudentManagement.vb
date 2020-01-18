Imports System.Data
Imports System.Data.OleDb
Public Class frmStudentManagement

    'declaration of a variable to be used to validate who the user is (student/employee/admin)
    Dim userType As String

    'declaration of provider for what type of connection we need to access our database
    Dim provider As String = "PROVIDER=Microsoft.Jet.OLEDB.4.0;"

    'decalaration of destination/path to database file
    Dim dataSource As String = "Data Source = C:\Users\Anthony\iCloudDrive\Documents\Anthony\Richfield\year_1\semester_2\Programming 512\Assignments\assignment 2\assignment.mdb"

    'declaration of connection object. it uses our provider and source as a connection string to connect to the database
    Dim connection As New OleDb.OleDbConnection

    'declaration of our data adapter that acts as a go between for our database and dataset
    Dim studentDataAdapter As New OleDbDataAdapter

    'declaration of our data adapter that acts as a go between for our database and dataset
    Dim employeeDataAdapter As New OleDbDataAdapter

    'declaration of our data adapter that acts as a go between for our database and dataset
    Dim courseDataAdapter As New OleDbDataAdapter

    'declaration of our data adapter that acts as a go between for our database and dataset this specific line will allow us to add users to our login table
    Dim loginDataAdapter As New OleDbDataAdapter

    'decleration of our dataset that will store a copy of a specific table for the program to work on
    Dim studentDataSet As New DataSet

    'decleration of our dataset that will store a copy of a specific table for the program to work on
    Dim empDataSet As New DataSet

    'decleration of our dataset that will store a copy of a specific table for the program to work on
    Dim courseDataSet As New DataSet

    'decleration of our dataset that will store a copy of a specific table for the program to work on this specific line will allow us to add users to our login table
    Dim loginDataSet As New DataSet

    'declaration of query we will use when connecting to the database, this will change alot and will allow us to access the different tables in the database
    Dim strSqlCMD As String

    'declaration of query we will use when connecting to the database, this will change alot and will allow us to access the different tables in the database
    Dim loginStrSqlCMD As String

    'this variable is used to tell the program that the about developer form has been show and will be changed to True when frmAboutDev does the onload event
    Public aboutDevShown = False

    Dim tempFlag As Boolean = False

    Private Sub FrmStudentManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'the bellow if statement will assign the relevent user type string to the usertype variable and will disable certain tabs for specific users
        If frmLogin.rdbStudent.Checked Then
            userType = "student"
            tabMain.TabPages(1).Enabled = False
            'tabMain.TabPages(2).Enabled = False        I commented this out because according to spec the student should be able to see employees
        ElseIf frmLogin.rdbEmp.Checked Then
            userType = "employee"
            tabMain.TabPages(0).Enabled = False
            tabMain.TabPages(3).Enabled = False
            tabMain.TabPages(4).Enabled = False
        ElseIf frmLogin.rdbAdmin.Checked Then
            userType = "admin"
        End If

        'this function is called to update the data grid view on the salary info tab
        updateDataGrid()
    End Sub

    'the 5 click events bellow allow the application to exit when the exit button is clicked 
    Private Sub BtnExitTabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnExitTabCourseDetail.Click
        Application.Exit()
    End Sub

    Private Sub BtnExitTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnExitTabStudentInformation.Click
        Application.Exit()
    End Sub

    Private Sub BtnExitTabSalaryInfo_Click(sender As Object, e As EventArgs) Handles btnExitTabSalaryInfo.Click
        Application.Exit()
    End Sub

    Private Sub BtnExitTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnExitTabEmpInfo.Click
        Application.Exit()
    End Sub

    Private Sub BtnExitTabFeeInfo_Click(sender As Object, e As EventArgs) Handles btnExitTabFeeInfo.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover

        'the bellow if statement checks whether aboutDev variable is equal to false when the mouse hovers over the picture in the about developer tab.
        'if false frmaboutDev is shown
        If aboutDevShown = False Then
            frmAboutDev.Show()
        End If

    End Sub

    Private Sub BtnClearTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnClearTabStudentInformation.Click

        'clears the text boxes in the student info tab, checks if the user is admin to fully clear all textboxes 
        'because only admin can clear certain data records And save them.
        If txtStudentIDTabStudentInfo.Text <> "" Then

            txtContactNumberTabStudentInformation.Clear()
            txtEmailIDTabStudentInformation.Clear()
            txtLocalAddressStudentInformation.Clear()
            txtPermanentAddressTabStudentInformation.Clear()

            If userType = "admin" Then

                txtMotherNameTabStudentInformation.Clear()
                txtFirstNameTabStudentInfo.Clear()
                txtMiddleNameTabStudentInformation.Clear()
                txtStudentIDTabStudentInfo.Clear()
                txtBloodGroupTabStudentInformation.Clear()
                txtFathersNameTabStudentInformation.Clear()
                txtLastNameTabStudentInformation.Clear()
                dtpTabStudentInformation.Update()

                'resets/clears student data set of any data it may have had for other click events to function
                studentDataSet.Reset()

            End If
        End If

    End Sub

    Private Sub BtnFindTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnFindTabStudentInformation.Click

        tempFlag = True

        Try

            'clears the text boxes in the student info tab, checks if the user is admin to fully clear all textboxes 
            'because only admin can clear certain data records And save them.
            If txtFirstNameTabStudentInfo.Text <> "" Then

                txtContactNumberTabStudentInformation.Clear()
                txtEmailIDTabStudentInformation.Clear()
                txtLocalAddressStudentInformation.Clear()
                txtPermanentAddressTabStudentInformation.Clear()
                txtMotherNameTabStudentInformation.Clear()
                txtFirstNameTabStudentInfo.Clear()
                txtMiddleNameTabStudentInformation.Clear()
                txtBloodGroupTabStudentInformation.Clear()
                txtFathersNameTabStudentInformation.Clear()
                txtLastNameTabStudentInformation.Clear()
                dtpTabStudentInformation.Update()

                'resets/clears student data set of any data it may have had for other click events to function
                studentDataSet.Reset()

            End If

            'checks that the student ID was used to search for students in the database
            If txtStudentIDTabStudentInfo.Text <> "" Then

                'declaring query string and open a connection and filling the dataset with information from the database through the data adapter
                strSqlCMD = "SELECT * FROM studentTable WHERE [studentID] ='" & txtStudentIDTabStudentInfo.Text & "'"
                connection.ConnectionString = provider & dataSource
                connection.Open()
                studentDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
                studentDataAdapter.Fill(studentDataSet, "studentTable")

                'this is a check to see that if the usertype is a student then the student who is logged in can only search for their details
                If userType = "student" Then
                    If frmLogin.username <> studentDataSet.Tables("studentTable").Rows(0).Item("firstName") Then
                        MessageBox.Show("You can only look up your own information as a student.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        studentDataSet.Reset()
                        connection.Close()
                        Exit Sub
                    End If
                End If

                'populates all textboxes and fields with information from the relevent databases
                txtStudentIDTabStudentInfo.Text = studentDataSet.Tables("studentTable").Rows(0).Item("studentID")
                txtFirstNameTabStudentInfo.Text = studentDataSet.Tables("studentTable").Rows(0).Item("firstName")
                txtLastNameTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("surname")
                txtMiddleNameTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("middleName")
                txtBloodGroupTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("bloodType")
                txtContactNumberTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("telephone")
                txtMotherNameTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("motherName")
                txtFathersNameTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("fatherName")
                txtEmailIDTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("email")
                txtLocalAddressStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("localAddress")
                txtPermanentAddressTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("permanentAddress")
                dtpTabStudentInformation.Text = studentDataSet.Tables("studentTable").Rows(0).Item("dateOfBirth")
                cmbCategoryTabStudentInformation.SelectedItem = studentDataSet.Tables("studentTable").Rows(0).Item("category")
                cmbGenderTabStudentInfo.SelectedItem = studentDataSet.Tables("studentTable").Rows(0).Item("gender")

            Else

                'message when a student ID wasnt used for the search
                MessageBox.Show("Please enter the student ID for the student details you are looking for.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As System.IndexOutOfRangeException

            MessageBox.Show("That was an invalid student ID.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        Finally

            connection.Close()

        End Try
    End Sub

    Private Sub BtnModifyTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnModifyTabStudentInformation.Click

        'declaration of a variable that will hold a dialog result and will be used in an if statement 
        Dim confirmation As String = MessageBox.Show("Are you sure you want to save this information?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        'checking to make sure the current user is not an employee because employees shouldnt be able to change student details unless authorized
        If userType = "admin" Or userType = "student" Then

            Try

                'if the user is a student check to see that they are searching for their information and exit sub if they arent
                If userType = "student" Then

                    If frmLogin.username <> studentDataSet.Tables("studentTable").Rows(0).Item("firstName") Then

                        MessageBox.Show("You can only update your own information as a student.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub

                    End If

                End If

                'setting up a command builder that will update our database 
                Dim cb As New OleDb.OleDbCommandBuilder(studentDataAdapter)

                'checking that the user is using a student ID to search
                If txtStudentIDTabStudentInfo.Text <> "" And tempFlag Then

                    'checking to see if the user did confirm they would like to save
                    If confirmation = System.Windows.Forms.DialogResult.Yes Then

                        'checking to see which user type is searching, only admin has full access to change all records
                        If userType = "student" Then

                            studentDataSet.Tables("studentTable").Rows(0).Item("telephone") = txtContactNumberTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("email") = txtEmailIDTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("localAddress") = txtLocalAddressStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("permanentAddress") = txtPermanentAddressTabStudentInformation.Text

                            'updating the database
                            studentDataAdapter.Update(studentDataSet, "studentTable")

                        Else    'the user is a admin

                            studentDataSet.Tables("studentTable").Rows(0).Item("firstName") = txtFirstNameTabStudentInfo.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("surname") = txtLastNameTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("middleName") = txtMiddleNameTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("bloodType") = txtBloodGroupTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("telephone") = txtContactNumberTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("motherName") = txtMotherNameTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("fatherName") = txtFathersNameTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("email") = txtEmailIDTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("localAddress") = txtLocalAddressStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("permanentAddress") = txtPermanentAddressTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("dateOfBirth") = dtpTabStudentInformation.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("category") = cmbCategoryTabStudentInformation.SelectedItem
                            studentDataSet.Tables("studentTable").Rows(0).Item("gender") = cmbGenderTabStudentInfo.SelectedItem

                            'updating the database
                            studentDataAdapter.Update(studentDataSet, "studentTable")
                        End If

                    End If

                Else  'the user did not enter anything into the ID text box

                    MessageBox.Show("Please search for the student details you would like to change using the student ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If

            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

            End Try

        Else 'the user is the wrong type of user and cant change these details

            MessageBox.Show("only students and admin can modify details.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnDeleteTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnDeleteTabStudentInformation.Click

        'checking if user is admin, only admin can delete records
        If userType = "admin" Then

            Try

                'setting up a query that will delete from our database it will use whatever is entered in the ID textbox as a parameter 
                Dim deleteCMD As New OleDbCommand("DELETE * FROM studentTable WHERE studentID =?", connection)

                'inserting parameter into query
                deleteCMD.Parameters.AddWithValue("@studentID", txtStudentIDTabStudentInfo.Text)

                'starting connection to database
                connection.ConnectionString = provider & dataSource
                connection.Open()

                'executing the delete query
                deleteCMD.ExecuteNonQuery()

                MessageBox.Show("The record has successfully been deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try

        Else     'the user is not admin and only admin can delete records

            MessageBox.Show("Only admin can delete records.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnSubmitTabStudentInformation_Click(sender As Object, e As EventArgs) Handles btnSubmitTabStudentInformation.Click

        'checking if the user is admin. only admin can save new records
        If userType = "admin" Then

            Try

                'setting up our query string that we will use to extract information from the database and connecting to the database
                strSqlCMD = "SELECT * FROM studentTable"
                connection.ConnectionString = provider & dataSource
                connection.Open()
                studentDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
                studentDataAdapter.Fill(studentDataSet, "studentTable")

                'asking the user if they are sure they want to save the information and using the result in an if statement 
                Dim confirmation As DialogResult = MessageBox.Show("Are you sure you would like to save the new details you have just entered?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                Dim cbStudentDataAdapter As New OleDb.OleDbCommandBuilder(studentDataAdapter)

                'checking if the ID that user wants to use as a new ID is not already in the database if so exiting the sub
                If checkForDuplicate("student", txtStudentIDTabStudentInfo.Text) Then
                    MessageBox.Show("That record is already in the data base", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    connection.Close()
                    Exit Sub
                End If

                If confirmation = DialogResult.Yes Then

                    'adding a new row that will be added into the dataset and will then update our database at the end
                    Dim newRow As DataRow = studentDataSet.Tables("studentTable").NewRow()

                    newRow.Item("studentID") = txtStudentIDTabStudentInfo.Text
                    newRow.Item("firstName") = txtFirstNameTabStudentInfo.Text
                    newRow.Item("surname") = txtLastNameTabStudentInformation.Text
                    newRow.Item("middleName") = txtMiddleNameTabStudentInformation.Text
                    newRow.Item("bloodType") = txtBloodGroupTabStudentInformation.Text
                    newRow.Item("telephone") = txtContactNumberTabStudentInformation.Text
                    newRow.Item("motherName") = txtMotherNameTabStudentInformation.Text
                    newRow.Item("fatherName") = txtFathersNameTabStudentInformation.Text
                    newRow.Item("email") = txtEmailIDTabStudentInformation.Text
                    newRow.Item("localAddress") = txtLocalAddressStudentInformation.Text
                    newRow.Item("permanentAddress") = txtPermanentAddressTabStudentInformation.Text
                    newRow.Item("dateOfBirth") = dtpTabStudentInformation.Text
                    newRow.Item("category") = cmbCategoryTabStudentInformation.SelectedItem
                    newRow.Item("gender") = cmbGenderTabStudentInfo.SelectedItem


                    'adding the new row to the dataset
                    studentDataSet.Tables("studentTable").Rows.Add(newRow)

                    'updating the database with the dataset
                    studentDataAdapter.Update(studentDataSet, "studentTable")

                    MessageBox.Show("You have successfully updated the database with a new record!", "New record added", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try

        Else     'user was not admin

            MessageBox.Show("only admin can add new students.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnFindTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnFindTabEmpInfo.Click
        tempFlag = True

        'checking if the user is admin, if admin user has more privelages
        Try

            'making sure the user did enter a ID in the ID textbox
            If txtEmpID.Text <> "" Then

                    txtEmpMobile.Clear()
                    txtEmpEmail.Clear()
                    txtEmpPhone.Clear()
                    txtEmpLocalAddress.Clear()
                    txtEmpPermanentAddress.Clear()

                    If userType = "admin" Then

                        txtEmpFirstName.Clear()
                        txtEmpTitle.Clear()
                        txtEmpLastName.Clear()
                        txtEmpDesignation.Clear()
                        txtEmpType.Clear()
                        txtSalary.Clear()
                        dateEmpDOB.Update()

                        'resetting the dataset for new information to be added
                        empDataSet.Reset()

                    End If
                End If


                'checking to make sure the ID textbox isnt clear 
                If txtEmpID.Text <> "" Then

                    'setting up connection to database with the relevent query
                    strSqlCMD = "SELECT * FROM employeeTable WHERE [employeeID] ='" & txtEmpID.Text & "'"
                    connection.ConnectionString = provider & dataSource
                    connection.Open()
                    employeeDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
                    employeeDataAdapter.Fill(empDataSet, "employeeTable")

                    'checking if the user is employee. employees can only look up their own information
                    If userType = "employee" Then
                        If frmLogin.username <> empDataSet.Tables("employeeTable").Rows(0).Item("firstName") Then
                            MessageBox.Show("You can only look up your own information as a employee.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            empDataSet.Reset()
                            connection.Close()
                            Exit Sub
                        End If
                    End If

                    'populating the relevent fields with searched data
                    txtEmpID.Text = empDataSet.Tables("employeeTable").Rows(0).Item("employeeID")
                    txtEmpFirstName.Text = empDataSet.Tables("employeeTable").Rows(0).Item("firstName")
                    txtEmpLastName.Text = empDataSet.Tables("employeeTable").Rows(0).Item("surname")
                    txtEmpTitle.Text = empDataSet.Tables("employeeTable").Rows(0).Item("empTitle")
                    txtEmpMobile.Text = empDataSet.Tables("employeeTable").Rows(0).Item("cellphone")
                    dateEmpDOB.Text = empDataSet.Tables("employeeTable").Rows(0).Item("dateOfBirth")
                    txtEmpPhone.Text = empDataSet.Tables("employeeTable").Rows(0).Item("cellphone")
                    txtEmpEmail.Text = empDataSet.Tables("employeeTable").Rows(0).Item("email")
                    txtEmpLocalAddress.Text = empDataSet.Tables("employeeTable").Rows(0).Item("localAddress")
                    txtEmpPermanentAddress.Text = empDataSet.Tables("employeeTable").Rows(0).Item("permanentAddress")
                    txtEmpDesignation.Text = empDataSet.Tables("employeeTable").Rows(0).Item("jobTitle")
                    txtEmpType.Text = empDataSet.Tables("employeeTable").Rows(0).Item("employeeType")
                    txtSalary.Text = empDataSet.Tables("employeeTable").Rows(0).Item("salary")


                Else    'the ID text box was empty

                    MessageBox.Show("Please enter the employee ID for the employee details you are looking for.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If

            Catch ex As System.IndexOutOfRangeException    'invalid or non existent ID was used for the search

                MessageBox.Show("That was an invalid employee ID.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

                connection.Close()

            End Try

    End Sub

    Private Sub BtnClearTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnClearTabEmpInfo.Click

        Try

            'clearing textboxes in employee info tab, if user is admin all tabs are cleared
            If txtEmpID.Text <> "" Then

                txtEmpMobile.Clear()
                txtEmpEmail.Clear()
                txtEmpPhone.Clear()
                txtEmpLocalAddress.Clear()
                txtEmpPermanentAddress.Clear()

                If userType = "admin" Then

                    txtEmpFirstName.Clear()
                    txtEmpTitle.Clear()
                    txtEmpLastName.Clear()
                    txtEmpDesignation.Clear()
                    txtEmpType.Clear()
                    txtSalary.Clear()
                    txtEmpID.Clear()
                    dateEmpDOB.Update()

                    empDataSet.Reset()

                End If
            End If


        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        End Try

    End Sub

    Private Sub BtnModifyTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnModifyTabEmpInfo.Click

        'confirming the user wants to save and then using that result in a if statement
        Dim confirmation As String = MessageBox.Show("Are you sure you want to save this information?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        'making sure students arent trying to update records
        If userType = "admin" Or userType = "employee" Then

            Try

                Dim cb As New OleDb.OleDbCommandBuilder(employeeDataAdapter)

                'Checking that the ID textbox isnt empty
                If txtEmpID.Text <> "" And tempFlag Then

                    'making sure that the employee is searching for thier own details and not other employee details if the user is an employee
                    If userType = "employee" Then
                        If frmLogin.username <> empDataSet.Tables("employeeTable").Rows(0).Item("firstName") Then
                            MessageBox.Show("You can only look up your own information as a employee.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            empDataSet.Reset()
                            connection.Close()
                            Exit Sub
                        End If
                    End If

                    'if confirmed relevent textboxes are populated with database info
                    If confirmation = System.Windows.Forms.DialogResult.Yes Then

                        empDataSet.Tables("employeeTable").Rows(0).Item("employeeID") = txtEmpID.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("firstName") = txtEmpFirstName.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("surname") = txtEmpLastName.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("empTitle") = txtEmpTitle.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("cellphone") = txtEmpMobile.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("dateOfBirth") = dateEmpDOB.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("cellphone") = txtEmpPhone.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("email") = txtEmpEmail.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("localAddress") = txtEmpLocalAddress.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("permanentAddress") = txtEmpPermanentAddress.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("jobTitle") = txtEmpDesignation.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("employeeType") = txtEmpType.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("salary") = txtSalary.Text

                        employeeDataAdapter.Update(empDataSet, "employeeTable")

                    End If

                Else

                    MessageBox.Show("Please search for the employee details you would like to change using the employee ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If
            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

            End Try
        Else          ' user was a student 

            MessageBox.Show("Only admin and staff can update records.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnDeleteTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnDeleteTabEmpInfo.Click

        'checking if user is admin, only admin can delete records
        If userType = "admin" Then

            Try

                'setting up a query that will delete from our database it will use whatever is entered in the ID textbox as a parameter
                Dim deleteCMD As New OleDbCommand("DELETE * FROM employeeTable WHERE employeeID =?", connection)

                'inserting parameter into query
                deleteCMD.Parameters.AddWithValue("@employeeID", txtEmpID.Text)

                'starting connection to database
                connection.ConnectionString = provider & dataSource
                connection.Open()

                'executing the delete query
                deleteCMD.ExecuteNonQuery()

                MessageBox.Show("The record has successfully been deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try

            'updating the datagrid view in tab salary info
            updateDataGrid()

        Else       'the user is not admin and only admin can delete records

            MessageBox.Show("Only admin can delete records", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnSubmitTabEmpInfo_Click(sender As Object, e As EventArgs) Handles btnSubmitTabEmpInfo.Click

        'a check is made to see if the current user is admin. only admin can add new records to the database
        If userType = "admin" Then

            Try

                'setting up our query string before we access the database
                strSqlCMD = "SELECT * FROM employeeTable"
                connection.ConnectionString = provider & dataSource
                connection.Open()
                employeeDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
                employeeDataAdapter.Fill(empDataSet, "employeeTable")

                'setting up a variable that will hold a dialog result after confirming that the user would like to save the new record 
                Dim confirmation As DialogResult = MessageBox.Show("Are you sure you would like to save the new details you have just entered?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                'setting up a commandbuilder, it will allow the data adapter to save data to the database
                Dim cb As New OleDb.OleDbCommandBuilder(employeeDataAdapter)

                'this function checks to see that the record trying to be saved is not already in the database. if it is the sub is exited
                If checkForDuplicate("employee", txtEmpID.Text) Then
                    MessageBox.Show("That record is already in the data base", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    connection.Close()
                    Exit Sub
                End If

                'if the user confirms then we proceed to create a new row and then save it to the data set and then update the data base through the dataset and data adapter 
                If confirmation = DialogResult.Yes Then

                    'creating the new row for our dataset
                    Dim newRow As DataRow = empDataSet.Tables("employeeTable").NewRow()

                    'populating the new records feilds
                    newRow.Item("employeeID") = txtEmpID.Text
                    newRow.Item("firstName") = txtEmpFirstName.Text
                    newRow.Item("surname") = txtEmpLastName.Text
                    newRow.Item("empTitle") = txtEmpTitle.Text
                    newRow.Item("telephone") = txtEmpPhone.Text
                    newRow.Item("cellphone") = txtEmpMobile.Text
                    newRow.Item("email") = txtEmpEmail.Text
                    newRow.Item("dateOfBirth") = dateEmpDOB.Text
                    newRow.Item("jobTitle") = txtEmpDesignation.Text
                    newRow.Item("localAddress") = txtEmpLocalAddress.Text
                    newRow.Item("permanentAddress") = txtEmpPermanentAddress.Text
                    newRow.Item("employeeType") = txtEmpType.Text
                    newRow.Item("salary") = txtSalary.Text
                    newRow.Item("salaryPaid") = "Yes"

                    'adding the new row to the dataset and finally updating the database
                    empDataSet.Tables("employeeTable").Rows.Add(newRow)
                    employeeDataAdapter.Update(empDataSet, "employeeTable")

                    MessageBox.Show("You have successfully updated the database with a new record!", "New record added", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try

            'updating the data grid view in the salary info tab
            updateDataGrid()

        Else   'user was not admin and does not have rights to change the record

            MessageBox.Show("Only admin can create a new record.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnSearchTabSalaryInfo_Click(sender As Object, e As EventArgs) Handles btnSearchTabSalaryInfo.Click


        'all employees are allowed to see eachothers salaries / employee salary tab is blocked off to students
        Try

            'clearing texboxes for new information to populate
            txtAmountTabSalaryInfo.Clear()
            txtDueAmountTabSalaryInfo.Clear()

            'resetting the dataset incase it has data in
            empDataSet.Reset()

            'setting up the query to be used to access the database and get information needed
            strSqlCMD = "SELECT * FROM employeeTable WHERE [employeeID] ='" & cmbEmployeeIDTabSalaryInfo.Text & "'"
            connection.ConnectionString = provider & dataSource
            connection.Open()
            employeeDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
            employeeDataAdapter.Fill(empDataSet, "employeeTable")

            'populating the relevent feilds with new information in dataset
            txtSalaryIDTabSalaryInfo.Text = empDataSet.Tables("employeeTable").Rows(0).Item("employeeID")
            cmbEmployeeIDTabSalaryInfo.SelectedItem = empDataSet.Tables("employeeTable").Rows(0).Item("employeeID")
            txtAmountTabSalaryInfo.Text = empDataSet.Tables("employeeTable").Rows(0).Item("salary")
            txtDueAmountTabSalaryInfo.Text = empDataSet.Tables("employeeTable").Rows(0).Item("salary")
            cmbIsPaidTabSalaryInfo.Text = empDataSet.Tables("employeeTable").Rows(0).Item("salaryPaid")
            cmbMonthTabSalaryInfo.Text = dtpTabSalaryInfo.Text



        Catch ex As System.IndexOutOfRangeException    'user did not select a valid ID

            MessageBox.Show("That was an invalid employee ID.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        Finally

            connection.Close()

        End Try
    End Sub


    Private Sub BtnDeleteTabSalaryInfo_Click(sender As Object, e As EventArgs) Handles btnDeleteTabSalaryInfo.Click

        'checking if user is admin, only admin can delete records
        If userType = "admin" Then

            Try

                'checking that the user has selected a record to delete
                If cmbEmployeeIDTabSalaryInfo.Text <> "" Then

                    'setting up a query that will delete from our database it will use whatever is entered in the ID textbox as a parameter
                    Dim deleteCMD As New OleDbCommand("DELETE * FROM employeeTable WHERE employeeID =?", connection)

                    'inserting parameter into query
                    deleteCMD.Parameters.AddWithValue("@employeeID", cmbEmployeeIDTabSalaryInfo.SelectedItem)

                    'starting connection to database
                    connection.ConnectionString = provider & dataSource
                    connection.Open()

                    'executing the delete query
                    deleteCMD.ExecuteNonQuery()

                    MessageBox.Show("The record has successfully been deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try

            'updating the datagrid view in tab salary info
            updateDataGrid()

        Else    'the user is not admin and only admin can delete records

            MessageBox.Show("Only admin can delete a record.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub


    Private Sub cmbEmployeeIDTabSalaryInfoTabSalaryInfo_Click(sender As Object, e As EventArgs) Handles cmbEmployeeIDTabSalaryInfo.Click

        'on this click event we populate the ID combo box with the available ID's to select by setting up a variable that will hold the amount
        'of rows there are in the employee table and then use that to loop through the table and access the ID number and populate the combo box items
        'with ID numbers

        'resetting the dataset so we can populate it again incase any changes have been made
        empDataSet.Reset()

        'setting up the variable to hold the row count
        Dim rows As Integer

        'setting up the query string to be used when we make our connection
        strSqlCMD = "SELECT * FROM employeeTable"

        'declaring the connection string 
        connection.ConnectionString = provider & dataSource

        'opening the connection
        connection.Open()

        'setting up our data adapter object with new query
        employeeDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)

        'using the data adapter to fill our dataset with info from the database
        employeeDataAdapter.Fill(empDataSet, "employeeTable")

        'assigning the row count to rows variable
        rows = empDataSet.Tables("employeeTable").Rows.Count()

        'closing the connection
        connection.Close()

        'clearing the combo box because we dont want duplicate ID numbers in the combo box
        cmbEmployeeIDTabSalaryInfo.Items.Clear()

        'iterating through the table and getting each available ID number
        For i As Integer = 0 To rows - 1
            'adding the ID number to the combobox
            cmbEmployeeIDTabSalaryInfo.Items.Add(empDataSet.Tables("employeeTable").Rows(i).Item("employeeID"))
        Next
    End Sub

    Private Sub BtnSaveTabSalaryInfo_Click(sender As Object, e As EventArgs) Handles btnSaveTabSalaryInfo.Click

        'making sure students and employees arent trying to update records
        If userType = "admin" Then

            'confirming the user wants to save and then using that result in a if statement
            Dim confirmation As String = MessageBox.Show("Are you sure you want to save this information?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            Try

                Dim cb As New OleDb.OleDbCommandBuilder(employeeDataAdapter)

                'Checking that the ID textbox isnt empty
                If cmbEmployeeIDTabSalaryInfo.Text <> "" Then

                    'if confirmed relevent textboxes are populated with database info
                    If confirmation = System.Windows.Forms.DialogResult.Yes Then

                        empDataSet.Tables("employeeTable").Rows(0).Item("salary") = txtAmountTabSalaryInfo.Text
                        empDataSet.Tables("employeeTable").Rows(0).Item("salaryPaid") = cmbIsPaidTabSalaryInfo.Text

                        'using the data adapter to update the database
                        employeeDataAdapter.Update(empDataSet, "employeeTable")

                    End If

                Else   'user did not select an ID to search for a record

                    MessageBox.Show("Please search for the employee details you would like to change using the employee ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If
            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

                'updating the datagrid view in tab salary info
                updateDataGrid()

            End Try


        Else   'user was not admin

            MessageBox.Show("Only admin can change information from the records.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub updateDataGrid()

        'the purpose of this function is to update our datagrid view in tab salary info

        Try

            'the dataset gets reset to allow it to be repopulated
            empDataSet.Reset()

            'setting up our query string 
            strSqlCMD = "SELECT * FROM employeeTable"

            'setting up our connection string
            connection.ConnectionString = provider & dataSource

            'opening the connection
            connection.Open()

            'setting up our data adapter with query and connection object
            employeeDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)

            'using the data adapter to fill the dataset with database information
            employeeDataAdapter.Fill(empDataSet, "employeeTable")

            'assigning the data source property of the data grid with data from the data set
            dgvTabSalaryInfo.DataSource = empDataSet.Tables("employeeTable")

        Catch ex As System.IndexOutOfRangeException

            MessageBox.Show("Something went wrong while trying to update the datagrid table.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        Finally

            'finally closing the connection
            connection.Close()

        End Try

    End Sub

    Private Sub BtnFindTabFeeInfo_Click(sender As Object, e As EventArgs) Handles btnFindTabFeeInfo.Click

        'checking if the user is admin or student, if admin user has more privelages
        If userType = "admin" Or userType = "student" Then

            Try

                'making sure the user did enter a ID in the ID textbox
                If txtStudentIDtabFeeInfo.Text <> "" And userType = "admin" Then

                    txtFeesID.Clear()
                    dtpFeesDateTabFeeInfo.Refresh()
                    txtFeesAmount.Clear()

                    'resetting the dataset for new information to be added
                    studentDataSet.Reset()

                End If

                'checking to make sure the ID textbox isnt clear 
                If txtStudentIDtabFeeInfo.Text <> "" Then

                    'setting up our query that looks for a specific ID in the database and starting connection
                    strSqlCMD = "SELECT * FROM studentTable WHERE [studentID] ='" & txtStudentIDtabFeeInfo.Text & "'"
                    connection.ConnectionString = provider & dataSource
                    connection.Open()
                    studentDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)
                    studentDataAdapter.Fill(studentDataSet, "studentTable")

                    'checking if the user is employee. employees can only look up their own information
                    If userType = "student" Then
                        If frmLogin.username <> studentDataSet.Tables("studentTable").Rows(0).Item("firstName") Then
                            MessageBox.Show("You can only look up your own information as a student.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            empDataSet.Reset()
                            connection.Close()
                            Exit Sub
                        End If
                    End If

                    'once all checks have been made the relevent text boxes get populated with information from the dataset holding 
                    'information from the database
                    txtFeesID.Text = studentDataSet.Tables("studentTable").Rows(0).Item("studentID")
                    dtpFeesDateTabFeeInfo.Text = studentDataSet.Tables("studentTable").Rows(0).Item("feesDate")
                    cmbFeesMonth.Text = studentDataSet.Tables("studentTable").Rows(0).Item("feesMonth")
                    cmbFeeYear.Text = studentDataSet.Tables("studentTable").Rows(0).Item("feesYear")
                    cmbPaymentType.Text = studentDataSet.Tables("studentTable").Rows(0).Item("paymentType")
                    txtFeesAmount.Text = studentDataSet.Tables("studentTable").Rows(0).Item("feesAmount")


                Else  'the ID textbox was empty when the button was clicked

                    MessageBox.Show("Please enter the student ID for the student details you are looking for.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If

            Catch ex As System.IndexOutOfRangeException    'an invalid or non-existant ID was used in the ID textbox

                MessageBox.Show("That was an invalid student ID.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch ex As Exception 'catching any other exceptions

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

                connection.Close()

            End Try

        Else     'Employee tried to lookup student fees which is not allowed

            MessageBox.Show("Employees dont have access to this functionality.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub BtnModifyTabFeeInfo_Click(sender As Object, e As EventArgs) Handles btnModifyTabFeeInfo.Click

        'confirming the user wants to save and then using that result in a if statement
        Dim confirmation As String = MessageBox.Show("Are you sure you want to save this information?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        'making sure employees arent trying to update records
        If userType = "admin" Or userType = "student" Then

            Try

                Dim cb As New OleDb.OleDbCommandBuilder(studentDataAdapter)

                'Checking that the ID textbox isnt empty
                If txtStudentIDtabFeeInfo.Text <> "" Then

                    'if confirmed relevent feilds are populated with database info
                    If confirmation = System.Windows.Forms.DialogResult.Yes Then

                        studentDataSet.Tables("studentTable").Rows(0).Item("paymentType") = cmbPaymentType.Text
                        studentDataSet.Tables("studentTable").Rows(0).Item("feesDate") = dtpFeesDateTabFeeInfo.Text
                        studentDataSet.Tables("studentTable").Rows(0).Item("feesMonth") = cmbFeesMonth.Text
                        studentDataSet.Tables("studentTable").Rows(0).Item("feesYear") = cmbFeeYear.Text
                        studentDataSet.Tables("studentTable").Rows(0).Item("feesAmount") = txtFeesAmount.Text

                        'checking if admin. only admin can change bellow info
                        If userType = "admin" Then
                            studentDataSet.Tables("studentTable").Rows(0).Item("feesID") = txtStudentIDtabFeeInfo.Text
                            studentDataSet.Tables("studentTable").Rows(0).Item("studentID") = txtStudentIDtabFeeInfo.Text
                        End If

                        'updating the database with new information
                        studentDataAdapter.Update(studentDataSet, "studentTable")
                    End If

                Else

                    MessageBox.Show("Please search for the student details you would like to change using the student ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

            End Try

        Else
            'user was an employee
            MessageBox.Show("only admin and students can access this functionality", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub BtnDeleteTabFeeInfo_Click(sender As Object, e As EventArgs) Handles btnDeleteTabFeeInfo.Click

        'checking if user is admin, only admin can delete records
        If userType = "admin" Then

            Try

                'setting up a query that will delete from our database it will use whatever is entered in the ID textbox as a parameter
                Dim deleteCMD As New OleDbCommand("DELETE * FROM studentTable WHERE studentID =?", connection)

                'inserting parameter into query
                deleteCMD.Parameters.AddWithValue("@studentID", txtStudentIDtabFeeInfo.Text)

                'starting connection to database
                connection.ConnectionString = provider & dataSource
                connection.Open()

                'executing the delete query
                deleteCMD.ExecuteNonQuery()

                MessageBox.Show("The record has successfully been deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                'closing the connection
                connection.Close()

            End Try

        Else
            ' the user was not admin 
            MessageBox.Show("Only admin can delete records.", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub BtnClearTabFeeInfo_Click(sender As Object, e As EventArgs) Handles btnClearTabFeeInfo.Click

        'checking if user is admin, only admin can clear fee info
        If userType = "admin" Then
            txtFeesID.Clear()
            dtpFeesDateTabFeeInfo.Refresh()
            txtFeesAmount.Clear()
        Else
            'user was not admin
            MessageBox.Show("You dont have access to this functionality", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub BtnFindTabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnFindTabCourseDetail.Click

        'all users are allowed to search for course details
        Try

            'making sure the user did enter a ID in the ID textbox
            If txtCourseID.Text <> "" Then

                'clearing the text boxes for new information to populate
                txtCourseTitle.Clear()
                txtCourseName.Clear()
                txtCourseCode.Clear()
                txtCourseFee.Clear()
                txtCourseDuration.Clear()

                'resetting the dataset for new information to be added
                courseDataSet.Reset()

            End If

            'making sure the user did enter a ID in the ID textbox
            If txtCourseID.Text <> "" Then

                'setting up our query that looks for a specific ID in the database and starting connection
                strSqlCMD = "SELECT * FROM courseTable WHERE [courseID] ='" & txtCourseID.Text & "'"
                connection.ConnectionString = provider & dataSource
                connection.Open()
                courseDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)

                'filling our dataset with information from the database
                courseDataAdapter.Fill(courseDataSet, "courseTable")

                'filling the relevent textboxes with information from the dataset
                txtCourseID.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseID")
                txtCourseTitle.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseTitle")
                txtCourseName.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseName")
                txtCourseCode.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseCode")
                txtCourseFee.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseFee")
                txtCourseDuration.Text = courseDataSet.Tables("courseTable").Rows(0).Item("CourseDuration")


            Else
                'no ID provided for the search
                MessageBox.Show("Please enter the course ID for the course details you are looking for.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As System.IndexOutOfRangeException

            'invalid ID was used for the search
            MessageBox.Show("That was an invalid course ID.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show("Exception: " & ex.ToString)

        Finally

            connection.Close()

        End Try
    End Sub

    Private Sub BtnUpdateTabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnUpdateTabCourseDetail.Click

        'confirming the user wants to save and then using that result in a if statement
        Dim confirmation As String = MessageBox.Show("Are you sure you want to save this information?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        'making sure only admin are accessing this feature
        If userType = "admin" Then

            Try

                'declaring a command builder which will assist in updating our database
                Dim cb As New OleDb.OleDbCommandBuilder(courseDataAdapter)

                'Checking that the ID textbox isnt empty
                If txtCourseID.Text <> "" Then

                    'if confirmed relevent feilds are populated with database info
                    If confirmation = System.Windows.Forms.DialogResult.Yes Then

                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseID") = txtCourseID.Text
                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseTitle") = txtCourseTitle.Text
                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseName") = txtCourseName.Text
                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseCode") = txtCourseCode.Text
                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseFee") = txtCourseFee.Text
                        courseDataSet.Tables("courseTable").Rows(0).Item("CourseDuration") = txtCourseDuration.Text

                        'updating the database with new information
                        courseDataAdapter.Update(courseDataSet, "courseTable")
                    End If

                Else
                    'user didnt enter an ID for the search
                    MessageBox.Show("Please search for the course details you would like to change using the course ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Catch ex As Exception

                MessageBox.Show("Exception: " & ex.ToString)

            Finally

            End Try
        Else
            'user is not admin
            MessageBox.Show("You dont have access to this functionality", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub BtnDeleteTabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnDeleteTabCourseDetail.Click

        'checking if user is admin, only admin can delete records
        If userType = "admin" Then

            Try

                'setting up a query that will delete from our database it will use whatever is entered in the ID textbox as a parameter
                Dim deleteCMD As New OleDbCommand("DELETE * FROM courseTable WHERE courseID =?", connection)

                'inserting parameter into query
                deleteCMD.Parameters.AddWithValue("@courseID", txtCourseID.Text)

                'starting connection to database
                connection.ConnectionString = provider & dataSource
                connection.Open()

                'executing the delete query
                deleteCMD.ExecuteNonQuery()

                MessageBox.Show("The record has successfully been deleted", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                'closing the connection
                connection.Close()

            End Try

        Else
            'user is not admin
            MessageBox.Show("You dont have access to this functionality", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub BtnSubmitTabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnSubmitTabCourseDetail.Click

        'checking if the user is admin. Only admin can add new records
        If userType = "admin" Then

            Try
                'setting up the query that will be used for our connection
                strSqlCMD = "SELECT * FROM courseTable"

                'starting the connection
                connection.ConnectionString = provider & dataSource
                connection.Open()
                courseDataAdapter = New OleDbDataAdapter(strSqlCMD, connection)

                'filling the dataset with data from our database
                courseDataAdapter.Fill(courseDataSet, "courseTable")

                'confirming the user wants to save and then using that result in a if statement
                Dim confirmation As DialogResult = MessageBox.Show("Are you sure you would like to save the new details you have just entered?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                'declaring a command builder object that will assist in updating our database 
                Dim cb As New OleDb.OleDbCommandBuilder(courseDataAdapter)

                'the bellow function assists in checking the database if the record already exists.
                If checkForDuplicate("course", txtCourseID.Text) Then
                    MessageBox.Show("That record is already in the data base", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    connection.Close()
                    Exit Sub
                End If

                'setting up a variable that will hold a dialog result after confirming that the user would like to save the new record 
                If confirmation = DialogResult.Yes Then

                    'creating the new row for our dataset
                    Dim newRow As DataRow = courseDataSet.Tables("courseTable").NewRow()

                    'populating the new records feilds
                    newRow.Item("CourseID") = txtCourseID.Text
                    newRow.Item("CourseTitle") = txtCourseTitle.Text
                    newRow.Item("CourseName") = txtCourseName.Text
                    newRow.Item("CourseCode") = txtCourseCode.Text
                    newRow.Item("CourseFee") = txtCourseFee.Text
                    newRow.Item("CourseDuration") = txtCourseDuration.Text

                    'adding the new row to the dataset and finally updating the database
                    courseDataSet.Tables("courseTable").Rows.Add(newRow)
                    courseDataAdapter.Update(courseDataSet, "courseTable")

                    MessageBox.Show("You have successfully updated the database with a new record!", "New record added", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If

            Catch ex As Exception

                MessageBox.Show(ex.ToString)

            Finally

                connection.Close()

            End Try
        Else
            'user is not admin and doesnt have rights to add new records
            MessageBox.Show("Only admin can add records", "access denied", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub BtnCleartabCourseDetail_Click(sender As Object, e As EventArgs) Handles btnCleartabCourseDetail.Click

        'checking that the ID text box isnt empty and if the user is admin
        If txtCourseID.Text <> "" And userType = "admin" Then

            txtCourseID.Clear()
            txtCourseTitle.Clear()
            txtCourseName.Clear()
            txtCourseCode.Clear()
            txtCourseFee.Clear()
            txtCourseDuration.Clear()

            'resetting the database
            courseDataSet.Reset()

        End If

    End Sub

    'the bellow helper function is to assist the program in checking for a ID in the relevent database based on which type of dataset we use in the 
    'function arguments and the ID we are looking for. This function is used in the submit click events.
    Function checkForDuplicate(Tabletype As String, ID As String)

        'setting up a variable to hold the value of how many rows there are in our table
        Dim rows As Integer

        'switch statement based on Tabletype in function arguments
        Select Case Tabletype

            ' if table type is student we will iterate through the studentTable and check if the ID is present, if so then will return True
            Case = "student"
                rows = studentDataSet.Tables("studentTable").Rows.Count()
                For i As Integer = 0 To rows - 1
                    If ID = studentDataSet.Tables("studentTable").Rows(i).Item("studentID") Then
                        Return True
                    End If
                Next

            ' if table type is employee we will iterate through the employeeTable and check if the ID is present, if so then will return True
            Case = "employee"
                rows = empDataSet.Tables("employeeTable").Rows.Count()
                For i As Integer = 0 To rows - 1
                    If ID = empDataSet.Tables("employeeTable").Rows(i).Item("employeeID") Then
                        Return True
                    End If
                Next

            ' if table type is course we will iterate through the courseTable and check if the ID is present, if so then will return True
            Case = "course"
                rows = courseDataSet.Tables("courseTable").Rows.Count()
                For i As Integer = 0 To rows - 1
                    If ID = courseDataSet.Tables("courseTable").Rows(i).Item("courseID") Then
                        Return True
                    End If
                Next
        End Select

        'if the case statement above doesnt return true, there is no duplicate, return false
        Return False


    End Function


End Class
