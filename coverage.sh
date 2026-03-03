#!/usr/bin/env bash
set -euo pipefail

COVERAGE_DIR="./coverage"
RAW_DIR="$COVERAGE_DIR/raw"
REPORT_DIR="$COVERAGE_DIR/report"

echo "Clearing old coverage data..."
rm -rf "$COVERAGE_DIR"

echo "Running tests with coverage..."
dotnet test \
  --collect:"XPlat Code Coverage" \
  --results-directory "$RAW_DIR" \
  -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.ExcludeByFile="**/Helper/**"

echo "Generating report..."
dotnet reportgenerator \
  -reports:"$RAW_DIR/**/coverage.cobertura.xml" \
  -targetdir:"$REPORT_DIR" \
  -reporttypes:"Html;TextSummary" \
  -assemblyfilters:"+Vulcan" \
  -verbosity:Warning

echo ""
cat "$REPORT_DIR/Summary.txt"
echo ""
echo "Full report: $REPORT_DIR/index.html"
