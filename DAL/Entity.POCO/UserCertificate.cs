using System;

namespace DAL.POCO
{
    /// <summary>
    /// Пользовательский сертификат
    /// </summary>
    public class UserCertificate
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сертификат
        /// </summary>
        public string Certificate { get; set; }

        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Пароль для экспорта сертификата
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Был ли сертификат установлен на сервере
        /// </summary>
        public bool IsInstalled { get; set; }

        /// <summary>
        /// Пользователь, ассоциированный с сертификатом.
        /// </summary>
        public string UserName { get; set; }
    }
}
