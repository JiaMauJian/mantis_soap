' http://symfoware.blog68.fc2.com/blog-entry-111.html

Imports System.IO

Public Class Form1
    Dim username As String = "admin"
    Dim password As String = "5050789"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim client As New mantis_soap.MantisReference.MantisConnectPortTypeClient()
        MessageBox.Show(client.mc_version())
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim client As New mantis_soap.MantisReference.MantisConnectPortTypeClient()
        Dim issueData As New mantis_soap.MantisReference.IssueData()

        Dim id As String = client.mc_project_get_id_from_name(username, password, "FDC_WS")

        issueData.project = New mantis_soap.MantisReference.ObjectRef()
        issueData.project.id = id
        issueData.category = "General"
        issueData.summary = "[F6WS-25][SlurryValve_Left]"
        issueData.description = "TOOL ID : F6WS-25" + vbCrLf + "RECIPE_ID : TEST"
        id = client.mc_issue_add(username, password, issueData)

        Dim filePath As String = "\\10.61.22.103\TMSSummaryChart\WS\F05C-F6WS25010034-20161130212108.png"
        Dim fileName As String = "F05C-F6WS25010034-20161130212108.png"
        client.mc_issue_attachment_add(username, password, id, fileName, "image/png", ConvertFileToBase64(filePath))

        MessageBox.Show("create issue id = " + id)
    End Sub

    Public Function ConvertFileToBase64(ByVal fileName As String) As Byte()
        Dim base64str As String = Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName))
        Dim bytes As Byte() = Convert.FromBase64String(base64str)
        Return bytes
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        'mantis 的 smtp超難設定，搞不定
        '想法: 用程式抓結案的issue，發mail
        Dim client As New mantis_soap.MantisReference.MantisConnectPortTypeClient()
        Dim issueData As New mantis_soap.MantisReference.IssueData()
        issueData = client.mc_issue_get(username, password, 2)

    End Sub
End Class
