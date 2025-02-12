namespace PCAxis.Core.Performancetests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Running;
    using PCAxis.Paxiom;

    public class PXFileBuilderBenchmark
    {
        private PXFileBuilder? builder;

        [ParamsSource(nameof(FilePaths))]
        public string FilePath { get; set; } = string.Empty;

        public static IEnumerable<string> FilePaths
        {
            get
            {
                string projectRoot = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.Parent!.Parent!.Parent!.FullName;
                string testFilesDirectory = Path.Combine(projectRoot, "TestFiles");

                if (!Directory.Exists(testFilesDirectory))
                {
                    Console.Error.WriteLine($"🔥 ERROR: TestFiles directory not found at {testFilesDirectory}");
                    return Array.Empty<string>();
                }

                return Directory.GetFiles(testFilesDirectory, "*.px");
            }
        }

        [Benchmark]
        public void BuildPXModelBenchmark()
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                {
                    Console.Error.WriteLine($"🔥 ERROR: Invalid file path: {FilePath}");
                    return;
                }

                builder = new PXFileBuilder();
                string absolutePath = Path.GetFullPath(FilePath);
                builder.SetPath(absolutePath);

                builder.BuildForSelection();
                var selection = Selection.SelectAll(builder.Model.Meta);
                builder.BuildForPresentation(selection);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ BuildPXModelBenchmark failed: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            BenchmarkRunner.Run<PXFileBuilderBenchmark>(config);
        }
    }
}
