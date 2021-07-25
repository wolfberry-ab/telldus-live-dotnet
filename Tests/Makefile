
PROJECTS = ConsoleAppDotNet5/ConsoleAppDotNet5.csproj ConsoleAppDotNet31/ConsoleAppDotNet31.csproj

all:
	$(foreach project,$(PROJECTS),\
		@echo "Building $(project)";\
		dotnet restore $(project);\
		dotnet build --no-restore $(project) || exit;\
	)
