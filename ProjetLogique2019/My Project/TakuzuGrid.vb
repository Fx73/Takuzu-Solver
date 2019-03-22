Imports System.IO

Public Class TakuzuGrid
    Dim n
    Dim grid() As Integer
    Public isinit As Boolean

    Private Sub TakuzuGrid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Definition de la grille de debut
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Grille.txt") Then
            ComboBoxN_SelectedIndexChanged()
            Exit Sub
        End If

        n = My.Forms.ProjetLogique2019.TailledeGrille
        If (n = -1) Then
            ComboBoxN_SelectedIndexChanged()
            MsgBox("Une grille non valide a été trouvé. Elle sera remplacée à la validation", vbInformation)
            Exit Sub
        End If
        ComboBoxN.Text = n.ToString + "x" + n.ToString
        ReDim grid(n * n - 1)

        Dim reader = New IO.StreamReader(Application.StartupPath() + "\Takuzu_Grille.txt")
        Dim oldgrid = (reader.ReadToEnd).Replace(vbCr, "").Replace(vbLf, "")
        reader.Close()

        For i = 0 To n * n - 1
            If Val(oldgrid(i)) < 0 Or Val(oldgrid(i)) > 2 Then
                ComboBoxN_SelectedIndexChanged()
                MsgBox("Une grille non valide a été trouvé. Elle sera remplacée à la validation", vbInformation)
                Exit Sub
            End If
            grid(i) = Val(oldgrid(i))
        Next

        CreateButtonGrid()
    End Sub

    Public Sub Loadcomplete()
        'Definition de la grille de fin
        Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Solved_Dimacs.txt")
        Dim resvar = Split(reader.ReadToEnd.Replace(vbCr, "").Replace(vbLf, ""), " ")
        reader.Close()

        If n <> My.Forms.ProjetLogique2019.n Then
            MsgBox("La taille de la grille a changée. Il est possible qu'il y ait des erreurs", vbInformation)
            n = My.Forms.ProjetLogique2019.n
        End If
        ComboBoxN.Text = n.ToString + "x" + n.ToString
        ReDim grid(n * n - 1)
        Try
            For i = 0 To n * n - 1
                If resvar(i)(0) = "-" Then
                    grid(i) = 0
                Else
                    grid(i) = 1
                End If
            Next
        Catch
            My.Forms.ProjetLogique2019.TextBoxMain.AppendText(vbCrLf + "Attention : La taille de la sortie actuelle ne correspond pas a celle de la grille en entrée")
        End Try
        CreateButtonGrid()
    End Sub

    Private Function CreateButtonGrid()
        PanelButtons.Controls.Clear()
        For i As Integer = 0 To n - 1
            For j As Integer = 0 To n - 1
                Dim mybutton As New Button
                With mybutton
                    .Size = New Drawing.Size(30, 30)
                    .Location = New Point(35 * j, 35 * i)
                    .Name = i * n + j
                    .Font = New Font(.Font, FontStyle.Bold)
                    If grid(i * n + j) = 0 Then
                        .Text = "0"
                    End If
                    If grid(i * n + j) = 1 Then
                        .Text = "1"
                    End If
                End With
                AddHandler mybutton.Click, AddressOf AnyButtonClick
                PanelButtons.Controls.Add(mybutton)
            Next
        Next
    End Function

    Private Sub ComboBoxN_SelectedIndexChanged() Handles ComboBoxN.SelectedIndexChanged
        If ComboBoxN.Text(1).ToString = "x" Then
            n = (ComboBoxN.Text(0).ToString)
        Else
            n = (ComboBoxN.Text(0).ToString + ComboBoxN.Text(1).ToString)
        End If
        ReDim grid(n * n - 1)
        For i = 0 To n * n - 1
            grid(i) = 2
        Next
        CreateButtonGrid()
    End Sub

    Private Sub AnyButtonClick(sender As Object, e As EventArgs) 'Handles tout les boutons du panel
        Select Case grid(Convert.ToInt32(sender.name))
            Case 0
                grid(sender.name) = 1
                sender.text = "1"
            Case 1
                grid(sender.name) = 2
                sender.text = ""
            Case 2
                grid(sender.name) = 0
                sender.text = "0"
        End Select
    End Sub



    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If isinit Then
            My.Forms.ProjetLogique2019.TakuzuCreate(grid)
        Else
            My.Forms.ProjetLogique2019.TakuzuCreateResolved(grid)
        End If
        Me.Hide()
    End Sub
End Class