#!/usr/bin/env bash

run_benchmarks() {
  mono ./bin/Release/net46/TimerBenchmarks.exe
  cp ./BenchmarkDotNet.Artifacts/results/UnixBenchmarks-report-console.md ./BenchmarkDotNet.Artifacts/linux-$(cat /sys/devices/system/clocksource/clocksource0/current_clocksource).md
}

dotnet restore
msbuild /p:Configuration=Release

sudo /bin/sh -c 'echo tsc > /sys/devices/system/clocksource/clocksource0/current_clocksource'
run_benchmarks

sudo /bin/sh -c 'echo hpet > /sys/devices/system/clocksource/clocksource0/current_clocksource'
run_benchmarks

sudo /bin/sh -c 'echo acpi_pm > /sys/devices/system/clocksource/clocksource0/current_clocksource'
run_benchmarks

sudo /bin/sh -c 'echo tsc > /sys/devices/system/clocksource/clocksource0/current_clocksource'