namespace AstralDelivery.Models
{
    /// <summary>
    /// Модель для создания токена восстановления
    /// </summary>
    public class RecoveryTokenCreationModel
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
    }
}
