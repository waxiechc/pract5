using System;

namespace CinemaProject
{
    class Viewer
    {
        public int ViewerNumber { get; }

        public Viewer(int number)
        {
            ViewerNumber = number;
        }
    }

    class Cinema
    {
        public delegate void CinemaHandler();
        public event CinemaHandler NotPlaces;

        private int totalSeats;
        private int currentViewers;

        public Cinema(int seats)
        {
            totalSeats = seats;
            currentViewers = 0;
        }

        public void PushViewer(Viewer viewer)
        {
            if (currentViewers < totalSeats)
            {
                currentViewers++;
                Console.WriteLine($"Глядач {viewer.ViewerNumber} зайняв своє мiсце {currentViewers}.");
            }

            if (currentViewers == totalSeats)
            {
                Console.WriteLine("Зал повнiстю заповнений.");
                NotPlaces?.Invoke();
            }
        }
    }

    class Security
    {
        public delegate void SecurityHandler();
        public event SecurityHandler SwitchOff;

        public void CloseZal()
        {
            Console.WriteLine("Черговий закрив зал.");
            SwitchOff?.Invoke();
        }
    }

    class Light
    {
        public delegate void LightHandler();
        public event LightHandler Begin;

        public void Turn()
        {
            Console.WriteLine("Вимикаємо свiтло!");
            Begin?.Invoke();
        }
    }

    class Hardware
    {
        private string movieTitle;

        public Hardware(string title)
        {
            movieTitle = title;
        }

        public void FilmOn()
        {
            Console.WriteLine($"Починається фiльм {movieTitle}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Введення кількості місць і назви фільму
            Console.Write("Введiть кiлькiсть мiсць у залi: ");
            int seats = int.Parse(Console.ReadLine());

            Console.Write("Введiть назву фiльму: ");
            string movieTitle = Console.ReadLine();

            // Створення об'єктів класів
            Cinema cinema = new Cinema(seats);
            Security security = new Security();
            Light light = new Light();
            Hardware hardware = new Hardware(movieTitle);

            // Підписка на події
            cinema.NotPlaces += security.CloseZal;
            security.SwitchOff += light.Turn;
            light.Begin += hardware.FilmOn;

            // Заповнення зали глядачами
            for (int i = 1; i <= seats; i++)
            {
                Viewer viewer = new Viewer(i);
                cinema.PushViewer(viewer);
            }

            Console.ReadKey();
        }
    }
}
