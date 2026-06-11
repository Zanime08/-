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
using System.Text.RegularExpressions; // Для проверки текста по шаблону (регулярные выражения)

namespace WpfApp3
{
    /// <summary>
    /// Окно для добавления или редактирования читателя
    /// </summary>
    public partial class AddReader : Window
    {
        public AddReader()
        {
            InitializeComponent(); // Загружаем интерфейс
        }

        /// <summary>
        /// Кнопка "Сохранить" - проверяем данные и закрываем окно
        /// </summary>
        private void SaveButton2_Click(object sender, RoutedEventArgs e)
        {
            bool hasErrors2 = false; // Флаг: есть ли ошибки

            // Красная кисть для подсветки ошибок
            var errorBrush2 = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            // Обычная кисть (тёмно-серый цвет)
            var normalBrush2 = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x3F, 0x3F, 0x46));

            // ========== Проверка ФИО ==========
            // Условие: поле пустое ИЛИ содержит не только буквы и пробелы
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text) || !Regex.IsMatch(FullNameTextBox.Text, @"^[a-zA-Zа-яА-ЯёЁ\s]+$"))
            {
                FullNameTextBox.BorderBrush = errorBrush2; // Красная рамка
                hasErrors2 = true; // Запоминаем ошибку
            }
            else
            {
                FullNameTextBox.BorderBrush = normalBrush2; // Всё хорошо
            }

            // ========== Проверка Телефона ==========
            // Условие: поле пустое ИЛИ содержит не цифры, пробелы, дефисы, скобки и плюс
            if (string.IsNullOrWhiteSpace(PhoneTextBox.Text) || !Regex.IsMatch(PhoneTextBox.Text, @"^\+?[0-9\s\-\(\)]+$"))
            {
                PhoneTextBox.BorderBrush = errorBrush2; // Красная рамка
                hasErrors2 = true;
            }
            else
            {
                PhoneTextBox.BorderBrush = normalBrush2;
            }

            // ========== Если есть ошибки ==========
            if (hasErrors2 == true)
            {
                // Показываем понятное сообщение пользователю
                MessageBox.Show("Пожалуйста, проверьте правильность ввода!\n\n" +
                                "• В имени не должно быть цифр.\n" +
                                "• В номере телефона не должно быть букв.",
                                "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Не закрываем окно
            }

            // Всё правильно - закрываем окно с успехом
            this.DialogResult = true;
        }

        /// <summary>
        /// Кнопка "Отмена" - закрываем без сохранения
        /// </summary>
        private void CancelButton2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Отмена
        }
    }
}
