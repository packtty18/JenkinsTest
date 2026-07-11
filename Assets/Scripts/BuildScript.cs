using UnityEditor;
using UnityEditor.Build.Reporting;
using System.Linq;

public class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes = EditorBuildSettings.scenes // 빌드할 씬 가져오기
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        string buildPath = "Builds/Windows/Game.exe"; // 빌드 경로 설정하기

        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.StandaloneWindows64, // Windows 64비트 빌드 설정하기
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);

        if (report.summary.result == BuildResult.Succeeded)
        {
            UnityEngine.Debug.Log("Build succeeded: " + report.summary.totalSize + " bytes");
        }
        else
        {
            UnityEngine.Debug.LogError("Build failed");
            EditorApplication.Exit(1); // Jenkins에 빌드 실패 전달하기
        }
    }
}