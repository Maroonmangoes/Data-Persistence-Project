using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName;
    public int currentScore;

    public int highScore;
    public string highScoreName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
        LoadHighScore();

    }

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;

        Application.Quit();
    }

    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public string highScoreName;
    }

    public void SaveHighScore(int currentScore, string playerName)
    {
        SaveData data = new SaveData();

        data.highScore = currentScore;
        data.highScoreName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }
}
