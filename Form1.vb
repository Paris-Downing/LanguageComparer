Public Class Form1
    Dim list As New ArrayList()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Language, Word Order, Family, Cases, Tones/Pitch, Conjugations
        list.Add(New Language("Arabic", "SVO", "Semitic", 3, 0, True))
        list.Add(New Language("Bengali", "SOV", "Indo-Aryan", 4, 0, True))
        list.Add(New Language("Chinese", "SVO", "Sinitic", 0, 5, False))
        list.Add(New Language("Danish", "SVO", "Germanic", 0, 0, False))
        list.Add(New Language("Dutch", "SVO", "Germanic", 0, 0, True))
        list.Add(New Language("Esperanto", "SVO", "Romance", 2, 0, False))
        list.Add(New Language("English", "SVO", "Germanic", 3, 0, True))
        list.Add(New Language("French", "SVO", "Romance", 0, 0, True))
        list.Add(New Language("German", "SVO", "Germanic", 4, 0, True))
        list.Add(New Language("Greek(Modern)", "SVO", "Hellenic", 5, 0, True))
        list.Add(New Language("Hindi", "SOV", "Indo-Aryan", 2, 0, True))
        list.Add(New Language("Italian", "SVO", "Romance", 0, 0, True))
        list.Add(New Language("Japanese", "SOV", "Japonic", 0, 2, True))
        list.Add(New Language("Korean", "SOV", "Altaic", 0, 2, True))
        list.Add(New Language("Latin", "SOV", "Romance", 6, 0, True))
        list.Add(New Language("Norwegian", "SVO", "Germanic", 0, 2, False))
        list.Add(New Language("Polish", "SVO", "Slavic", 7, 0, True))
        list.Add(New Language("Portuguese", "SVO", "Romance", 0, 0, True))
        list.Add(New Language("Russian", "SVO", "Slavic", 6, 0, True))
        list.Add(New Language("Spanish", "SVO", "Romance", 0, 0, True))
        list.Add(New Language("Swedish", "SVO", "Germanic", 0, 2, False))
        list.Add(New Language("Telugu", "SOV", "Dravidian", 8, 0, True))
        list.Add(New Language("Ukrainian", "SVO", "Slavic", 7, 0, True))
        list.Add(New Language("Xhosa", "SVO", "Bantu", 0, 2, True))
        list.Add(New Language("Yoruba", "SVO", "Yoruboid", 0, 3, False))
        list.Add(New Language("Zulu", "SVO", "Bantu", 0, 3, True))


        For i As Integer = 0 To list.Count - 1
            ListBox1.Items.Add(list(i).Name)
            ListBox2.Items.Add(list(i).Name)
        Next
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCompare.Click
        If ListBox1.SelectedItem = "" Or ListBox2.SelectedItem = "" Then
            MsgBox("Please select 2 languages")
        ElseIf ListBox1.SelectedItem.ToString().Equals(ListBox2.SelectedItem.ToString()) Then
            MsgBox("You cannot compare a language to itself")
        Else
            LangCompare(ListBox1.SelectedItem.ToString(), ListBox2.SelectedItem.ToString())
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

    End Sub

    Public Sub LangCompare(lang1 As String, lang2 As String)

        Dim compLang1 As Language = New Language("", "", "", 0, 0, False)
        Dim compLang2 As Language = New Language("", "", "", 0, 0, False)

        'Language, Word Order, Family, Cases, Tones/Pitch, Conjugations
        For i As Integer = 0 To list.Count - 1
            Console.Write(list(i).Name)
            If list(i).Name.Equals(lang1) Then
                compLang1.MyLang() = list(i)
            ElseIf list(i).Name.Equals(lang2) Then
                compLang2.MyLang() = list(i)
            End If

        Next
        If compLang1 Is Nothing Or compLang2 Is Nothing Then
            MsgBox("Error")
        Else
            MsgBox(compLang1.Compare(compLang2))
        End If
    End Sub
End Class

