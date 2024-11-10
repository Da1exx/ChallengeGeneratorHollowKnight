using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChallengeGeneratorHollowKnight
{
    public class TaskLoader
    {
        public static List<TaskItem> LoadTasks(string filePath)
        {
            List<TaskItem> tasks = new List<TaskItem>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 4)
                {
                    string name = parts[0];
                    int minAmount = int.Parse(parts[1]);
                    int maxAmount = int.Parse(parts[2]);
                    string imagePath = parts[3].Trim();

                    var task = new TaskItem(name, minAmount, maxAmount, imagePath);
                    tasks.Add(task);
                }
            }

            return tasks;
        }
    }
}
