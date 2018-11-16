using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace AnimationApp
{
    public partial class Form1 : Form
    {
        // Размерность графа
        public const int GraphWidth = 10;
        public const int GraphHeight = 4;

        // Размеры фигуры в пикселях
        public const int RectangleWidth = 100;
        public const int RectangleHeight = 100;

        // Количество пикселей на шаг графа
        public double PixelMultiplierX { get; set; }
        public double PixelMultiplierY { get; set; }

        // Первоначальные координаты фигуры внутри графа
        public double currentX { get; set; } = 0;
        public double currentY { get; set; } = 0;

        // Сдвиг оси координат в писклях внутри формы
        public int ShiftX { get; set; } = 0;
        public int ShiftY { get; set; } = 0;

        // Шаг отрисовки по оси X
        public double Step { get; set; } = 0.05;

        public System.Timers.Timer Timer { get; set; } = new System.Timers.Timer();
        public Pen Pen { get; set; } = new Pen(Color.FromArgb(255, 0, 0, 0));

        public Form1()
        {
            InitializeComponent();

            // Вычисляем шаг изменения координат в пикселях относительно размеров формы
            PixelMultiplierX= Width / GraphWidth;
            PixelMultiplierY = Height / GraphHeight;

            // Смещаем граф, чтобы фигура оказалась посередине формы
            ShiftY = Height / 2 - RectangleHeight / 2;

            // Задаем таймер
            Timer.Elapsed += (sender, e) => OnTimedEvent();
            Timer.Interval = 10;
            Timer.Enabled = true;
        }
  
        private void OnTimedEvent()
        {
            // Считаем новые координаты 
            currentX += Step;
            currentY = Math.Sin(currentX);

            DrawRectangle();
        }


        private void DrawRectangle()
        {
            var g = CreateGraphics();
            g.Clear(Color.White);
            g.DrawRectangle(Pen, new Rectangle(
                // Округляем результаты X по модулю размерности координатной доски, 
                // чтобы фигура отрисовывалась в цикле и не уплывала за пределы формы
                Convert.ToInt32(currentX % GraphWidth * PixelMultiplierX + ShiftX), 
                Convert.ToInt32(currentY * PixelMultiplierY + ShiftY),
                RectangleWidth,
                RectangleHeight
            ));
        }
    }
}
