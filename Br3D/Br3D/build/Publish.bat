rem 불필요한 파일 삭제
pushd ..\bin\x64\Release
del *.pdb
del *.xml
popd

pushd ..\bin\x64\ReleaseLT
del *.pdb
del *.xml
popd

pushd ..\Install
call CodeSign_Program_All.bat
popd

pushd ..\Patch
"c:\Program Files (x86)\wyBuild\wybuild.cmd.exe" "Br3D.wyp" /bu /bwu /upload
"c:\Program Files (x86)\wyBuild\wybuild.cmd.exe" "Br3DLT.wyp" /bu /bwu /upload
popd

pushd ..\Install
"c:\Program Files (x86)\Inno Setup 6\Compil32.exe" /cc Setup_Br3D.iss
"c:\Program Files (x86)\Inno Setup 6\Compil32.exe" /cc Setup_Br3DLT.iss
call CodeSign_Setup_All.bat
popd


rem ftp -s:upload.txt
