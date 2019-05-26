#!/usr/bin/env bash

dotnet restore
msbuild /p:Configuration=Release
mono ./bin/Release/net46/TimerBenchmarks.exe
