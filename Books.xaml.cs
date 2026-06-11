using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
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
    /// Главное окно приложения (книги, читатели, бронирование)
    /// </summary>
    public partial class Books : Window
    {
        // Коллекции для хранения данных (автоматически обновляют интерфейс)
        public ObservableCollection<Book> GetBooks { get; set; }      // Список книг
        public ObservableCollection<Reader> GetReaders { get; set; }  // Список читателей
        public ObservableCollection<Booking> GetBookings { get; set; } // Список броней

        /// <summary>
        /// Конструктор - инициализирует окно и коллекции
        /// </summary>
        public Books()
        {
            InitializeComponent();

            // Создаём коллекции и привязываем к таблицам
            GetBooks = new ObservableCollection<Book>();
            BooksDataGrid.ItemsSource = GetBooks;

            GetReaders = new ObservableCollection<Reader>();
            ReadersDataGrid.ItemsSource = GetReaders;

            GetBookings = new ObservableCollection<Booking>();
            BookingsDataGrid.ItemsSource = GetBookings;
        }

        /// <summary>
        /// Добавление новой книги
        /// </summary>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBook addbook = new AddBook();
            addbook.Owner = this; // Главное окно - владелец диалога

            // Если пользователь нажал "Сохранить"
            if (addbook.ShowDialog() == true)
            {
                // Создаём новую книгу из введённых данных
                Book newBook = new Book
                {
                    Id = GetBooks.Count + 1,  // ID = следующий номер
                    Title = addbook.TitleTextBox.Text,
                    Author = addbook.AuthorTextBox.Text,
                    Genre = addbook.GenreTextBox.Text,
                    Year = int.Parse(addbook.YearTextBox.Text),
                    Quantity = int.Parse(addbook.QuantityTextBox.Text)
                };
                GetBooks.Add(newBook); // Добавляем в коллекцию (таблица обновится сама)
            }
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Book? selectedBook = BooksDataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {
                // Запрашиваем подтверждение
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить книгу \"{selectedBook.Title}\"?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    GetBooks.Remove(selectedBook); // Удаляем из коллекции
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу из таблицы для удаления.", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Подтверждение выхода из приложения
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Отменяем закрытие окна
            }
        }

        /// <summary>
        /// Редактирование книги
        /// </summary>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Book? selectedBook = BooksDataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {
                // Открываем окно редактирования с предзаполненными полями
                AddBook editWindow = new AddBook();
                editWindow.Owner = this;
                editWindow.Title = "Редактирование книги";
                editWindow.TitleTextBox.Text = selectedBook.Title;
                editWindow.AuthorTextBox.Text = selectedBook.Author;
                editWindow.GenreTextBox.Text = selectedBook.Genre;
                editWindow.YearTextBox.Text = selectedBook.Year.ToString();
                editWindow.QuantityTextBox.Text = selectedBook.Quantity.ToString();

                if (editWindow.ShowDialog() == true)
                {
                    // Обновляем данные выбранной книги
                    selectedBook.Title = editWindow.TitleTextBox.Text;
                    selectedBook.Author = editWindow.AuthorTextBox.Text;
                    selectedBook.Genre = editWindow.GenreTextBox.Text;
                    selectedBook.Year = int.Parse(editWindow.YearTextBox.Text);
                    selectedBook.Quantity = int.Parse(editWindow.QuantityTextBox.Text);
                    BooksDataGrid.Items.Refresh(); // Принудительно обновляем таблицу
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите книгу из таблицы для редактирования.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// Добавление читателя (номер билета генерируется автоматически)
        /// </summary>
        private void AddButton2_Click(object sender, RoutedEventArgs e)
        {
            AddReader addWindow = new AddReader();
            addWindow.Owner = this;

            if (addWindow.ShowDialog() == true)
            {
                Random random = new Random();
                int nextOrderNumber = GetReaders.Count + 1;
                // Генерируем номер билета: (порядковый номер * 100) + случайное число от 1 до 99
                int generatedTicket = (nextOrderNumber * 100) + random.Next(1, 100);

                Reader newReader = new Reader
                {
                    Id = nextOrderNumber,
                    FullName = addWindow.FullNameTextBox.Text,
                    TicketNumber = generatedTicket.ToString(),
                    Phone = addWindow.PhoneTextBox.Text,
                    RegistrationDate = DateTime.Now // Текущая дата регистрации
                };
                GetReaders.Add(newReader);
            }
        }

        /// <summary>
        /// Редактирование читателя
        /// </summary>
        private void EditButton2_Click(object sender, RoutedEventArgs e)
        {
            Reader? selectedReader = ReadersDataGrid.SelectedItem as Reader;
            if (selectedReader != null)
            {
                AddReader editReader = new AddReader();
                editReader.Owner = this;
                editReader.Title = "Редактирование читателя";
                editReader.FullNameTextBox.Text = selectedReader.FullName;
                editReader.PhoneTextBox.Text = $"+{selectedReader.Phone}"; // Добавляем + для редактирования

                if (editReader.ShowDialog() == true)
                {
                    selectedReader.FullName = editReader.FullNameTextBox.Text;
                    selectedReader.Phone = editReader.PhoneTextBox.Text;
                    ReadersDataGrid.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите читателя из таблицы для редактирования.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Удаление читателя
        /// </summary>
        private void DeleteButton2_Click(object sender, RoutedEventArgs e)
        {
            Reader? selectedReader = ReadersDataGrid.SelectedItem as Reader;

            if (selectedReader != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить читателя \"{selectedReader.FullName}\"?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    GetReaders.Remove(selectedReader);
                }
            }
            else
            {
                MessageBox.Show("Выберите читателя из таблицы для удаления.", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /// <summary>
        /// Создание бронирования
        /// </summary>
        private void AddBookingButton_Click(object sender, RoutedEventArgs e)
        {
            AddBooking addWindow = new AddBooking();
            addWindow.Owner = this;

            // Передаём списки в выпадающие списки диалогового окна
            addWindow.ReaderComboBox.ItemsSource = GetReaders;
            addWindow.ReaderComboBox.DisplayMemberPath = "FullName"; // Показываем ФИО

            addWindow.BookComboBox.ItemsSource = GetBooks;
            addWindow.BookComboBox.DisplayMemberPath = "Title"; // Показываем название книги

            if (addWindow.ShowDialog() == true)
            {
                Reader? selectedReader = addWindow.ReaderComboBox.SelectedItem as Reader;
                Book? selectedBook = addWindow.BookComboBox.SelectedItem as Book;

                // Проверяем наличие книги
                if (selectedBook.Quantity <= 0)
                {
                    MessageBox.Show($"Книги \"{selectedBook.Title}\" сейчас нет в наличии! Все экземпляры выданы.",
                                    "Недоступно", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Уменьшаем количество экземпляров на 1
                selectedBook.Quantity--;
                BooksDataGrid.Items.Refresh();

                // Создаём запись о бронировании
                Booking newBooking = new Booking
                {
                    ReaderTicket = selectedReader.TicketNumber,
                    ReaderName = selectedReader.FullName,
                    BookTitle = selectedBook.Title,
                    BookingDate = DateTime.Now
                };
                GetBookings.Add(newBooking);
            }
        }

        /// <summary>
        /// Отмена бронирования (возвращаем книгу)
        /// </summary>
        private void DeleteBookingButton_Click(object sender, RoutedEventArgs e)
        {
            Booking? selectedBooking = BookingsDataGrid.SelectedItem as Booking;

            if (selectedBooking != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить бронь?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Ищем книгу по названию и возвращаем экземпляр
                    foreach (var book in GetBooks)
                    {
                        if (book.Title == selectedBooking.BookTitle)
                        {
                            book.Quantity++; // Увеличиваем количество обратно
                            break;
                        }
                    }
                    BooksDataGrid.Items.Refresh(); // Обновляем таблицу книг
                    GetBookings.Remove(selectedBooking); // Удаляем бронь
                }
            }
            else
            {
                MessageBox.Show("Выберите бронирование для отмены.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