Public Class Language
    Private Property _Name As String
    Private Property _WordOrder As String
    Private Property _Family As String
    Private Property _Cases As Integer
    Private Property _Tones As Integer
    Private Property _Conjugations As Boolean

    Public Sub New(ByVal name As String, ByVal wordOrder As String, ByVal family As String, ByVal cases As Integer, ByVal tones As Integer, ByVal conjugations As Boolean)
        _Name = name
        _WordOrder = wordOrder
        _Family = family
        _Cases = cases
        _Tones = tones
        _Conjugations = conjugations
        'Console.Write("Lang: " + family)
    End Sub

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal name As String)
            _Name = name
        End Set
    End Property

    Public Property WordOrder() As String
        Get
            Return _WordOrder
        End Get
        Set(ByVal wordOrder As String)
            _WordOrder = wordOrder
        End Set
    End Property

    Public Property Family() As String
        Get
            Return _Family
        End Get
        Set(ByVal family As String)
            _Family = family
        End Set
    End Property

    'Language, Word Order, Family, Cases, Tones/Pitch, Conjugations
    Public Property Cases() As String
        Get
            Return _Cases
        End Get
        Set(ByVal cases As String)
            _Cases = cases
        End Set
    End Property

    Public Property Tones() As String
        Get
            Return _Tones
        End Get
        Set(ByVal tones As String)
            _Tones = tones
        End Set
    End Property

    Public Property Conjugations() As String
        Get
            Return _Conjugations
        End Get
        Set(ByVal conjugations As String)
            _Conjugations = conjugations
        End Set
    End Property

    Public WriteOnly Property MyLang() As Language
        Set(ByVal l As Language)
            _Name = l.Name
            _WordOrder = l.WordOrder
            _Family = l.Family
            _Cases = l.Cases
            _Tones = l.Tones
            _Conjugations = l.Conjugations
        End Set
    End Property

    Public Function IsTonal(num As Integer) As String
        Dim MyReturn As String = ""
        If num = 0 Then
            MyReturn = "non-tonal"
        ElseIf num = 2 Then
            MyReturn = "pitch"
        Else
            MyReturn = "tonal"
        End If
        Return MyReturn
    End Function
    Public Function Compare(lang2 As Language) As String
        Dim MyReturn As String = "Comparing: " + _Name + " and " + lang2.Name + Environment.NewLine + Environment.NewLine
        Dim similarities As New ArrayList()
        Dim differences As New ArrayList()
        If _WordOrder.Equals(lang2.WordOrder) Then
            similarities.Add("Word Order: " + _WordOrder + Environment.NewLine)
        Else
            differences.Add("Word Order: (" + _Name + ", " + _WordOrder + ") (" + lang2.Name + ", " + lang2.WordOrder + ")" + Environment.NewLine)
        End If

        'Language, Word Order, Family, Cases, Tones/Pitch, Conjugations
        'Console.Write("Language Family: " + lang2.Family.ToString())
        If _Family.Equals(lang2.Family) Then
            similarities.Add("Language Family: " + _Family + Environment.NewLine)
        Else
            differences.Add("Language Family: (" + _Name + ", " + _Family + ") (" + lang2.Name + ", " + lang2.Family + ")" + Environment.NewLine)
        End If

        If _Cases = lang2.Cases Then
            similarities.Add("Number of Cases: " + _Cases.ToString + Environment.NewLine)
        Else
            differences.Add("Number of Cases: (" + _Name + ", " + _Cases.ToString + ") (" + lang2.Name + ", " + lang2.Cases.ToString + ")" + Environment.NewLine)
        End If

        If _Tones = lang2.Tones Then
            similarities.Add(IsTonal(_Tones) + Environment.NewLine)
        Else
            differences.Add("(" + _Name + ", " + IsTonal(_Tones) + ") (" + lang2.Name + ", " + IsTonal(lang2.Tones) + ")" + Environment.NewLine)
        End If

        If _Conjugations And lang2.Conjugations Then
            similarities.Add("Conjugations in present tense: yes")
        ElseIf (_Conjugations = False) And (lang2.Conjugations = False) Then
            similarities.Add("Conjugations in present tense: no")
        ElseIf Not _Conjugations And lang2.Conjugations Then
            differences.Add("Conjugations in present tense: (" + _Name + ", no) (" + lang2.Name + ", yes)")
        Else
            differences.Add("Conjugations in present tense: (" + _Name + ", yes) (" + lang2.Name + ", no)")
        End If

        If similarities.Count > 0 Then
            MyReturn += "Similarities: " + Environment.NewLine
            For i As Integer = 0 To similarities.Count - 1
                MyReturn += similarities(i)
            Next
        Else
            MyReturn += "Similarities: None" + Environment.NewLine
        End If

        MyReturn += Environment.NewLine + Environment.NewLine

        If differences.Count > 0 Then
            MyReturn += "Differences " + Environment.NewLine
            For i As Integer = 0 To differences.Count - 1
                MyReturn += differences(i)
            Next
        Else
            MyReturn += "Differences: None" + Environment.NewLine
        End If

        Return MyReturn
    End Function
End Class