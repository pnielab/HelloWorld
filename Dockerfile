#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
ADD ["bin", "bin"]
ENTRYPOINT ["dotnet", "bin/debug/HashDb.exe"]