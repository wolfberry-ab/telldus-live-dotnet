
PROJECTS = ConsoleAppDotNet10/ConsoleAppDotNet10.csproj

all:
	$(foreach project,$(PROJECTS),\
		@echo "Building $(project)";\
		dotnet restore $(project);\
		dotnet build --no-restore $(project) || exit;\
	)
