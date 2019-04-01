Imports System.Text
Imports System.IO
Imports System.Threading


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
    Private Shadows Sub Closing() Handles MyBase.Closing
        If MyWorker.IsAlive Then
            If MyWorker.ThreadState = ThreadState.Suspended Then
                MyWorker.Resume()
            End If
            MyWorker.Abort()
        End If
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
 ● pas plus de 2 chiffres identiques côte à côte
 ● autant de 1 et de 0 sur chaque ligne et sur chaque colonne
 ● 2 lignes ou 2 colonnes ne peuvent être identiques")
    End Sub

    Private Sub ButtonRegleConj_Click(sender As Object, e As EventArgs) Handles ButtonRegleConj.Click
        TextBoxMain.AppendText(vbCrLf + "Regle 1 :
  n   n-2
  ⋀    ⋀   (Xi,j V Xi,j+1 V Xi,j+2) ∧ (¬Xi,j v ¬Xi,j+1 v ¬ Xi,j+2) ∧ (Xj,i v Xj+1,i v Xj+2,i) ∧(¬Xj,i v ¬Xj+1,i v ¬Xj+2,i)
 i=1  j=1")
        TextBoxMain.AppendText(vbCrLf + "Regle 2 :
  n    n     n              n
  ⋀    ⋀  ¬[ ⋀(Xik <=> Xjk) ⋀(Xki <=> Xkj)]
 i=1  j=i   k=1            k=1")
        TextBoxMain.AppendText(vbCrLf + "Regle 3 :
  n                   n/2+1   n/2+1    n/2+1   n/2+1
  ⋀   ∀(k1,…,kn2+1) [ ⋁(Xikj) ⋁(¬Xikj) ⋁(Xjki) ⋁(¬Xjki)]
 i=1                  j=1     j=1      j=1     j=1")
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

    Private Sub CheckReglesSep_CheckedChanged(sender As Object, e As EventArgs) Handles CheckReglesMix.CheckedChanged
        If CheckReglesMix.Checked = True Then
            TextBoxMain.AppendText(vbCrLf + "Utilisation la forme conjonctive générée par la règle 2 et 3 ensemble")
        Else
            TextBoxMain.AppendText(vbCrLf + "Utilise la forme conjonctive générée par la règle 2 et par la règle 3 séparé" + vbCrLf + "Attention, la règle 2 pure cause des problèmes de performance" + vbCrLf + " (à éviter au dela de 10x10)")
        End If
    End Sub

    Private Sub CheckRes3Sat_CheckedChanged(sender As Object, e As EventArgs) Handles CheckRes3Sat.CheckedChanged
        If CheckRes3Sat.Checked Then
            If Button3sat.ForeColor = Color.Black Then
                If Buttondimacs.ForeColor = Color.Brown Or ButtonO3s.ForeColor = Color.Brown Then
                    Button3sat.ForeColor = Color.Brown
                Else
                    Button3sat.ForeColor = Color.Green
                End If
            End If
        Else
            If Button3sat.ForeColor = Color.Brown Then
                Button3sat.ForeColor = Color.Black
            End If
        End If
    End Sub
#End Region

#Region "Threading"
    Public MyWorker As System.Threading.Thread = New Threading.Thread(AddressOf CreateConj)
    'Public WorkerConj As System.Threading.Thread = New Threading.Thread(AddressOf CreateConj)
    'Public WorkerDimacs As System.Threading.Thread = New Threading.Thread(AddressOf CreateDimacs)
    'Public Worker3Sat As System.Threading.Thread = New Threading.Thread(AddressOf Create3Sat)
    'Public WorkerSolveMinisat As System.Threading.Thread = New Threading.Thread(AddressOf ResolveMiniSat)
    'Public WorkerSolvePerso As System.Threading.Thread = New Threading.Thread(AddressOf ResolvePerso)

    Delegate Sub SetTextDelegate(ByVal TB As TextBox, ByVal sText As String)
    Delegate Sub SetStopDelegate()

    Private SetText As New SetTextDelegate(AddressOf SetRecuTextBoxText)
    Private Sub SetRecuTextBoxText(ByVal TB As TextBox, ByVal sText As String)
        TB.AppendText(sText)
    End Sub
    Private EnableStopDelegate As New SetStopDelegate(AddressOf EnableStop)
    Private Sub EnableStop()
        MyProgressBar.Style = ProgressBarStyle.Marquee
        MyProgressBar.MarqueeAnimationSpeed = 20
        ButtonStop.Text = "Stop"
        ButtonStop.Enabled = True
    End Sub
    Private DisableStopDelegate As New SetStopDelegate(AddressOf DisableStop)
    Private Sub DisableStop()
        MyProgressBar.MarqueeAnimationSpeed = 0
        MyProgressBar.Style = ProgressBarStyle.Continuous
        ButtonStop.Enabled = False
    End Sub
    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        If MyWorker.ThreadState = ThreadState.Running Then
            MyWorker.Suspend()
            ButtonStop.Text = "Redo"
            MyProgressBar.MarqueeAnimationSpeed = 0
            Exit Sub
        End If
        If MyWorker.ThreadState = ThreadState.Suspended Then
            MyWorker.Resume()
            ButtonStop.Text = "Stop"
            MyProgressBar.MarqueeAnimationSpeed = 20
            Exit Sub
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
        If (MyWorker.IsAlive) Then
            If MyWorker.ThreadState = ThreadState.Suspended Then

                MyWorker = New Thread(AddressOf CreateConj)
                MyWorker.Start()
                EnableStop()
            Else
                MsgBox("Une autre opération est déjà en cours", vbInformation)
            End If
        Else
            MyWorker = New Thread(AddressOf CreateConj)
            MyWorker.Start()
            EnableStop()
        End If
    End Sub

    Public Function CreateConj()


        Using writer = CreateTextFile("Takuzu_Conjonctive.txt")

            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - Nombre supposé de variables (NxN) : " + (n * n).ToString})
            Dim nbclause = 0

            'Preremplissage
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - - Preremplissage "})
            Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Grille.txt")
            Dim i = 0, j = 0
            While Not reader.EndOfStream
                Dim s = reader.ReadLine()
                For i = 0 To s.Length - 1
                    If s(i) = "0" Then
                        writer.WriteLine("-({0},{1}) .", j, i)
                        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                    ElseIf s(i) = "1" Then
                        writer.WriteLine("({0},{1}) .", j, i)
                        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                    ElseIf s(i) <> "2" Then
                        MsgBox("Attention, la grille de départ est érroné")
                    End If
                Next
                j += 1
            End While
            reader.Close()

            'regle 1
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - - Ecriture de la regle 1 "})
            For i = 0 To n - 1
                For j = 0 To n - 3
                    writer.Write("(({0},{1}) + ({0},{2}) + ({0},{3})) ." + vbCrLf + "(-({0},{1}) + -({0},{2}) + -({0},{3}))." + vbCrLf + "(({1},{0}) + ({2},{0}) + ({3},{0})) ." + vbCrLf + "(-({1},{0}) + -({2},{0}) + -({3},{0})) ." + vbCrLf, i, j, j + 1, j + 2)
                    nbclause += 4
                Next
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
            Next

            Dim tab(n - 1) As Boolean
            'regle 2 + 3 non trouvée
            If CheckReglesMix.Checked = True Then
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - - Ecriture de la regle 2 + 3 "})
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
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                Next




            Else
                'regle 2
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - - Ecriture de la regle 2 "})
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
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                Next
                ReDim tab(n - 1)
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
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                Next

                'regle 3 a faire
                ReDim tab(n - 1)
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - - Ecriture de la regle 3 "})
                For j = 0 To n - 1
                    Init3(tab)
                    Do
                        Dim compt = 0, k = 0
                        While (compt < n / 2 + 1)
                            If (tab(k)) Then
                                If (compt <> n / 2) Then
                                    writer.Write("({0},{1}) + ", j, k)
                                Else
                                    writer.Write("({0},{1}) . " + vbCrLf, j, k)
                                End If
                                compt += 1
                            End If
                            k += 1
                        End While

                        compt = 0
                        k = 0
                        While (compt < n / 2 + 1)
                            If (tab(k)) Then
                                If (compt <> n / 2) Then
                                    writer.Write("-({0},{1}) + ", j, k)
                                Else
                                    writer.Write("-({0},{1}) . " + vbCrLf, j, k)
                                End If
                                compt += 1
                            End If
                            k += 1
                        End While
                        nbclause += 2


                    Loop While Increment3(tab)
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                Next
                ReDim tab(n - 1)
                For j = 0 To n - 1
                    Init3(tab)
                    Do
                        Dim compt = 0, k = 0
                        While (compt < n / 2 + 1)
                            If (tab(k)) Then
                                If (compt <> n / 2) Then
                                    writer.Write("({0},{1}) + ", k, j)
                                Else
                                    writer.Write("({0},{1}) . " + vbCrLf, k, j)
                                End If
                                compt += 1
                            End If
                            k += 1
                        End While
                        compt = 0
                        k = 0
                        While (compt < n / 2 + 1)
                            If (tab(k)) Then
                                If (compt <> n / 2) Then
                                    writer.Write("-({0},{1}) + ", k, j)
                                Else
                                    writer.Write("-({0},{1}) . " + vbCrLf, k, j)
                                End If
                                compt += 1
                            End If
                            k += 1
                        End While
                        nbclause += 2
                        nbclause += 1
                    Loop While Increment3(tab)
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "."})
                Next

                writer.Write("(0,0) +  -(0,0)")
            End If

            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - Nombre de clauses créées: " + nbclause.ToString})
        End Using
        ButtonOCo.ForeColor = Color.DarkGreen
        Buttoncreerconjonctive.ForeColor = Color.DarkGreen
        Buttondimacs.ForeColor = Color.Brown
        Button3sat.ForeColor = Color.Brown
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown
        Invoke(DisableStopDelegate)

    End Function

    Private Function Init3(ByRef tab() As Boolean)
        For k = 0 To n / 2
            tab(k) = True
        Next
        For k = n / 2 + 1 To n - 1
            tab(k) = False
        Next
    End Function
    Private Function Increment3(ByRef tab() As Boolean) As Boolean
        For i = 0 To tab.Length - 1
            If (tab(i) = False) Then
                tab(i) = True
                If (i <> 0) Then
                    tab(i - 1) = False
                    Return True
                Else
                    Dim index = RecIncrement3(tab, 0)
                    If (index <> -1) Then
                        tab(index - 1) = False
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        Next
        Return False
    End Function
    Private Function RecIncrement3(ByRef tab() As Boolean, index As Integer) As Integer
        For j = 0 To tab.Length - 1
            If (tab(j) = False) Then
                tab(j) = True
                If (j - 1 = index) Then
                    index = RecIncrement3(tab, j)
                    If (index <> -1) Then
                        tab(index - 1) = False
                        Return index - 1
                    Else
                        Return -1
                    End If
                Else
                    tab(j - 1) = False
                    Return j - 1
                End If
            End If
        Next
        Return -1
    End Function
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
        If (MyWorker.IsAlive) Then
            If MyWorker.ThreadState = ThreadState.Suspended Then
                MyWorker.Resume()
                MyWorker.Abort()
                MyWorker = New Thread(AddressOf CreateDimacs)
                MyWorker.Start()
                EnableStop()
            Else
                MsgBox("Une autre opération est déjà en cours", vbInformation)
            End If
        Else
            MyWorker = New Thread(AddressOf CreateDimacs)
            MyWorker.Start()
            EnableStop()
        End If
    End Sub
    Public Function CreateDimacs()
        Dim reader = New StreamReader(Application.StartupPath() + "\Takuzu_Conjonctive.txt")
        Dim writer = CreateTextFile("Takuzu_Dimacs.txt")
        If writer Is Nothing Then
            Invoke(DisableStopDelegate)
            Exit Function
        End If

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
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - Nombre de variable vérifiées : ok : " + nbvar.ToString})
            Else
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - Nombre de variable vérifié : erreur : " + nbvar.ToString})
                For i = 1 To n * n
                    If Not tabvar(i) Then
                        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " * La variable " + i.ToString + " est manquante"})
                    End If
                Next
                For i = n * n + 1 To n * n * 2 - 2
                    If tabvar(i) Then
                        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " * La variable " + i.ToString + " est de trop"})
                    End If
                Next
            End If
        Catch ex As Exception
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Erreur lors de la verification des variables du fichier Dimacs" + vbCrLf + ex.Message})
        End Try

        ButtonODi.ForeColor = Color.DarkGreen
        Buttondimacs.ForeColor = Color.DarkGreen
        Button3sat.ForeColor = Color.Brown
        ButtonSatSolveOnline.ForeColor = Color.Brown
        ButtonSatSolvePerso.ForeColor = Color.Brown
        ButtonCompleteTakuzu.ForeColor = Color.Brown
        Invoke(DisableStopDelegate)
    End Function
