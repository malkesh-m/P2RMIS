set SAVESTAMP=%DATE:/=-%@%TIME::=-%
set SAVESTAMP=%SAVESTAMP: =%
set SAVESTAMP=%SAVESTAMP:,=.%
echo Copying code files 
echo d   | xcopy /E /D /C /Y \\ifprsmci01\Jenkins\P2RMIS-Staging\workspace\P2RMIS\obj\Staging\Package\PackageTmp \\uap2rmisws04\webprojects\review-ua4\wwwroot
echo Copying report and db files
xcopy  /E /D /C /Y \\ifprsmci01\Jenkins\P2RMIS-Staging\workspace\Reporting \\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\ReportFiles
xcopy  /E /D /C /Y \\ifprsmci01\Jenkins\P2RMIS-Staging\workspace\Database \\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\Database
echo Deploying Reports
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" "\\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\ReportFiles\Reporting.rptproj" /Deploy "Staging" /out log%SAVESTAMP%.txt
echo Deploying Database Project
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "\\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\Database\Database.sqlproj" /t:Rebuild
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "\\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\Database\Database.sqlproj" /t:Publish /p:SqlPublishProfilePath="\\ifshfs01\Data\P2RMIS\DeploymentFiles\Staging\Database\Staging.publish.xml"
echo Deployment Complete
PAUSE