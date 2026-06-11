using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для окна AddBook.xaml
    /// Окно используется для добавления новой книги или редактирования существующей
    /// </summary>
    public partial class AddBook : Window
    {
        /// <summary>
        /// Конструктор окна. Инициализирует компоненты интерфейса.
        /// </summary>
        public AddBook()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Сохранить".
        /// Выполняет валидацию всех полей ввода.
        /// При успешной валидации устанавливает DialogResult = true и закрывает окно.
        /// При ошибке подсвечивает некорректные поля красным и выводит сообщение.
        /// </summary>
        /// <param name="sender">Источник события (кнопка Сохранить)</param>
        /// <param name="e">Параметры события</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Флаг, указывающий на наличие ошибок валидации
            bool hasErrors = false;

            // Кисть для красной рамки (ошибка валидации)
            var errorBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);

            // Кисть для обычной рамки (нормальное состояние) - цвет #3F3F46 (тёмно-серый)
            var normalBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x3F, 0x3F, 0x46));

            // ========== Валидация поля "Название книги" ==========
            // Проверка: поле не должно быть пустым или состоять только из пробелов
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleTextBox.BorderBrush = errorBrush;  // Подсветка красным
                hasErrors = true;                       // Устанавливаем флаг ошибки
            }
            else
            {
                TitleTextBox.BorderBrush = normalBrush; // Возвращаем нормальный цвет рамки
            }

            // ========== Валидация поля "Автор" ==========
            // Проверка: поле не должно быть пустым или состоять только из пробелов
            if (string.IsNullOrWhiteSpace(AuthorTextBox.Text))
            {
                AuthorTextBox.BorderBrush = errorBrush;
                hasErrors = true;
            }
            else
            {
                AuthorTextBox.BorderBrush = normalBrush;
            }

            // ========== Валидация поля "Жанр" ==========
            // Проверка: поле не должно быть пустым или состоять только из пробелов
            if (string.IsNullOrWhiteSpace(GenreTextBox.Text))
            {
                GenreTextBox.BorderBrush = errorBrush;
                hasErrors = true;
            }
            else
            {
                GenreTextBox.BorderBrush = normalBrush;
            }

            // ========== Валидация поля "Год издания" ==========
            // Проверка 1: значение должно быть целым числом (int.TryParse)
            // Проверка 2: год должен быть в диапазоне от 1450 до 2026 включительно
            if (!int.TryParse(YearTextBox.Text, out int year) || year <= 1449 || year >= 2027)
            {
                YearTextBox.BorderBrush = errorBrush;
                hasErrors = true;
            }
            else
            {
                YearTextBox.BorderBrush = normalBrush;
            }

            // ========== Валидация поля "Количество экземпляров" ==========
            // Проверка 1: значение должно быть целым числом (int.TryParse)
            // Проверка 2: количество не может быть отрицательным
            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity < 0)
            {
                QuantityTextBox.BorderBrush = errorBrush;
                hasErrors = true;
            }
            else
            {
                QuantityTextBox.BorderBrush = normalBrush;
            }

            // ========== Если есть ошибки валидации ==========
            if (hasErrors == true)
            {
                // Выводим предупреждающее сообщение пользователю
                MessageBox.Show("Пожалуйста, заполните все поля корректно. Ошибочные поля подсвечены красным.",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Прерываем выполнение метода — окно не закрывается
            }

            // ========== Валидация пройдена успешно ==========
            // Устанавливаем DialogResult = true, что сигнализирует вызывающему коду (главному окну)
            // о том, что пользователь подтвердил ввод и данные можно сохранять
            this.DialogResult = true;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Отмена".
        /// Закрывает окно без сохранения данных.
        /// </summary>
        /// <param name="sender">Источник события (кнопка Отмена)</param>
        /// <param name="e">Параметры события</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем DialogResult = false, что сигнализирует вызывающему коду
            // о том, что пользователь отменил операцию
            this.DialogResult = false;
        }
    }
}