#End Region

#Region "Format 3-Sat"
    Private Sub Button3sat_Click(sender As Object, e As EventArgs) Handles Button3sat.Click
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
            TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs n-Sat")
            Exit Sub
        End If
        If (MyWorker.IsAlive) Then
            If MyWorker.ThreadState = ThreadState.Suspended Then
                MyWorker.Resume()
                MyWorker.Abort()
                MyWorker = New Thread(AddressOf Create3Sat)
                MyWorker.Start()
                EnableStop()
            Else
                MsgBox("Une autre opération est déjà en cours", vbInformation)
            End If
        Else
            MyWorker = New Thread(AddressOf Create3Sat)
            MyWorker.Start()
            EnableStop()
        End If
    End Sub
    Public Function Create3Sat()
        Dim writer = CreateTextFile("Takuzu_3Sat.txt")
        If writer Is Nothing Then
            Invoke(DisableStopDelegate)
            Exit Function
        End If

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
        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + " - Calcul pour 3Sat :" + vbCrLf + " - - nombre de variables : " + nbvar.ToString + vbCrLf + " - - nombre de clause :" + nbclause.ToString})
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
        Invoke(DisableStopDelegate)
    End Function
#End Region

#Region "Solver online"
    Private Sub ButtonSatSolveOnline_Click(sender As Object, e As EventArgs) Handles ButtonSatSolveOnline.Click
        If CheckRes3Sat.Checked Then
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
                TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs 3-Sat")
                Exit Sub
            End If
        Else
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
                TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs n-Sat")
                Exit Sub
            End If
        End If
        If (MyWorker.IsAlive) Then
            If MyWorker.ThreadState = ThreadState.Suspended Then
                MyWorker.Resume()
                MyWorker.Abort()
                MyWorker = New Thread(AddressOf ResolveMiniSat)
                MyWorker.Start()
                EnableStop()
            Else
                MsgBox("Une autre opération est déjà en cours", vbInformation)
            End If
        Else
            MyWorker = New Thread(AddressOf ResolveMiniSat)
            MyWorker.Start()
            EnableStop()
        End If
    End Sub
    Public Function ResolveMiniSat()

        My.Computer.FileSystem.WriteAllBytes(Path.GetTempPath & "\Minisat5.exe", My.Resources.minisat5, False)

        'Run du Sat externe
        Dim oProcess As New Process()
        Dim oStartInfo
        If CheckRes3Sat.Checked Then
            oStartInfo = New ProcessStartInfo(Path.GetTempPath & "\Minisat5.exe ", Application.StartupPath() + "\Takuzu_3Sat.txt")
        Else
            oStartInfo = New ProcessStartInfo(Path.GetTempPath & "\Minisat5.exe ", Application.StartupPath() + "\Takuzu_Dimacs.txt")
        End If
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
        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, sOutput})

        'Recuperation de la solution du Sat externe
        If s(49) = "s SATISFIABLE" Then

            Dim writer = CreateTextFile("Takuzu_Solved_Dimacs.txt")
            If writer Is Nothing Then
                Invoke(DisableStopDelegate)
                Exit Function
            End If


            Dim i = 50
            While s(i) <> ""
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + s(i)})
                writer.WriteLine(s(i).Substring(2))
                i += 1
            End While
            writer.Close()
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "------------------"})
            ButtonSatSolveOnline.ForeColor = Color.DarkGreen
            ButtonSatSolvePerso.ForeColor = Color.DarkGreen
            ButtonCompleteTakuzu.ForeColor = Color.Brown
        Else
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, "------------------" + vbCrLf + "Il n'y a pas de solutions, la grille de départ n'est peut-etre pas valide"})
        End If
        Invoke(DisableStopDelegate)
    End Function
