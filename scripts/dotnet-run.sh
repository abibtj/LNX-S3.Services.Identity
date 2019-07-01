#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/S3.Services.Identity
dotnet run --no-restore