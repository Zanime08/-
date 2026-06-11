using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp3
{
    /// <summary>
    /// Класс "Читатель" - хранит информацию о читателе библиотеки
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// Уникальный идентификатор читателя (ID)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО читателя (Фамилия Имя Отчество)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Номер читательского билета (генерируется автоматически)
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Номер телефона читателя
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Дата регистрации в библиотеке (устанавливается автоматически)
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
