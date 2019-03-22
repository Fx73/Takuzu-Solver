Imports System.Text
Imports System.IO

Public Class ProjetLogique2019
    Public n As Integer = TailledeGrille() 'taille de grille
    Dim pageTakuzuini As TakuzuGrid = New TakuzuGrid With {.isinit = True}
    Dim pageTakuzuend As TakuzuGrid = New TakuzuGrid With {.isinit = False}

#Region "Initialisation de l'app"
    Private Sub Projet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LabelBienvenue.Text = "Bienvenue sur notre projet de Logique : Le Takuzu"
        TextBoxMain.Text = "- Tout les fichiers de sortie sont créés dans le repertoire actuel de l'executable"
        n = TailledeGrille()
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Conjonctive.txt") Then
            ButtonOCo.ForeColor = Color.DarkGreen
        End If
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
            ButtonODi.ForeColor = Color.DarkGreen
        End If
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
            ButtonO3s.ForeColor = Color.DarkGreen
        End If

    End Sub
    Private Sub ButtonQuit_Click(sender As Object, e As EventArgs) Handles ButtonQuit.Click
        Me.Close()
    End Sub
#End Region

#Region "Boutons descriptifs"
    Private Sub ButtonPreRap_Click(sender As Object, e As EventArgs) Handles ButtonPreRap.Click
        Try
            My.Computer.FileSystem.WriteAllBytes(Path.GetTempPath & "\Prerendutemp.docx", My.Resources.Pré_rendu_INF402, False)
            Process.Start(Path.GetTempPath & "\Prerendutemp.docx")
            TextBoxMain.AppendText(vbCrLf + "Pre-rendu chargé et ouvert correctement")
        Catch ex As Exception
            TextBoxMain.AppendText(vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub Buttonregles_Click(sender As Object, e As EventArgs) Handles Buttonregles.Click
        TextBoxMain.AppendText(vbCrLf + "Règles du Takuzu :
 ● autant de 1 et de 0 sur chaque ligne et sur chaque colonne
 ● pas plus de 2 chiffres identiques côte à côte
 ● 2 lignes ou 2 colonnes ne peuvent être identiques")
    End Sub

    Private Sub ButtonRegleConj_Click(sender As Object, e As EventArgs) Handles ButtonRegleConj.Click
        TextBoxMain.AppendText(vbCrLf + "En fait ca rend pas fou l'affichage mathematique, je mettrai peut etre une image")
    End Sub

    Private Sub ButtonOCo_Click(sender As Object, e As EventArgs) Handles ButtonOCo.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Conjonctive.txt") Then
            Try
                Process.Start(Application.StartupPath() + "\Takuzu_Conjonctive.txt")
                TextBoxMain.AppendText(vbCrLf + "Format conjonctive ouvert")
            Catch ex As Exception
                TextBoxMain.AppendText(vbCrLf + ex.Message)
            End Try
        End If
    End Sub

    Private Sub ButtonODi_Click(sender As Object, e As EventArgs) Handles ButtonODi.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
            Try
                Process.Start(Application.StartupPath() + "\Takuzu_Dimacs.txt")
                TextBoxMain.AppendText(vbCrLf + "Format Dimacs n-Sat ouvert")
            Catch ex As Exception
                TextBoxMain.AppendText(vbCrLf + ex.Message)
            End Try
        End If
    End Sub

    Private Sub ButtonO3s_Click(sender As Object, e As EventArgs) Handles ButtonO3s.Click
        If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
            Try
                Process.Start(Application.StartupPath() + "\Takuzu_3Sat.txt")
                TextBoxMain.AppendText(vbCrLf + "Format 3Sat ouvert")
            Catch ex As Exception
                TextBoxMain.AppendText(vbCrLf + ex.Message)
            End Try
        End If
    End Sub
#End Region

#Region "Grille de Takuzu"
    Private Sub ButtonTakuzuCreate_Click(sender As Object, e As EventArgs) Handles ButtonTakuzuCreate.Click
        pageTakuzuini.Show()
    End Sub

    Public Sub TakuzuCreate(grilletakuzu As Integer())
        Try
            Dim writer = CreateTextFile("Takuzu_Grille.txt")
            For i = 0 To grilletakuzu.Length - 1
                writer.Write(grilletakuzu(i).ToString)
                If (i Mod Math.Sqrt(grilletakuzu.Length) = Math.Sqrt(grilletakuzu.Length) - 1) Then
                    writer.Write(vbCrLf)
                End If
            Next
            writer.Flush()
            writer.Close()
            n = TailledeGrille()
            TextBoxMain.AppendText(vbCrLf + " - Les 2 représentent des blancs" + vbCrLf + " - La taille de la grille a été defini sur " + n.ToString + "x" + n.ToString)
        Catch ex As Exception
            TextBoxMain.AppendText(vbCrLf + ex.Message)
        End Try
        ButtonTakuzuCreate.ForeColor = Color.DarkGreen
        Buttoncreerconjonctive.ForeColor = Color.Brown
        Buttondimacs.ForeColor = Color.Brown
        Button3sat.ForeColor = Color.Brown
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown
    End Sub
#End Region

#Region "Forme conjonctive"
    Private Sub Buttoncreerconjonctive_Click(sender As Object, e As EventArgs) Handles Buttoncreerconjonctive.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Grille.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord une grille initiale de Takuzu")
            Exit Sub
        End If

        Using writer = CreateTextFile("Takuzu_Conjonctive.txt")

            TextBoxMain.AppendText(vbCrLf + " - Nombre supposé de variables (NxN) : " + (n * n).ToString)
            Dim nbclause = 0

            'Preremplissage
            TextBoxMain.AppendText(vbCrLf + " - - Preremplissage ")
            Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Grille.txt")
            Dim i = 0, j = 0
            While Not reader.EndOfStream
                Dim s = reader.ReadLine()
                For i = 0 To s.Length - 1
                    If s(i) = "0" Then
                        writer.WriteLine("-({0},{1}) .", j, i)
                        TextBoxMain.AppendText(".")
                    ElseIf s(i) = "1" Then
                        writer.WriteLine("({0},{1}) .", j, i)
                        TextBoxMain.AppendText(".")
                    ElseIf s(i) <> "2" Then
                        MsgBox("Attention, la grille de départ est érroné")
                    End If
                Next
                j += 1
            End While
            reader.Close()

            'regle 1
            TextBoxMain.AppendText(vbCrLf + " - - Ecriture de la regle 1 ")
            For i = 0 To n - 1
                For j = 0 To n - 3
                    writer.Write("(({0},{1}) + ({0},{2}) + ({0},{3})) ." + vbCrLf + "(-({0},{1}) + -({0},{2}) + -({0},{3}))." + vbCrLf + "(({1},{0}) + ({2},{0}) + ({3},{0})) ." + vbCrLf + "(-({1},{0}) + -({2},{0}) + -({3},{0})) ." + vbCrLf, i, j, j + 1, j + 2)
                    nbclause += 4
                Next
                TextBoxMain.AppendText(".")
            Next

            'regle 2
            TextBoxMain.AppendText(vbCrLf + " - - Ecriture de la regle 2 ")
            Dim tab(n) As Boolean
            For i1 = 0 To n - 2
                For i2 = i1 + 1 To n - 1
                    For k = 0 To Math.Pow(2, n) - 1
                        For j = 0 To n - 2
                            If tab(j) Then
                                writer.Write("({0},{1}) + ({0},{2}) + ", j, i1, i2)
                            Else
                                writer.Write("-({0},{1}) + -({0},{2}) + ", j, i1, i2)
                            End If
                        Next
                        If tab(n - 1) Then
                            writer.Write("({0},{1}) + ({0},{2}) ." + vbCrLf, n - 1, i1, i2)
                        Else
                            writer.Write("-({0},{1}) + -({0},{2}) ." + vbCrLf, n - 1, i1, i2)
                        End If
                        nbclause += 1
                        Increment(tab)
                    Next
                Next
                TextBoxMain.AppendText(".")
            Next
            ReDim tab(n)
            For i1 = 0 To n - 2
                For i2 = i1 + 1 To n - 1
                    For k = 0 To Math.Pow(2, n) - 1
                        For j = 0 To n - 2
                            If tab(j) Then
                                writer.Write("({1},{0}) + ({2},{0}) + ", j, i1, i2)
                            Else
                                writer.Write("-({1},{0}) + -({2},{0}) + ", j, i1, i2)
                            End If
                        Next
                        If tab(n - 1) Then
                            writer.Write("({1},{0}) + ({2},{0}) ." + vbCrLf, n - 1, i1, i2)
                        Else
                            writer.Write("-({1},{0}) + -({2},{0}) ." + vbCrLf, n - 1, i1, i2)
                        End If
                        nbclause += 1
                        Increment(tab)
                    Next
                Next
                TextBoxMain.AppendText(".")
            Next

            'regle 3 a faire
            writer.Write("(0,0) +  -(0,0)")

            TextBoxMain.AppendText(vbCrLf + " - Nombre de clauses créées: " + nbclause.ToString)
        End Using

        ButtonOCo.ForeColor = Color.DarkGreen
        Buttoncreerconjonctive.ForeColor = Color.DarkGreen
        Buttondimacs.ForeColor = Color.Brown
        Button3sat.ForeColor = Color.Brown
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown
    End Sub

    Private Function Increment(ByRef tab() As Boolean)
        For i = 0 To tab.Length - 1
            If (tab(i) = False) Then
                tab(i) = True
                Exit Function
            End If
            tab(i) = False
        Next
    End Function

#End Region

#Region "Format Dimacs"
    Private Sub Buttondimacs_Click(sender As Object, e As EventArgs) Handles Buttondimacs.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Conjonctive.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier de logique conjonctive")
            Exit Sub
        End If

        Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Conjonctive.txt")
        Dim writer = CreateTextFile("Takuzu_Dimacs.txt")
        If writer Is Nothing Then Exit Sub

        'Ecriture de l'en-tete
        Dim m = 1
        Dim c As Char = Convert.ToChar(reader.Read)
        Do While Not reader.EndOfStream
            If (c = ".") Then
                m += 1
            End If
            c = Convert.ToChar(reader.Read())
        Loop
        writer.WriteLine("p cnf {0} {1}", n * n, m)
        reader.Close()
        reader = New StreamReader(Application.StartupPath() + "\Takuzu_Conjonctive.txt")

        'Ecriture du corps
        c = Convert.ToChar(reader.Read)
        Dim cext = ""
        Dim first As Boolean
        Do While Not reader.EndOfStream
            If c >= "0" And c <= "9" Then
                cext += c
            ElseIf c = "," Then
                If cext.Length = 2 Then
                    first = True
                Else
                    first = False
                End If
            ElseIf c = "+" Then
                writer.Write(BaseN(cext, first) + " ")
                cext = ""
            ElseIf c = "." Then
                writer.Write(BaseN(cext, first) + " 0" + vbCrLf)
                cext = ""
            ElseIf c = "-" Then
                writer.Write("-")
            End If
            c = Convert.ToChar(reader.Read)
        Loop
        writer.Write(BaseN(cext, first) + " 0")
        reader.Close()
        writer.Close()

        'Verification du nombre de variable (NxN)
        Try
            reader = New StreamReader(Application.StartupPath() + "\Takuzu_Dimacs.txt")
            reader.ReadLine()
            Dim nbvar = 0
            Dim tabvar(n * n * 2) As Boolean
            Dim actvar = ""
            While Not reader.EndOfStream
                c = Chr(reader.Read)
                If c >= "0" And c <= "9" Then
                    actvar += c
                ElseIf actvar <> "" And Not tabvar(Val(actvar)) Then
                    tabvar(Val(actvar)) = True
                    actvar = ""
                Else
                    actvar = ""
                End If
            End While
            reader.Close()

            For i = 1 To n * n * 2 - 1
                If tabvar(i) Then
                    nbvar += 1
                End If
            Next

            If nbvar = n * n Then
                TextBoxMain.AppendText(vbCrLf + " - Nombre de variable vérifiées : ok : " + nbvar.ToString)
            Else
                TextBoxMain.AppendText(vbCrLf + " - Nombre de variable vérifié : erreur : " + nbvar.ToString)
                For i = 1 To n * n
                    If Not tabvar(i) Then
                        TextBoxMain.AppendText(vbCrLf + " * La variable " + i.ToString + " est manquante")
                    End If
                Next
                For i = n * n + 1 To n * n * 2 - 2
                    If tabvar(i) Then
                        TextBoxMain.AppendText(vbCrLf + " * La variable " + i.ToString + " est de trop")
                    End If
                Next
            End If
        Catch ex As Exception
            TextBoxMain.AppendText("Erreur lors de la verification des variables du fichier Dimacs" + vbCrLf + ex.Message)
        End Try

        ButtonODi.ForeColor = Color.DarkGreen
        Buttondimacs.ForeColor = Color.DarkGreen
        Button3sat.ForeColor = Color.Brown
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown

    End Sub
#End Region

#Region "Format 3-Sat"
    Private Sub Button3sat_Click(sender As Object, e As EventArgs) Handles Button3sat.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs n-Sat")
            Exit Sub
        End If

        Dim writer = CreateTextFile("Takuzu_3Sat.txt")
        If writer Is Nothing Then Exit Sub

        'Calcul du nouveau nombre de variable et de clause et entete
        Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Dimacs.txt")
        Dim s = Split(reader.ReadLine, " ")
        Dim nbvar = s(2)
        Dim nbclause = s(3)
        While Not reader.EndOfStream
            s = Split(reader.ReadLine, " ")
            Select Case (s.Length - 1)
                Case 0
                    'Erreur
                Case 1
                    nbvar += 2
                    nbclause += 3
                Case 2
                    nbvar += 1
                    nbclause += 1
                Case 3
                    'nothing
                Case Else
                    nbvar += s.Length - 4
                    nbclause += s.Length - 4
            End Select
        End While

        reader.Close()
        TextBoxMain.AppendText(vbCrLf + " - Calcul pour 3Sat :" + vbCrLf + " - - nombre de variables : " + nbvar.ToString + vbCrLf + " - - nombre de clause :" + nbclause.ToString)
        writer.WriteLine("p cnf {0} {1}", nbvar, nbclause)

        'Ecriture du corps
        reader = New StreamReader(Application.StartupPath() + "\Takuzu_Dimacs.txt")
        s = Split(reader.ReadLine, " ")
        Dim nbv = s(2)
        While Not reader.EndOfStream
            s = Split(reader.ReadLine, " ")
            Select Case (s.Length - 1)
                Case 0
                    'Erreur, ne pas recopier
                Case 1
                    writer.WriteLine("{0} {1} {2} 0", s(0), nbv + 1, nbv + 2)
                    writer.WriteLine("{0} -{1} {2} 0", s(0), nbv + 1, nbv + 2)
                    writer.WriteLine("{0} {1} -{2} 0", s(0), nbv + 1, nbv + 2)
                    writer.WriteLine("{0} -{1} -{2} 0", s(0), nbv + 1, nbv + 2)
                    nbv += 2
                Case 2
                    writer.WriteLine("{0} {1} {2} 0", s(0), s(1), nbv + 1)
                    writer.WriteLine("{0} {1} -{2} 0", s(0), s(1), nbv + 1)
                    nbv += 1
                Case 3
                    writer.WriteLine("{0} {1} {2} 0", s(0), s(1), s(2))
                Case Else
                    writer.WriteLine("{0} {1} {2} 0", s(0), s(1), nbv + 1)
                    For i = 2 To (s.Length - 4)
                        writer.WriteLine("-{0} {1} {2} 0", nbv + 1, s(i), nbv + 2)
                        nbv += 1
                    Next
                    writer.WriteLine("-{0} {1} {2} 0", nbv + 1, s(s.Length - 3), s(s.Length - 2))
                    nbv += 1
            End Select
        End While
        reader.Close()
        writer.Close()

        'Ouverture du fichier
        ButtonO3s.ForeColor = Color.DarkGreen
        Button3sat.ForeColor = Color.DarkGreen
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown

    End Sub
#End Region

#Region "Solver online"
    Private Sub ButtonSatSolveOnline_Click(sender As Object, e As EventArgs) Handles ButtonSatSolveOnline.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs 3-Sat")
            Exit Sub
        End If
        My.Computer.FileSystem.WriteAllBytes(Path.GetTempPath & "\Minisat5.exe", My.Resources.minisat5, False)

        'Run du Sat externe
        Dim oProcess As New Process()
        Dim oStartInfo As New ProcessStartInfo(Path.GetTempPath & "\Minisat5.exe ", Application.StartupPath() + "\Takuzu_3Sat.txt")
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()

        'Affichage de la sortie du Sat externe
        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using
        Dim s = Split(sOutput, vbCrLf)
        sOutput = s(1) + vbCrLf + s(2) + vbCrLf + s(8) + vbCrLf + s(9) + vbCrLf + s(10) + vbCrLf + s(11) + vbCrLf + s(13) + vbCrLf + s(14) + vbCrLf + s(16) + vbCrLf + s(17) + vbCrLf + s(18) + vbCrLf + s(19) + vbCrLf + s(20) + vbCrLf + s(21) + vbCrLf + s(22) + vbCrLf + s(23) + vbCrLf + s(24) + vbCrLf + s(47) + vbCrLf + s(48) + vbCrLf + s(49) + vbCrLf
        TextBoxMain.AppendText(sOutput)

        'Recuperation de la solution du Sat externe
        If s(49) = "s SATISFIABLE" Then

            Dim writer = CreateTextFile("Takuzu_Solved_Dimacs.txt")
            If writer Is Nothing Then Exit Sub


            Dim i = 50
            While s(i) <> ""
                TextBoxMain.AppendText(s(i) + vbCrLf)
                writer.WriteLine(s(i).Substring(2))
                i += 1
            End While
            writer.Close()
            TextBoxMain.AppendText("------------------")
            ButtonSatSolveOnline.ForeColor = Color.DarkGreen
            ButtonSatSolvePerso.ForeColor = Color.DarkGreen
            ButtonCompleteTakuzu.ForeColor = Color.Brown
        Else
            TextBoxMain.AppendText("------------------" + vbCrLf + "Il n'y a pas de solutions, la grille de départ n'est peut-etre pas valide")
        End If
    End Sub
#End Region

#Region "Solver perso"
    Private Sub ButtonSatSolvePerso_Click(sender As Object, e As EventArgs) Handles ButtonSatSolvePerso.Click
        TextBoxMain.AppendText(vbCrLf + "A venir. Il faut utiliser l'autre pour l'instant")
    End Sub
#End Region

#Region "Completion takuzu"
    Private Sub ButtonCompleteTakuzu_Click(sender As Object, e As EventArgs) Handles ButtonCompleteTakuzu.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Solved_Dimacs.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez d'abord resoudre une grille à l'aide de l'un des solver")
            Exit Sub
        End If
        pageTakuzuend.Show()
        pageTakuzuend.Loadcomplete()
    End Sub

    Public Sub TakuzuCreateResolved(grilletakuzu As Integer())
        Try
            Dim writer = CreateTextFile("Takuzu_Grille_Resolue.txt")
            For i = 0 To grilletakuzu.Length - 1
                writer.Write(grilletakuzu(i).ToString)
                If (i Mod Math.Sqrt(grilletakuzu.Length) = Math.Sqrt(grilletakuzu.Length) - 1) Then
                    writer.Write(vbCrLf)
                End If
            Next
            writer.Flush()
            writer.Close()
            n = TailledeGrille()
            TextBoxMain.AppendText(vbCrLf + "La grille résolue a été créée")
        Catch ex As Exception
            TextBoxMain.AppendText(vbCrLf + ex.Message)
        End Try
        ButtonCompleteTakuzu.ForeColor = Color.DarkGreen
    End Sub
#End Region

#Region "Fonctions diverses"
    Private Sub ButtonDelAll_Click(sender As Object, e As EventArgs) Handles ButtonDelAll.Click
        Try
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Grille.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_Grille.txt")
                TextBoxMain.AppendText(vbCrLf + "Grille de takuzu supprimée")
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Conjonctive.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_Conjonctive.txt")
                TextBoxMain.AppendText(vbCrLf + "Forme conjonctive supprimée")
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_Dimacs.txt")
                TextBoxMain.AppendText(vbCrLf + "Forme Dimacs supprimée")
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_3Sat.txt")
                TextBoxMain.AppendText(vbCrLf + "Forme 3Sat supprimée")
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Solved_Dimacs.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_Solved_Dimacs.txt")
                TextBoxMain.AppendText(vbCrLf + "Forme Dimacs solvée supprimée")
            End If
            If My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Grille_Resolue.txt") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath() + "\Takuzu_Grille_Resolue.txt")
                TextBoxMain.AppendText(vbCrLf + "Grille de Takuzu résolue supprimée")
            End If
            ButtonOCo.ForeColor = Color.Brown
            ButtonODi.ForeColor = Color.Brown
            ButtonO3s.ForeColor = Color.Brown
            ButtonTakuzuCreate.ForeColor = Color.Brown
            Buttoncreerconjonctive.ForeColor = Color.Brown
            Buttondimacs.ForeColor = Color.Brown
            Button3sat.ForeColor = Color.Brown
            ButtonSatSolveOnline.ForeColor = Color.Brown
            ButtonSatSolvePerso.ForeColor = Color.Brown
            ButtonCompleteTakuzu.ForeColor = Color.Brown
        Catch ex As Exception
            TextBoxMain.AppendText(vbCrLf + ex.Message)
        End Try
    End Sub

    Private Function CreateTextFile(path As String) As StreamWriter
        Dim writer
        Try
            My.Computer.FileSystem.WriteAllText(Application.StartupPath() + "\" + path, "", False)
            TextBoxMain.AppendText(vbCrLf + "Fichier créé : " + path)
            writer = New StreamWriter(Application.StartupPath() + "\" + path)
        Catch ex As Exception
            TextBoxMain.AppendText(vbCrLf + ex.Message)
            Return Nothing
        End Try
        Return writer
    End Function

    Public Function TailledeGrille() As Integer
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Grille.txt") Then
            Return 10
        End If
        Dim reader = New IO.StreamReader(Application.StartupPath() + "\Takuzu_Grille.txt")
        Dim grid = Split(reader.ReadToEnd.TrimEnd, vbCrLf)
        reader.Close()

        If grid.Length <> grid(0).Length Then
            Return -1
        End If
        For i = 1 To grid.Length - 1
            If grid(i - 1).Length <> grid(i).Length Then
                Return -1
            End If
        Next
        Return grid.Length
    End Function

    Private Function BaseN(s As String, first As Boolean) As String
        Dim a, b As Integer
        Select Case s.Length
            Case 2
                a = Val(s(0))
                b = Val(s(1))
            Case 3
                If first Then
                    a = Val(s(0) + s(1))
                    b = Val(s(2))
                Else
                    a = Val(s(0))
                    b = Val(s(1) + s(2))
                End If
            Case 4
                a = Val(s(0) + s(1))
                b = Val(s(2) + s(3))
            Case Else
                Return s
        End Select


        Return (a * n + b + 1).ToString
    End Function







#End Region

End Class
