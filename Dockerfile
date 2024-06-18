FROM mcr.microsoft.com/dotnet/aspnet:8.0.1-alpine3.18 AS base
WORKDIR /app
EXPOSE 8080
RUN apk add --no-cache icu-libs

ENV TZ=America/Bogota

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENV ASPNETCORE_URLS=http://+:8080
ENV COMPlus_EnableDiagnostics=0

RUN addgroup --system -g 1200 customuser \
&& adduser --system -u 1200 -g 1200 customuser
RUN chown -R customuser:customuser /app 

USER customuser

FROM base AS final
COPY _#{Build.Repository.Name}#/Artifact-#{Build.Repository.Name}#  .

ENTRYPOINT ["dotnet", "API.dll"]