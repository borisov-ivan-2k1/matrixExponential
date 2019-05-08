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

namespace MatrixExponential
{

    public class MatrixExp
    {
        public double[,] matrix = new double[2, 2];

        public double s;

        // Метод для вычисления суммы матриц:
        public static MatrixExp operator +(MatrixExp A, MatrixExp N)
        {
            MatrixExp T = new MatrixExp();
            int i, j;
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    T.matrix[i, j] = A.matrix[i, j] + N.matrix[i, j];
                }
            }

            return T;
        }
        // Метод деление матрицы на число
        static public MatrixExp operator /(MatrixExp A, double x)
        {
            int i, j;
            for (i = 0; i < 2; i++)
                for (j = 0; j < 2; j++)
                    A.matrix[i, j] /= x;
            return A; //this
        }




        // Метод для вычисления произведения матриц:
        static public MatrixExp operator *(MatrixExp A, MatrixExp N)
        {
            MatrixExp T = new MatrixExp();
            int i, j, k;
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < 2; j++)
                {
                    T.matrix[i, j] = 0;
                    for (k = 0; k < 2; k++)
                    {
                        T.matrix[i, j] += A.matrix[i, k] * N.matrix[k, j];
                    }
                }
            }
            return T;
        }




        // Метод для вычисления матричной экспоненты:
        public MatrixExp mExp(MatrixExp N)
        {
            MatrixExp E = new MatrixExp();
            MatrixExp T = new MatrixExp();
            //MatrixExp T, Q;
            // Начальное значение - единичная матрица:
            //A = new MatrixExp();
            //B = new MatrixExp();


            E.matrix[0, 0] = 1;
            E.matrix[1, 1] = 1;
            E.matrix[0, 1] = 0;
            E.matrix[1, 0] = 0;

            T.matrix = E.matrix;

            MatrixExp Eold = new MatrixExp();
            //MatrixExp t = new MatrixExp();


            // Вычисление ряда для экспоненты:

            int i = 0;
            do
            {
                i++;
                Eold.matrix = E.matrix;
                T = (T * N) / i;
                E = E + T;
            }
            while (i < 10);

            return E;
        }
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public void button_Click(object sender, RoutedEventArgs e)
        {


            //считывание значения матрицы из  textBox
            try
            {
                double vi00 = Convert.ToDouble(i00.Text);
                double vi01 = Convert.ToDouble(i01.Text);
                double vi10 = Convert.ToDouble(i10.Text);
                double vi11 = Convert.ToDouble(i11.Text);

                MatrixExp A = new MatrixExp();

                Console.WriteLine("Матрица A:");
                A.matrix[0, 0] = vi00;
                A.matrix[0, 1] = vi01;
                A.matrix[1, 0] = vi10;
                A.matrix[1, 1] = vi11;


                A = A.mExp(A);


                r00.Text = Convert.ToString(A.matrix[0, 0]);
                r01.Text = Convert.ToString(A.matrix[0, 1]);
                r10.Text = Convert.ToString(A.matrix[1, 0]);
                r11.Text = Convert.ToString(A.matrix[1, 1]);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Вы ввели не валидные данные, повторите снова");
                i00.Clear();
                i01.Clear();
                i10.Clear();
                i11.Clear();
            }
            
        }
    }
}
