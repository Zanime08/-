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
    /// Окно для бронирования книги читателем
    /// </summary>
    public partial class AddBooking : Window
    {
        public AddBooking()
        {
            InitializeComponent(); // Загружаем интерфейс из XAML
        }

        /// <summary>
        /// Кнопка "Подтвердить" - создаём бронь
        /// </summary>
        private void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что выбран и читатель, и книга
            if (ReaderComboBox.SelectedItem == null || BookComboBox.SelectedItem == null)
            {
                // Если что-то не выбрано - показываем ошибку
                MessageBox.Show("Пожалуйста, выберите читателя и книгу из списков!",
                                "Ошибка бронирования", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Выходим, не закрывая окно
            }

            // Всё выбрано - закрываем окно с успешным результатом
            this.DialogResult = true;
        }

        /// <summary>
        /// Кнопка "Отмена" - закрываем без бронирования
        /// </summary>
        private void CancelBookingButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Отмена
        }
    }
}