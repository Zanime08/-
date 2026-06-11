using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp3
{
    /// <summary>
    /// Класс "Книга" - хранит информацию об одной книге
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Уникальный идентификатор книги (ID)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Год издания (должен быть от 1450 до 2026)
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Жанр книги (роман, детектив, фантастика и т.д.)
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Количество экземпляров в библиотеке
        /// </summary>
        public int Quantity { get; set; }
    }
}
