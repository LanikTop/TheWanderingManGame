using System.IO;
using Newtonsoft.Json;

namespace The_wandering_man
{
    public class GameDataSave
    {
        private static readonly string SavePath = $"../../../SaveData.json";

        public class GameData
        {
            public int HighScoreFloors { get; set; }
            public int EnemiesKilled { get; set; }
        }

        public static void Save(GameData data)
        {
            var load = Load();
            if (data.HighScoreFloors < load.HighScoreFloors)
                data.HighScoreFloors = load.HighScoreFloors;
            if (data.EnemiesKilled < load.EnemiesKilled)
                data.EnemiesKilled = load.EnemiesKilled;
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(SavePath, json);
        }

        public static GameData Load()
        {
            if (!File.Exists(SavePath))
            {
                return new GameData();
            }
            string json = File.ReadAllText(SavePath);
            return JsonConvert.DeserializeObject<GameData>(json);
        }

        public static void ResetSaveData()
        {
            var basicData = new GameData();
            string json = JsonConvert.SerializeObject(basicData);
            File.WriteAllText(SavePath, json);
        }
    }
}
