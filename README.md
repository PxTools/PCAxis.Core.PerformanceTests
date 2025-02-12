# PCAxis.Core.PerformanceTests

This project contains performance tests for the PCAxis.Core library using BenchmarkDotNet.

## Prerequisites

- .NET 9.0 SDK or later
- BenchmarkDotNet

## Running the Benchmarks

1. Clone the repository:

    ```sh
    git clone https://github.com/pxtools/PCAxis.Core.PerformanceTests.git
    cd PCAxis.Core.PerformanceTests
    ```

2. Restore the dependencies:

    ```sh
    dotnet restore
    ```

3. Build the project:

    ```sh
    dotnet build -c Release
    ```

4. Run the benchmarks:

    ```sh
    dotnet run -c Release --project PCAxis.Core.PerformanceTests.csproj
    ```

## Benchmark Results

The benchmark results will be displayed in the console and saved in the BenchmarkDotNet.Artifacts directory.

## License

This project is licensed under the MIT License.