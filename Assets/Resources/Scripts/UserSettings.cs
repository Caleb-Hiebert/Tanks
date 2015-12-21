using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UserSettings {

	public static string fileName = "settings.txt";

	public static float cameraShake = 1;
	public static float sfxVolume = 0;
	public static float ambientVolume = -29;
	public static bool autoSpawn = true;
	public static bool dynamicTerrain = true;
	public static float dynamicTerrainQuality = 50;
	public static bool damageIndicators = true;

	public static void Save() {

		try {
				
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + fileName, FileMode.OpenOrCreate);

			SaveSettings settings = new SaveSettings ();
			settings.cameraShake = cameraShake;
			settings.sfxVolume = sfxVolume;
			settings.ambientVolume = ambientVolume;
			settings.autoSpawn = autoSpawn;
			settings.dynamicTerrain = dynamicTerrain;
			settings.dynamicTerrainQuality = dynamicTerrainQuality;
			settings.damageIndicators = damageIndicators;

			bf.Serialize (file, settings);
			file.Close ();
		} catch {
			Debug.LogWarning("Settings file was busy");
		}
	}

	public static void Load() {
		if (File.Exists (Application.persistentDataPath + fileName)) {

			try {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
				SaveSettings settings = (SaveSettings) bf.Deserialize(file);

				cameraShake = settings.cameraShake;
				sfxVolume = settings.sfxVolume;
				ambientVolume = settings.ambientVolume;
				autoSpawn = settings.autoSpawn;
				dynamicTerrain = settings.dynamicTerrain;
				dynamicTerrainQuality = settings.dynamicTerrainQuality;
				damageIndicators = true;

			} catch {
				Debug.LogWarning("Settings file was busy");
			}
		}
	}
}

[Serializable]
class SaveSettings {
	public float cameraShake;
	public float sfxVolume;
	public float ambientVolume;
	public bool autoSpawn;
	public bool dynamicTerrain;
	public float dynamicTerrainQuality;
	public bool damageIndicators;
}
