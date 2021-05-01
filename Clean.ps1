cd "C:\Development\FileSystemProcessor"
dir .\ -include bin,obj,_resharper*,packages,'.vs',TestResults -recurse | foreach($_) { rd $_.fullname –Recurse –Force}