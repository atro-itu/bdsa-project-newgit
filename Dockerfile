FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Test
RUN dotnet test /p:CollectCoverage=true
