using UnityEngine;

public class LevelLoader: MonoBehaviour{
	public static void LoadNextLevel(){
		Application.LoadLevel (Application.loadedLevel+1);
        Time.timeScale = 1.0f;
	}

	public static void RestartLevel(){
		Application.LoadLevel (Application.loadedLevel);
        Time.timeScale = 1.0f;
	}

	public static void LoadLevel(string sceneName){
		Application.LoadLevel (sceneName);
        Time.timeScale = 1.0f;
	}

    public static bool IsLastLevel()
    {
        return (Application.loadedLevel == Application.levelCount - 1);
    }
}