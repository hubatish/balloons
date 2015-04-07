using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;

public class SupersonicPostProccesser
{

	[PostProcessBuild(500)]
	public static void OnPostProcessBuild( BuildTarget target, string path )
	{
		if (target == BuildTarget.iOS)
		{
			UnityEditor.XCodeEditor.XCProject project = new UnityEditor.XCodeEditor.XCProject(path);
			
			// Find Supersonic.projmods file and run it
			
			var files = System.IO.Directory.GetFiles(Application.dataPath, "Supersonic.projmods", System.IO.SearchOption.AllDirectories);
			if(files[0] != null)
				project.ApplyMod(Application.dataPath, files[0]);
			project.Save();
			Debug.Log("Supersonic PostProccesser finished");
		}
	}
}