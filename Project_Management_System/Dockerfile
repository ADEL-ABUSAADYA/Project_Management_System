
FROM mcr.microsoft.com/dotnet/sdk:9.0 
WORKDIR /app
COPY ["Project_Management_System/Project_Management_System.csproj", "Project_Management_System/"]
RUN dotnet restore "./Project_Management_System/Project_Management_System.csproj"
COPY . .
WORKDIR "/app/Project_Management_System"
ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://+:8080"]