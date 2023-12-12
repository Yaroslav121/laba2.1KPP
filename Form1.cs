using System;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private TextBox xTextBox;
        private TextBox nTextBox;
        private Button calculateButton;
        private Label resultLabel;

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            xTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(100, 20),
                Text = "0"
            };
            Controls.Add(xTextBox);

            nTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 40),
                Size = new System.Drawing.Size(100, 20),
                Text = "1"
            };
            Controls.Add(nTextBox);

            calculateButton = new Button
            {
                Text = "Обчислити",
                Location = new System.Drawing.Point(10, 70)
            };
            calculateButton.Click += CalculateButton_Click;
            Controls.Add(calculateButton);

            resultLabel = new Label
            {
                Text = "Результат:",
                Location = new System.Drawing.Point(10, 100)
            };
            Controls.Add(resultLabel);

            // Додамо обробник події Load прямо тут
            this.Load += Form1_Load;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(xTextBox.Text, out double x) && int.TryParse(nTextBox.Text, out int n))
            {
                // Запуск дочірнього потоку для обчислення суми
                Thread thread = new Thread(() =>
                {
                    double result = CalculateExpression(x, n);
                    // Виведення результату в головному потоці
                    this.Invoke((MethodInvoker)delegate
                    {
                        resultLabel.Text = $"Результат виразу: {result:F5}";
                    });
                });
                thread.Start();
            }
            else
            {
                resultLabel.Text = "Некоректні вхідні дані. Введіть правильні значення x та n.";
            }
        }

        private double CalculateExpression(double x, int n)
        {
            double sum = 0.0;

            for (int i = 1; i <= n; i++)
            {
                double term = Math.Pow(-1, i + 1) * Math.Pow(x, i) / i;
                sum += term;
            }

            return sum;
        }

        // Обробник події Load для ініціалізації форми
        private void Form1_Load(object sender, EventArgs e)
        {
            // Можна залишити цей метод порожнім або додати код ініціалізації, якщо потрібно.
        }
    }
}
