rem 불필요한 파일 삭제
pushd ..\bin\x64\Release
del *.pdb
del *.xml
popd

rem ReleaseLT 폴더삭제 후 복사
del ..\bin\x64\ReleaseLT /q /s
md ..\bin\x64\ReleaseLT
xcopy ..\bin\x64\Release\* ..\bin\x64\ReleaseLT\* /s
move ..\bin\x64\ReleaseLT\Br3D.exe ..\bin\x64\ReleaseLT\Br3DLT.exe 

rem patch 정보 복사
copy ..\Patch\wyUpdate\client.wyc ..\bin\x64\Release /y
copy ..\Patch\wyUpdateLT\client.wyc ..\bin\x64\ReleaseLT  /y

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
