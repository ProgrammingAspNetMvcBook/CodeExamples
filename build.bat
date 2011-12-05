SET MSBUILD=%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe

%MSBUILD% /t:Clean,Rebuild /p:Configuration=Release "Programming ASP.NET MVC.sln"
