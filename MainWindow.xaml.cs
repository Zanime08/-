using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Окно авторизации - проверяет логин и пароль перед входом в систему
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Загружаем интерфейс из XAML
        }

        /// <summary>
        /// Кнопка "Вход" - проверяет логин и пароль
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введённые данные
            string login = Login.Text;           // Логин из текстового поля
            string password = Password.Password; // Пароль из поля PasswordBox

            // Проверка по ТЗ: логин "librarian", пароль "book2026"
            if (login == "librarian" && password == "book2026")
            {
                // Успех - открываем главное окно
                Books Window1 = new Books();
                Window1.Show();     // Показываем окно с книгами
                this.Close();       // Закрываем окно авторизации
            }
            else
            {
                // Ошибка - показываем сообщение и очищаем поля
                MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации");
                Login.Clear();      // Очищаем поле логина
                Password.Clear();   // Очищаем поле пароля
            }
        }

        /// <summary>
        /// Кнопка "Выход" - закрывает приложение
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрываем окно (и всё приложение)
        }
    }
}