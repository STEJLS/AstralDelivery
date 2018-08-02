namespace AstralDelivery.Utils.ExceptionFilter
{
    /// <summary>
    /// Объект описывающий ошибку
    /// </summary>
    public class ResponseMessage
    {
        public bool Сompleted { get; set; }
        public string Message { get; set; }
        public string Raw { get; set; }

        public ResponseMessage()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="complited"> Указывает завершено ли действие </param>
        /// <param name="message"> Сообщение с ошибкой </param>
        /// <param name="raw"> стэк </param>
        public ResponseMessage(bool complited, string message, string raw)
        {
            Сompleted = complited;
            Message = message;
            Raw = raw;
        }
    }
}
