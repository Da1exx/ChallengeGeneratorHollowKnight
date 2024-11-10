using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeGeneratorHollowKnight
{
    public class TaskItem
    {
        public string Name { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public string ImagePath { get; set; }

        public TaskItem(string name, int minAmount, int maxAmount, string imagePath)
        {
            Name = name;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
            ImagePath = imagePath;
        }
    }
}
