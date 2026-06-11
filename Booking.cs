using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp3
{
    /// <summary>
    /// Класс "Бронирование" - хранит информацию о том, кто и какую книгу забронировал
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Номер читательского билета (кто забронировал)
        /// </summary>
        public string ReaderTicket { get; set; }

        /// <summary>
        /// ФИО читателя (для удобного отображения)
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// Название забронированной книги
        /// </summary>
        public string BookTitle { get; set; }

        /// <summary>
        /// Дата и время бронирования
        /// </summary>
        public DateTime BookingDate { get; set; }
    }
}
