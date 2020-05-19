Imports System.Data
Imports System.Data.OleDb

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtLogin.Select()
    End Sub

    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        Dim provider As String = "provider=SQLOLEDB.1;"
        Dim ficheiro As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\User.mdf;Integrated Security=True"

        Dim connstring As String = provider & ficheiro
        Dim ligacao As OleDbConnection = New OleDbConnection(connstring)
        Try
            ligacao.Open()
            MsgBox("Foi estabelecida a conexao com a base de dados.")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM utilizadores where nome='" & txtLogin.Text & "' AND password ='" & txtSenha.Text & "'", ligacao)
        Dim dr As OleDbDataReader = cmd.ExecuteReader
        Dim encontrou As Boolean = False
        Dim strNome As String = ""
        Dim strApledido As String = ""
        If dr.HasRows Then
            While dr.Read
                encontrou = True
                strNome = dr("nome").ToString
                strApledido = dr("Apelido").ToString
            End While
        End If
        ligacao.Close()

        If encontrou = True Then
            Me.Close()
            Form1.Show()
            Form1.Label1.Text = "Bem vindo(a)" & strNome & strApledido

        Else
            Dim msg As String = "Nao encontrado." & vbNewLine & "Utilizador ou senha incorretos."
            Dim titulo As String = "Aviso!"
            Dim botoes = MessageBoxButtons.OK
            Dim icone = MessageBoxIcon.Exclamation
            MessageBox.Show(msg, titulo, botoes, icone)
        End If
    End Sub
End Class