#End Region

#Region "Solver perso"
    Public Class Pile
        Public val As Int16 = 0
        Public savenbClause As Integer
        Public b As Boolean = True
        Public saveVar() As Int16
        Public saveClause()() As Integer
        Public suiv As Pile = Nothing

        Public Function noeud(ByVal i As Int16, ByVal snbClause As Integer, ByVal sVar() As Int16, ByVal sClause()() As Integer) As Pile
            Dim p = New Pile
            With p
                val = i
                saveVar = sVar
                saveClause = sClause
                savenbClause = snbClause
            End With
            Return p
        End Function

    End Class

    Public variables() As Int16
    Public f()() As Integer
    Public nbClause As Integer

    Private Sub ButtonSatSolvePerso_Click(sender As Object, e As EventArgs) Handles ButtonSatSolvePerso.Click
        If CheckRes3Sat.Checked Then
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_3Sat.txt") Then
                TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs 3-Sat")
                Exit Sub
            End If
        Else
            If Not My.Computer.FileSystem.FileExists(Application.StartupPath() + "\Takuzu_Dimacs.txt") Then
                TextBoxMain.AppendText(vbCrLf + " * Vous devez creer d'abord un fichier au format Dimacs n-Sat")
                Exit Sub
            End If
        End If
        If (MyWorker.IsAlive) Then
            If MyWorker.ThreadState = ThreadState.Suspended Then
                MyWorker.Resume()
                MyWorker.Abort()
                MyWorker = New Thread(AddressOf ResolvePerso)
                MyWorker.Start()
                EnableStop()
            Else
                MsgBox("Une autre opération est déjà en cours", vbInformation)
            End If
        Else
            MyWorker = New Thread(AddressOf ResolvePerso)
            MyWorker.Start()
            EnableStop()
        End If
    End Sub

    Public Function ResolvePerso()
        'Lecture du fichier
        Dim reader
        If CheckRes3Sat.Checked Then
            reader = New StreamReader("Takuzu_3Sat.txt")
        Else
            reader = New StreamReader("Takuzu_Dimacs.txt")
        End If
        Dim s = reader.ReadLine().Trim.Split(" ")
        If (s(0) <> "p" And s(1) <> "cnf") Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Erreur fichier d'entrée"})
            Invoke(DisableStopDelegate)
            Exit Function
        End If

        Dim nbVar = CInt(s(2))
        nbClause = CInt(s(3))
        Dim f0 = reader.ReadToEnd().Trim.Split(New Char() {vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        reader.Close()
        ReDim variables(nbVar)
        For i = 0 To nbVar
            variables(i) = -1
        Next



        'transformation en tableau de tableaux d'entiers via un tableau de tableaux de string
        Dim f1(nbClause - 1)() As String
        For i = 0 To f0.Length() - 1
            f1(i) = f0(i).Trim.Substring(0, f0(i).Trim.Length - 1).Trim.Split()
        Next

        ReDim f(nbClause - 1)

        For i = 0 To f1.Length() - 1
            For j = 0 To f1(i).Length - 1
                If (f(i) Is Nothing) Then
                    Array.Resize(f(i), 1)
                Else
                    Array.Resize(f(i), f(i).Length + 1)
                End If
                If (f1(i)(j) <> "") Then
                    f(i)(f(i).Length - 1) = CInt(f1(i)(j))
                End If
            Next
        Next


        Dim resolution = True
        Dim save As Pile = New Pile

resolutionDPLL:
        While (nbClause > 0)
            Dim i, j As Integer
            While (resolution)
                'Clauses unitaires -> Enregistrement de la variable et retrait de l'ensemble de clauses
                i = 0
                While (i < nbClause)
                    If f(i).Length = 1 Then
                        Dim a = Math.Abs(f(i)(0))
                        If variables(a) = -1 Then
                            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Validation par clause unitaire de " + f(i)(0).ToString})
                            If f(i)(0) >= 0 Then
                                variables(a) = 1
                            Else
                                variables(a) = 0
                            End If
                            RemoveAt(f, i)
                            nbClause -= 1
                            If endcheck() Then
                                Exit While
                            End If
                            i -= 1
                        ElseIf variables(a) = pos(f(i)(0)) Then
                            RemoveAt(f, i)
                            nbClause -= 1
                            If endcheck() Then
                                Exit While
                            End If
                            i -= 1
                        Else
                            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Clause unitaire contradictoire : " + f(i)(0).ToString})
                            GoTo unsatisfiable
                        End If
                    End If
                    i += 1
                End While
                resolution = False

                'Verification de clause post validation unitaire
                i = 0
                While (i < nbClause)
                    j = 0
                    While (j < f(i).Length)
                        If variables(Math.Abs(f(i)(j))) <> -1 Then
                            resolution = True
                            If variables(Math.Abs(f(i)(j))) = pos(f(i)(j)) Then
                                RemoveAt(f, i)
                                nbClause -= 1
                                If endcheck() Then
                                    Exit While
                                End If
                                i -= 1
                                Exit While
                            Else
                                RemoveAt(f(i), j)
                                If f(i) Is Nothing Then
                                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Clause vide, retour en arrière"})
                                    GoTo unsatisfiable
                                End If
                                j -= 1
                            End If
                        End If
                        j += 1
                    End While
                    i += 1
                End While
            End While

            'Plus de resolution possible, attribution arbitraire
            For i = 1 To nbVar
                If variables(i) = -1 Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Attribution arbitraire de " + i.ToString + " -> vrai"})
                    Empiler(i, save)
                    variables(i) = 1
                    resolution = True
                    Exit For
                End If
            Next

        End While

        'Fin et conclusion du DPLL
        Dim writer = CreateTextFile("Takuzu_Solved_Dimacs.txt")
        If writer Is Nothing Then
            DisableStop()
            Exit Function
        End If

        For j = 0 To nbVar
            If variables(j) = 1 Then
                writer.Write(j.ToString + " ")
            ElseIf variables(j) = 0 Then
                writer.Write("-" + j.ToString + " ")
            End If
        Next
        writer.Write("0")
        writer.Close()

        Dim ecrire As Boolean = True
        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Solvé !" + vbCrLf + "Variables vraies :"})
        For i = 1 To n * n
            If variables(i) = 1 Then
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString + " "})
            End If
        Next
        For i = n * n + 1 To variables.Length - 1
            If variables(i) = 1 Then
                If ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, ", " + i.ToString + " à "})
                    ecrire = False
                End If
            Else
                If Not ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString})
                    ecrire = True
                End If
            End If
        Next
        If Not ecrire Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, (variables.Length - 1).ToString})
            ecrire = True
        End If

        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Variables fausses :" + vbCrLf})
        For i = 1 To n * n
            If variables(i) = 0 Then
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString + " "})
            End If
        Next
        For i = n * n + 1 To variables.Length - 1
            If variables(i) = 0 Then
                If ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, ", " + i.ToString + " à "})
                    ecrire = False
                End If
            Else
                If Not ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString})
                    ecrire = True
                End If
            End If
        Next
        If Not ecrire Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, (variables.Length - 1).ToString.ToString})
            ecrire = True
        End If

        TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Les autres variables sont libres : " + vbCrLf})
        For i = 1 To n * n
            If variables(i) = -1 Then
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString + " "})
            End If
        Next
        For i = n * n + 1 To variables.Length - 1
            If variables(i) = -1 Then
                If ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, ", " + i.ToString + " à "})
                    ecrire = False
                End If
            Else
                If Not ecrire Then
                    TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, i.ToString})
                    ecrire = True
                End If
            End If
        Next
        If Not ecrire Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, (variables.Length - 1).ToString.ToString})
            ecrire = True
        End If

        ButtonSatSolveOnline.ForeColor = Color.DarkGreen
        ButtonSatSolvePerso.ForeColor = Color.DarkGreen
        ButtonCompleteTakuzu.ForeColor = Color.Brown
        Invoke(DisableStopDelegate)

        Exit Function


