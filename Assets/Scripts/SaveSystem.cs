using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static void SaveGame(GameState gameState)
	{
		string directoryPath = Application.persistentDataPath + "/saves/";
		if (!Directory.Exists(directoryPath))
			Directory.CreateDirectory(directoryPath);
		BinaryFormatter formatter = new BinaryFormatter();
		string path = directoryPath + "inTheShadows.save";
		FileStream stream = new FileStream(path, FileMode.Create);

		GameData data = new GameData(gameState);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static GameData LoadGame()
	{
		string path = Application.persistentDataPath + "/saves/inTheShadows.save";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			GameData data = formatter.Deserialize(stream) as GameData;
			stream.Close();
			return data;
		}
		else
		{
			Debug.LogError("Save file not found in " + path);
			return null;
		}
	}

	public static void DeleteSaves()
	{
		string path = Application.persistentDataPath + "/saves/";
		// DirectoryInfo directory = new DirectoryInfo(path);
		if (Directory.Exists(path))
			Directory.Delete(path, true);
		Directory.CreateDirectory(path);
	}

}
