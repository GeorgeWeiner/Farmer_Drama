using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    /// <summary>
    /// Saves the highscore to an own datatype
    /// </summary>
    /// <param name="_highScore"></param>
    public static void SaveHighScore(int _highScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highscore.farmerdrama";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_highScore);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads the saved highscore from the datatype "highscore"
    /// </summary>
    /// <returns></returns>
    public static SaveData LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.farmerdrama";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream == null)
            {
                stream.Close();
                Debug.LogWarning("Stream empty");
            }

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }

    }

}
