REM codesign program
pushd CertificateFile_pfx

rem %1 : path for exe file and dll file

rem code sign start *.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\Release\NGettext.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\Release\hanee.Cad.Tool.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\Release\hanee.Geometry.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\Release\hanee.ThreeD.dll

signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\ReleaseLT\NGettext.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\ReleaseLT\hanee.Cad.Tool.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\ReleaseLT\hanee.Geometry.dll
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\ReleaseLT\hanee.ThreeD.dll


rem code sign start *.exe
signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\Release\Br3D.exe

signtool.exe sign /a /v  /f HanGil_CodeSign.pfx /p aa123123 ..\..\bin\x64\ReleaseLT\Br3D_LT.exe

rem call Codesign_all.bat ..\..\bin\Release
popd
