using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChallengeGeneratorHollowKnight
{
    public class Boss
    {
        public string Name { get; }
        public double ProbabilitySona { get; }
        public double ProbabilityVoznes { get; }
        public double ProbabilitySvetozar { get; }
        public string ImagePath { get; }

        public Boss(string name, double probabilitySona, double probabilityVoznes, double probabilitySvetozar, string imagePath)
        {
            Name = name;
            ProbabilitySona = probabilitySona;
            ProbabilityVoznes = probabilityVoznes;
            ProbabilitySvetozar = probabilitySvetozar;
            ImagePath = imagePath;
        }
    }
    public class BossLoader
    {
        public static List<Boss> LoadBossData(string filePath)
        {
            List<Boss> bosses = new List<Boss>();

            // Чтение всех строк из файла
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(' '); // Используем пробел как разделитель

                if (parts.Length == 5) // Учитываем 5 элементов
                {
                    string name = parts[0];
                    double probability1 = double.Parse(parts[1]);
                    double probability2 = double.Parse(parts[2]);
                    double probability3 = double.Parse(parts[3]);
                    string imagePath = parts[4].Trim(); // Убираем лишние пробелы

                    // Создаем новый объект Boss
                    var boss = new Boss(name, probability1, probability2, probability3, imagePath);
                    bosses.Add(boss);
                }
            }

            return bosses;
        }
    }

}
