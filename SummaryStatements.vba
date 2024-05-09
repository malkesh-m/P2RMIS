
Sub SSFormatMacro()
'
' SSFormatMacro Macro
'
'
    Selection.MoveDown Unit:=wdLine, Count:=1
    Selection.Rows.ConvertToText Separator:=wdSeparateByParagraphs, _
        NestedTables:=False
    Selection.Rows.ConvertToText Separator:=wdSeparateByParagraphs, _
        NestedTables:=False
End Sub
Sub MacroKeepwNext()
'
' MacroKeepwNext Macro
'
'
    Selection.HomeKey Unit:=wdStory
    Selection.MoveDown Unit:=wdLine, Count:=1
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find.Replacement.ParagraphFormat
        .SpaceBeforeAuto = False
        .SpaceAfterAuto = False
        .KeepWithNext = True
    End With
    With Selection.Find
        .Text = "<keepn>"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
End Sub
Sub MacroDeleteKeepnMiscFormatting()
'
' MacroDeleteKeepn Macro
'
'
    Selection.HomeKey Unit:=wdStory
    Selection.MoveDown Unit:=wdLine, Count:=1
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    With Selection.Find
        .Text = "<keepn>"
        .Replacement.Text = ""
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = "  "
        .Replacement.Text = " "
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = " ^p"
        .Replacement.Text = "^p"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = "^p "
        .Replacement.Text = "^p"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = "^p^p"
        .Replacement.Text = "^p"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    With Selection.Find
        .Text = """"
        .Replacement.Text = """"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = False
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
End Sub


Sub ProcessSummaryStatementDirectory()
'
' ProcessSummaryStatements Macro
'
'
    Dim Pathname As String
    Application.DisplayAlerts = wdAlertsNone
    Pathname = ActiveDocument.Path & "\ToProcess\"
    Call ProcessSummaryStatements(Pathname, 1)
End Sub
Sub ProcessSummaryStatements(strPath As String, lngSheet As Long)
    Application.DisplayAlerts = wdAlertsNone
    
    Dim Filename, pdfPath As String
    Dim doc As Document
    Dim strDirList() As String
    Dim lngArrayMax, x As Long
    lngArrayMax = 0
    pdfPath = Replace(strPath, "doc", "pdf")
    If Len(Dir(pdfPath, vbDirectory)) = 0 Then
       MkDir pdfPath
    End If
    Filename = Dir(strPath & "*.*", 23)
    Do While Filename <> ""
        If Filename <> "." And Filename <> ".." Then
                If (GetAttr(strPath & Filename) And vbDirectory) = vbDirectory Then
                lngArrayMax = lngArrayMax + 1
                ReDim Preserve strDirList(lngArrayMax)
                strDirList(lngArrayMax) = strPath & Filename & "\"
            Else
                
                Set doc = Documents.Open(strPath & Filename)
                Documents(Filename).Activate
                ProcessSummaryStatement
                ActiveDocument.ExportAsFixedFormat OutputFileName:= _
                (pdfPath & Replace(Filename, ".doc", ".pdf")), ExportFormat:= _
                wdExportFormatPDF, OpenAfterExport:=False, OptimizeFor:= _
                wdExportOptimizeForPrint, Range:=wdExportAllDocument, From:=1, To:=1, _
                Item:=wdExportDocumentContent, IncludeDocProps:=True, KeepIRM:=True, _
                CreateBookmarks:=wdExportCreateNoBookmarks, DocStructureTags:=True, _
                BitmapMissingFonts:=True, UseISO19005_1:=False
                doc.Close SaveChanges:=True
            End If
        End If
        Filename = Dir()
    Loop
    If lngArrayMax <> 0 Then
        For x = 1 To lngArrayMax
            Call ProcessSummaryStatements(strDirList(x), lngSheet)
        Next
    End If
    ActiveDocument.Close
End Sub
Sub ProcessSummaryStatement()
    SSFormatMacro
    MacroKeepwNext
    MacroDeleteKeepnMiscFormatting
    ReplaceFirstPageFooter
End Sub

Sub ReplaceFirstPageFooter()
'
' Macro3 Macro
'
'
    If ActiveWindow.View.SplitSpecial = wdPaneNone Then
        ActiveWindow.ActivePane.View.Type = wdPrintView
    Else
        ActiveWindow.View.Type = wdPrintView
    End If
    ActiveWindow.ActivePane.View.SeekView = wdSeekFirstPageFooter
    With Selection.Find
         .Text = _
            "Procurement Sensitive Document^lDo not copy or distribute without CDMRP written permission."
        .Replacement.Text = _
            "Procurement Sensitive Document^pDo not copy or distribute without CDMRP written permission.^pPrepared by Systems Research and Applications Corporation (SRA),^pA Wholly Owned Subsidiary of SRA International, Inc., a CSRA Company, a support contractor to CDMRP"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchWildcards = False
        .MatchSoundsLike = False
        .MatchAllWordForms = False
    End With
    Selection.Find.Execute Replace:=wdReplaceAll
    ActiveWindow.ActivePane.View.SeekView = wdSeekMainDocument
End Sub

