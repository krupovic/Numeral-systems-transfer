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
using static System.Math;
using System.Diagnostics;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Copyright.Text = "Copyright 2022 Petr Kruppa, Kuzma Skorkin\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files(the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
        }

        private void ButtonClicked (object sender, RoutedEventArgs e)
        {
            string alphabet = "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZ";
            int n = int.Parse(N_number.Text);
            int m = int.Parse(M_number.Text);
            string x = X_number.Text;
            bool correct = true;
            

            if ((n <= 1 || n > 32) && (m <= 1 || m > 32))
            {
                MessageBox.Show("Число n не входит в требуемый промежуток!\nЧисло m не входит в требуемый промежуток!");
                N_number.Text = "";
                M_number.Text = "";
                correct = false;
            }
            else if (n <= 1 || n > 32)
            {
                MessageBox.Show("Число n не входит в требуемый промежуток!");
                N_number.Text = "";
                correct = false;
            }
            else if (m <= 1 || m > 32)
            {
                MessageBox.Show("Число m не входит в требуемый промежуток!");
                M_number.Text = "";
                correct = false;
            }

            foreach (char i in x)
            {
                if (alphabet.IndexOf(i) == -1 || alphabet.IndexOf(i) >= n)
                {
                    MessageBox.Show("Введённые данные некорректны!");
                    correct = false;
                    X_number.Text = "";
                    break;
                }
            }

            if (correct) Answer.Content = ($"Your answer is: {Transfer(n, m, x)}");

            

        }

        string Transfer(int n, int m, string x)
        {
            char Repr1(int x) => "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"[x];
            int Repr2(char c) => "0123456789ABCDEFGHJKLMNOPQRSTUVWXYZ".IndexOf(c);

            int NToDec(string s, int n)
            {
                int k = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    k += Repr2(s[i]) * (int)Pow(n, s.Length - i - 1);
                }
                return k;
            }

            string DecToM(int k, int m)
            {
                List<char> ans = new();
                while (k != 0)
                {
                    ans.Add(Repr1(k % m));
                    k /= m;
                };

                ans.Reverse();
                return String.Join('\0', ans);
            }

            return DecToM(NToDec(x, n), m);

        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
