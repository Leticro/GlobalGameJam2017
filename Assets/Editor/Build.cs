using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Build : Editor
{
	[MenuItem("Tools/Update Level List")]
	private static void MakeBuild()
	{
		var levelData = Resources.Load<LevelData>("levelData");
		levelData.levelNames = new List<string>(EditorBuildSettings.scenes.Length);

		foreach (var scene in EditorBuildSettings.scenes)
		{
			var split = scene.path.Split('/');
			var sceneName = split[split.Length - 1].Split('.')[0];

			levelData.levelNames.Add(sceneName);
		}

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh(ImportAssetOptions.Default);
	}
}
