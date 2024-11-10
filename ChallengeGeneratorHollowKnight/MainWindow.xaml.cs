using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Effects;


namespace ChallengeGeneratorHollowKnight
{
    public partial class MainWindow : Window
    {
        private List<Boss> _bosses;

        public MainWindow()
        {
            InitializeComponent();

            // Путь к текстовому файлу в папке Files
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "boss_data.txt");

            // Загрузка данных о боссах из файла
            _bosses = BossLoader.LoadBossData(filePath);
        }
        private Dictionary<string, string> pantheonImages = new Dictionary<string, string>
        {
            { "Пантеон Мастера", "PanteonImages/Pantheon_of_the_Master.png" },
            { "Пантеон Художника", "PanteonImages/Pantheon_of_the_Artist.png" },
            { "Пантеон Гуру", "PanteonImages/Pantheon_of_the_Sage.png" },
            { "Пантеон Рыцаря", "PanteonImages/Pantheon_of_the_Knight.png" },
            { "Пантеон Халлоунеста", "PanteonImages/Pantheon_of_Hallownest.png" }
        };

        private Dictionary<string, string> restrictionImages = new Dictionary<string, string>()
        {
            { "Гвоздь", "PanteonImages/Radiant_Badge.png" },
            { "Панцарь", "PanteonImages/Shell_Binding.png" },
            { "Амулеты", "PanteonImages/Charms_Binding.png" },
            { "Душа", "PanteonImages/Soul_Binding.png" }
        };
        //1 испытание
        private void GenerateBosses_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            HashSet<int> generatedIndexes = new HashSet<int>();
            List<Boss> bosses = BossLoader.LoadBossData("Files/boss_data.txt");

            BossContainer.Children.Clear(); // Очистить предыдущие результаты

            while (generatedIndexes.Count < 3)
            {
                int index = random.Next(bosses.Count);
                if (generatedIndexes.Add(index))
                {
                    var boss = bosses[index];

                    // Создаем контейнер для боссов
                    StackPanel bossPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10) };
                    string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, boss.ImagePath);

                    // Создаем изображение босса
                    Image bossImage = new Image
                    {
                        Source = new BitmapImage(new Uri(imagePath)),
                        Width = 400,
                        Height = 400
                    };

                    // Определяем иконку сложности
                    string difficultyIconPath = GetDifficultyIconPath(boss);
                    Image difficultyIcon = new Image
                    {
                        Source = new BitmapImage(new Uri(difficultyIconPath)),
                        Width = 120, // Установите нужный размер для иконки
                        Height = 120
                    };

                    // Добавляем изображение и иконку сложности в панель
                    bossPanel.Children.Add(bossImage);
                    bossPanel.Children.Add(difficultyIcon); // Добавляем иконку сложности

                    // Добавляем панель босса в общий контейнер
                    BossContainer.Children.Add(bossPanel);
                }
            }
        }
        //1 испытание
        private string GetDifficultyIconPath(Boss boss)
        {
            double roll = new Random().NextDouble() * 100;

            if (roll <= boss.ProbabilitySona)
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Attuned_Badge.png");
            else if (roll <= boss.ProbabilityVoznes)
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Ascended_Badge.png");
            else
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Radiant_Badge.png");
        }
        //2 испытание
        private void GenerateTasks_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            List<TaskItem> tasks = TaskLoader.LoadTasks("Files/TravelChallangs.txt");

            TaskContainer.Children.Clear(); // Очистить предыдущие результаты

            HashSet<int> generatedIndexes = new HashSet<int>();
            while (generatedIndexes.Count < 3)
            {
                int index = random.Next(tasks.Count);
                if (generatedIndexes.Add(index))
                {
                    var task = tasks[index];

                    // Генерируем случайное количество в диапазоне
                    int amount = random.Next(task.MinAmount, task.MaxAmount + 1);

                    // Создаем контейнер для задания
                    StackPanel taskPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(10) };

                    // Картинка задания
                    string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, task.ImagePath);
                    Image taskImage = new Image
                    {
                        Source = new BitmapImage(new Uri(imagePath)),
                        Width = 400,
                        Height = 400
                    };

                    string displayText = amount == 1 ? task.Name : $"{task.Name}: {amount}";

                    TextBlock taskText = new TextBlock
                    {
                        // Заменяем подчеркивания на пробелы
                        Text = task.Name.Replace("_", " ") + (amount > 1 ? $": {amount}" : ""),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 45,
                        FontFamily = new FontFamily("Arial"),
                        TextWrapping = TextWrapping.Wrap,
                        MaxWidth = 300,
                        TextAlignment = TextAlignment.Center,
                        FontWeight = FontWeights.Bold, // Полужирное начертание
                        Foreground = Brushes.White,

                        Effect = new DropShadowEffect // Эффект тени
                        {
                            Color = Colors.Black,
                            BlurRadius = 5,
                            ShadowDepth = 1
                        }
                    };

                    // Добавляем картинку и текст в панель
                    taskPanel.Children.Add(taskImage);
                    taskPanel.Children.Add(taskText);

                    // Добавляем панель в общий контейнер
                    TaskContainer.Children.Add(taskPanel);
                }
            }
        }
        //3 испытание
        private void PantheonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PantheonComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedPantheon = selectedItem.Content.ToString();
                if (pantheonImages.ContainsKey(selectedPantheon))
                {
                    string pantheonImagePath = pantheonImages[selectedPantheon];
                    PantheonImage.Source = new BitmapImage(new Uri(pantheonImagePath, UriKind.Relative));

                    // Генерируем случайное ограничение
                    string restriction = GetRandomRestriction();
                    RestrictionTextBlock.Text = restriction; // Отображаем текст ограничения

                    // Загружаем соответствующую картинку ограничения
                    if (restrictionImages.ContainsKey(restriction))
                    {
                        string restrictionImagePath = restrictionImages[restriction];
                        RestrictionImage.Source = new BitmapImage(new Uri(restrictionImagePath, UriKind.Relative));
                    }
                }
            }
        }
        //3 испытание
        private string GetRandomRestriction()
        {
            Random random = new Random();
            List<string> restrictions = restrictionImages.Keys.ToList();
            int index = random.Next(restrictions.Count);
            return restrictions[index];
        }
        //3 испытание
        private void RerollRestriction_Click(object sender, RoutedEventArgs e)
        {
            // Генерируем новое случайное ограничение
            string newRestriction = GetRandomRestriction();
            RestrictionTextBlock.Text = newRestriction; // Обновляем текст ограничения

            // Загружаем соответствующую картинку ограничения
            if (restrictionImages.ContainsKey(newRestriction))
            {
                string restrictionImagePath = restrictionImages[newRestriction];
                RestrictionImage.Source = new BitmapImage(new Uri(restrictionImagePath, UriKind.Relative));
            }
        }

    }
}