unsatisfiable:
        If (save.suiv Is Nothing) Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Insatisfaisable. La grille de depart n'est peut etre pas valide"})
        Else
            Dim a = Depiler(save)
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Attribution arbitraire de " + a.ToString + " -> faux"})
            variables(a) = 0
            GoTo resolutionDPLL
        End If
    End Function

    Private Function pos(a As Integer) As Int16
        If a = 0 Then
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Erreur : 0 s'est retrouvé en variable"})
        End If
        If a >= 0 Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Private Function Empiler(i As Integer, ByRef p As Pile)
        Dim old = p
        p = p.noeud(i, nbClause, variables.Clone, f)
        p.suiv = old
    End Function

    Private Function Depiler(ByRef p As Pile) As Int16
        Dim a = p.suiv.val
        nbClause = p.suiv.savenbClause
        For i = 1 To variables.Length - 1
            variables(i) = p.suiv.saveVar(i)
        Next
        f = p.suiv.saveClause.Clone
        If p.suiv.b Then
            p.suiv.b = False
        Else
            p.suiv = p.suiv.suiv
        End If
        Return a
    End Function

    Private Function endcheck() As Boolean
        Return nbClause <= 0
    End Function

    'Fonction de retrait de ligne de tableau
    '<System.Runtime.CompilerServices.Extension()>
    Public Sub RemoveAt(Of T)(ByRef arr As T(), ByVal index As Integer)
        Dim uBound = arr.GetUpperBound(0)
        Dim lBound = arr.GetLowerBound(0)
        Dim arrLen = uBound - lBound

        If index < lBound OrElse index > uBound Then
            Throw New ArgumentOutOfRangeException(
        String.Format("Index must be from {0} to {1}.", lBound, uBound))

        Else
            'return nothing if it is the last element
            If arrLen = 0 Then
                arr = Nothing
                Exit Sub
            End If
            'create an array 1 element less than the input array
            Dim outArr(arrLen - 1) As T
            'copy the first part of the input array
            Array.Copy(arr, 0, outArr, 0, index)
            'then copy the second part of the input array
            Array.Copy(arr, index + 1, outArr, index, uBound - index)

            arr = outArr
        End If
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
            writer = New StreamWriter(Application.StartupPath() + "\" + path)
        Catch ex As Exception
            Try
                TextBoxMain.AppendText(vbCrLf + ex.Message)
            Catch
                TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + ex.Message})
            End Try
            Return Nothing
        End Try
        Try
            TextBoxMain.AppendText(vbCrLf + "Fichier créé : " + path)
        Catch ex As Exception
            TextBoxMain.Invoke(SetText, New Object() {TextBoxMain, vbCrLf + "Fichier créé : " + path})
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
