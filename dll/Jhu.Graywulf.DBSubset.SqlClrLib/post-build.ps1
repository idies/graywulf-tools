﻿param(
	[string]$SolutionDir,
	[string]$SolutionName,
	[string]$ProjectDir,
	[string]$ProjectName,
	[string]$OutDir,
	[string]$TargetName
)

& "${SolutionDir}${OutDir}gwsqlclrutil.exe" $ProjectDir$OutDir$TargetName.dll Unrestricted

cp $ProjectDir$OutDir$TargetName.dll $SolutionDir$OutDir
cp $ProjectDir$OutDir$TargetName.pdb $SolutionDir$OutDir

