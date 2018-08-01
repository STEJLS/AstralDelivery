using System.Text;
using AstralDelivery.Domain.Models;

namespace AstralDelivery.Domain.Utils
{
    /// <summary>
    /// Класс, предоставляющий общий доступ к переменной "Salt" конфига в виде массива байт
    /// </summary>
    public class SaltManager
    {
        private readonly byte[] _salt;

        /// <summary>
        /// Конструктор, принимающий один параметр объект ConfigurationOptions
        /// </summary>
        /// <param name="options"> <see cref="ConfigurationOptions"/> </param>
        public SaltManager(ConfigurationOptions options)
        {
            _salt = Encoding.UTF8.GetBytes(options.Salt);
        }

        /// <summary>
        /// Возвращет соль в виде массива байт
        /// </summary>
        /// <returns> Соль </returns>
        public byte[] Get() => _salt;
    }
}
