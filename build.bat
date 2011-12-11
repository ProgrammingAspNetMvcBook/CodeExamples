SET MSBUILD="%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
SET MSTEST="%VS100COMNTOOLS%..\IDE\mstest.exe"
SET BUILDDIR="%CD%\Build"

%MSBUILD% /t:Clean,Rebuild /p:OutDir=%BUILDDIR%/ /p:Configuration=Release "Programming ASP.NET MVC.sln"
%MSTEST%  /testcontainer:%BUILDDIR%/UnitTests.dll /testcontainer:%BUILDDIR%/IntegrationTests.dll
