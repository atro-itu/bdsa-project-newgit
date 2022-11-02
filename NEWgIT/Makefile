publish: publish_macos publish_linux publish_windows

publish_macos:
	dotnet publish -p\:AssemblyName=newgit -p\:PublishSingleFile=true -o dist/macos/ -r osx-x64 --self-contained

publish_windows:
	dotnet publish -p\:AssemblyName=newgit -p\:PublishSingleFile=true -o dist/windows/ -r win-x64 --self-contained

publish_linux:
	dotnet publish -p\:AssemblyName=newgit -p\:PublishSingleFile=true -o dist/linux/sf-sc -r linux-x64 --self-contained
