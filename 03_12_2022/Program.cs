using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _03_12_2022
{
    public interface ISwitch
    {
        void Turn_on();
        void Turn_off();
    }
    abstract class Home_appliances : ISwitch
    {
        public bool On { get; protected set; }
        public string Name { get; protected set; }
        public bool Busy { get; protected set; }
        public void Turn_on()
        {
            if(!On)
            {
                On = true;
                Console.WriteLine($"{Name} включен\n");
            }
        }
        public void Turn_off()
        {
            if(On)
            {
                if (!Busy)
                {
                    On = false;
                    Console.WriteLine($"{Name} выключается\n");
                }
                else Console.WriteLine($"{Name} в процессе работы\n");
            }
        }
        public void Show_status()
        {
            if(On)
            {
                if (Busy) Console.WriteLine($"{Name} в процессе работы\n");
                else Console.WriteLine($"{Name} включен\n");
            }
            else Console.WriteLine($"{Name} выключен\n");
        }
        abstract public void Start_work();
        public void Menu()
        {
            int checked_variant;
            do
            {
                do
                {
                    Show_status();
                    Console.WriteLine("1. Включить");
                    Console.WriteLine("2. Выключить");
                    Console.WriteLine("3. Начать работу");
                    Console.WriteLine("0. Выйти\n");
                    checked_variant = Convert.ToInt32(Console.ReadLine());
                } while (checked_variant < 0 || checked_variant > 3);
                switch (checked_variant)
                {
                    case 1:
                        Turn_on();
                        break;
                    case 2:
                        Turn_off();
                        break;
                    case 3:
                        Task.Factory.StartNew(Start_work);
                        break;
                }
            } while (checked_variant != 0);
        }
    }
    class Washing_machine : Home_appliances
    {
        public Washing_machine()
        {
            Name = "Стиральная машина";
            On = false;
            Busy = false;
        }
        public override void Start_work()
        {
            if (On)
            {
                if (!Busy)
                {
                    Console.WriteLine("Начинаю стирать\n");
                    Busy = true;
                    Thread.Sleep(60000);
                    Console.WriteLine("Стирка закончена\n");
                    Busy = false;
                }
                else Console.WriteLine($"{Name} уже работает\n");
            }
            else Console.WriteLine($"{Name} выключена\n");
        }
    }
    class Dishwasher : Home_appliances
    {
        public Dishwasher()
        {
            Name = "Посудомоечная машина";
            On = false;
        }
        public override void Start_work()
        {
            if(On)
            {
                if (!Busy)
                {
                    Console.WriteLine("Начинаю мыть посуду\n");
                    Busy = true;
                    Thread.Sleep(60000);
                    Console.WriteLine("Посуда помыта\n");
                    Busy = false;
                }
                else Console.WriteLine($"{Name} уже работает\n");
            }
            else Console.WriteLine($"{Name} выключена\n");
        }
    }
    class Robot_vacuum_cleaner : Home_appliances
    {
        public Robot_vacuum_cleaner()
        {
            Name = "Самоходный пылесос";
            On = false;
        }
        public override void Start_work()
        {
            if(On)
            {
                if (!Busy)
                {
                    Console.WriteLine("Начинаю самоходно пылесосить\n");
                    Busy = true;
                    Thread.Sleep(60000);
                    Console.WriteLine("Пропылесошено\n");
                    Busy = false;
                }
                else Console.WriteLine($"{Name} уже работает\n");
            }
            else Console.WriteLine($"{Name} выключен\n");
        }
    }
    internal class Program
    {
        static void Main()
        {
            bool continue_working = true;
            List<Home_appliances> home_appliances = new List<Home_appliances>
            {
                new Washing_machine(),
                new Dishwasher(),
                new Robot_vacuum_cleaner()
            };
            do
            {
                int checked_variant;
                do
                {
                    foreach (var home_appliance in home_appliances) home_appliance.Show_status();
                    Console.WriteLine("Выберите пункт меню, 0 для выхода: ");
                    checked_variant = Convert.ToInt32(Console.ReadLine());
                } while (checked_variant < 0 || checked_variant > home_appliances.Count);
                if (checked_variant == 0)
                {
                    continue_working = false;
                    foreach (var home_appliance in home_appliances) if (home_appliance.On) continue_working = true;
                    if (continue_working) Console.WriteLine("В тебе сражаються два котика: темно - свекло. Выключайте электроприборы\n");
                }
                else home_appliances[checked_variant - 1].Menu();
            } while (continue_working);
        }
    }
}