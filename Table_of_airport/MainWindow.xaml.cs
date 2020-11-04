using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Windows.Threading;

namespace Table_of_airport
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int N = 10;
        Boolean flag = false;
        int timerTickCount = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            switch (numberstr.SelectedIndex) {
                case 0:
                    N = 10;
                    break;
                case 1:
                    N = 20;
                    break;
                case 2:
                    N = 50;
                    break;
                case 3:
                    N = 100;
                    break;
                case 4:
                    N = 500;
                    break;
                case 5:
                    N = 2000;
                    break;
                case 6:
                    N = 5000;
                    break;
                default:
                    N = 10;
                    Console.WriteLine("Будет сгенерированно 10 строк (по дефолту)");
                    break;
            }
            flag = write_text_to_file(N);

            if (flag == true)
                MessageBox.Show("Запись выполнена");
            else
                MessageBox.Show("Запись не выполнена");

            // Вывод данных в richtextbox
            string str_way_to_file = @"C:\Users\Vladislav\C# WPF projects\Table_of_airport\Table_of_airport\input.txt";

            System.IO.StreamReader objReader = new StreamReader(str_way_to_file);
            if (File.Exists(str_way_to_file))
            {
                richtextboxforfile.AppendText(objReader.ReadToEnd());
            }
            else
                richtextboxforfile.AppendText("Не могу открыть фаил");
            objReader.Close();
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
        }

        private Boolean write_text_to_file(int N)
        {
            // Путь к фаилу в который будет генерироваться расписание рейсов
            string path = @"C:\Users\Vladislav\C# WPF projects\Table_of_airport\Table_of_airport\input.txt";

            // Описание структуры фаила
            string struct_file = "Номер_рейса|Тип_самолёта|Город_вылета|Аэропорт_вылета|Город_прилёта|Аэропорт_прилёта|Время_отправления|Время_прибытия|";

            // Тип самолётов
            string[] types_of_aircraft = { "Ту-134", "Ту-154 ", "Ту-204", "Сc-100", "ИЛ-62", "ИЛ-86", "ИЛ-96", "ИЛ-96-300", "ИЛ-96М", "Аэробус-A310", "Аэробус-A320", "Аэробус-A330", "Боинг-737", "Боинг-747", "Боинг-767", "Боинг-777" };

            // Максимальная вместимость
            string[] max_people_of_airctraft = { "96", "158", "214", "98", "198", "314", "181", "262", "435", "183", "259", "440", "114", "298", "252", "235" };
            
            // Пытаемся написать в фаил
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    // Записываем первую строку фаила, стирая весь предыдущий
                    sw.WriteLine(struct_file);
                }

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                {
                    // Создание обьекта генерации
                    Random rnd = new Random();

                    for (int i = 0; i < N; i++)
                    {
                        // Генерируем Номер рейса формат ББ ЦЦЦЦ (Б - буква, Ц - цифра)
                        string str_letter = "QWERTYUIOPASDFGHJKLZXCVBNM";
                        string str_numbers = "0123456789";

                        string str_number_aircraft;
                        str_number_aircraft = Convert.ToString(str_letter[rnd.Next(0, str_letter.Length)]) + Convert.ToString(str_letter[rnd.Next(0, str_letter.Length)]) + " " + Convert.ToString(str_numbers[rnd.Next(0, str_numbers.Length)]) + Convert.ToString(str_numbers[rnd.Next(0, str_numbers.Length)]) + Convert.ToString(str_numbers[rnd.Next(0, str_numbers.Length)]) + Convert.ToString(str_numbers[rnd.Next(0, str_numbers.Length)]);

                        // Выбираем случайный тип самолёта
                        int rnd_value = rnd.Next(0, types_of_aircraft.Length);
                        string str_type_of_aircraft = types_of_aircraft[rnd_value];

                        // Выбираем занятное количество мест
                        string str_max_people_of_airctraft = max_people_of_airctraft[rnd_value];
                        int int_str_max_people_of_airctraft = Convert.ToInt32(str_max_people_of_airctraft);
                        int min_people_of_airctraft = Convert.ToInt32(Math.Round(int_str_max_people_of_airctraft * .3, 0));
                        int people_of_airctraft = rnd.Next(min_people_of_airctraft, int_str_max_people_of_airctraft);

                        // Выбираем случайный город вылета
                        string[] str_towns = { "Абакан", "Анадырь", "Анапа", "Архангельск", "Астрахань", "Барнаул", "Белгород", "Благовещенск", "Братск", "Брянск", "Варандей", "Владивосток", "Владикавказ", "Волгоград", "Воронеж", "Грозный", "Екатеринбург", "Иркутск", "Казань", "Калининград", "Калуга", "Кемерово", "Краснодар", "Красноярск", "Курган", "Курск", "Липецк", "Магадан", "Магнитогорск", "Майкоп", "Махачкала", "Минеральные Воды", "Москва", "Москва", "Москва", "Москва", "Мурманск", "Нальчик", "Нижневартовск", "Нижнекамск", "Нижний Новгород", "Новокузнецк", "Новосибирск", "Омск", "Оренбург", "Орск", "Пермь", "Петрозаводск", "Петропавловск - Камчатский", "Провидения", "Псков", "Ростов - на - Дону", "Сабетта", "Самара", "Курумоч", "Санкт - Петербург", "Саранск", "Саратов", "Симферополь", "Сочи", "Ставрополь", "Сургут", "Сыктывкар", "Томск", "Тюмень", "Улан - Удэ", "Ульяновск", "Ульяновск", "Уфа", "Хабаровск", "Ханты - Мансийск", "Чебоксары", "Челябинск", "Череповец", "Чита", "Элиста", "Южно - Сахалинск", "Якутск", "Ярославль" };
                        string[] str_airports = { "Абакан", "Угольный", "Витязево", "Талаги", "Нариманово", "Барнаул", "Белгород", "Игнатьево", "Братск", "Брянск", "Варандей", "Кневичи", "Беслан", "Гумрак", "Чертовицкое", "Северный", "Кольцово", "Иркутск", "Казань", "Храброво", "Грабцево", "Кемерово", "Пашковский", "Емельяново", "Курган", "Восточный", "Липецк", "Сокол", "Магнитогорск", "Майкоп", "Уйташ", "Минеральные Воды", "Внуково", "Домодедово", "Жуковский", "Шереметьево", "Мурманск", "Нальчик", "Нижневартовск", "Бегишево", "Стригино", "Спиченково", "Толмачёво", "Омск-Центральный", "Оренбург-Центральный", "Орск", "Большое Савино", "Петрозаводск", "Елизово", "Бухта Провидения", "Кресты", "Платов", "Сабетта", "Самара", "Курумоч", "Пулково", "Саранск", "Гагарин", "Симферополь", "Адлер", "Шпаковское", "Сургут", "Сыктывкар", "Богашёво", "Рощино", "Мухино", "Баратаевка", "Восточный", "Уфа", "Хабаровск-Новый", "Ханты - Мансийск", "Чебоксары", "Баландино", "Череповец", "Кадала", "Элиста", "Хомутово", "Якутск", "Туношна" };
                        int k = rnd.Next(0, str_towns.Length);
                        string str_town = str_towns[k];
                        string str_airport = str_airports[k];

                        // Выбираем случайный город прилёта
                        int m = rnd.Next(0, str_towns.Length);
                        if (m == k)
                        {
                            m = rnd.Next(0, str_towns.Length);
                        }
                        string str_town2 = str_towns[m];
                        string str_airport2 = str_airports[m];

                        // Генерируем время отправления
                        DateTime thisDay = DateTime.Today;
                        DateTime departure_time = new DateTime(thisDay.Year, thisDay.Month, thisDay.Day, rnd.Next(0, 24), rnd.Next(0, 60), 0);

                        // Рассчитываем время прибытия
                        DateTime arrival_time = departure_time.AddMinutes(rnd.Next(60, 300));

                        // Записываем в фаил данные
                        sw.WriteLine(str_number_aircraft + "|" + str_type_of_aircraft + "|" + people_of_airctraft + "|" + str_town + "|" + str_airport + "|" + str_town2 + "|" + str_airport2 + "|" + departure_time + "|" + arrival_time + "|");
                    }
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        public class aircraft
        {
            // Номер рейса
            public string number_flight;
            // Тип самолёта
            public string type_of_aircraft;
            // Город вылета
            public string departure_city;
            // Аэропорт вылета
            public string departure_airport;
            // Город прилёта
            public string arrival_city;
            // Аэропорт прилёта
            public string arrival_airport;
            // Время отправления
            public string departure_time;
            // Время прибытия
            public string arrival_time;

            // Конструктор
            public aircraft()
            {

            }

            // Импорт данных
            public void import_data()
            {
                // Открываем фаил с данными
                // Путь к фаилу с данными рейсов
                string path = @"C:\Users\Vladislav\C# WPF projects\Table_of_airport\Table_of_airport\input.txt";
                // Переменная в которую мы запишем данные из фаила
                string result;

                // Чтение строки
                using (StreamReader reader = new StreamReader(path))
                {
                    result = reader.ReadToEnd();
                }

                // Преобразуем строку в массив
                char[] array = result.ToCharArray();

                // Пробегаемся по массиву
                for(int i = 0; i<array.Length; i++)
                {
                    // Пропускаем первую строку
                    if(i == 120)
                    {
                        // Формируем первый элемент нашей базы данных
                    }
                }
                Console.WriteLine(array);
            }

            // Деструктор
            ~aircraft()
            {

            }
        }

        // Функция запускается как только мы переключаемся на вкладку
        private void Table_of_air(object sender, RoutedEventArgs e)
        {
            // Запускаем таймеры для подачёта времени
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();


            air_flight.Text = "Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.Генерация данных.";
            Console.WriteLine("Начинаю читать фаилы");
            aircraft air = new aircraft();
            air.import_data();
            Console.WriteLine("3-4 закончили упражнение");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Время сейчас
            DateTime nowTime = DateTime.Now;
            //Console.WriteLine(nowTime.ToString("HH:mm:ss"));
            clock.Text = nowTime.ToString("HH:mm:ss");
        }
    }
}
