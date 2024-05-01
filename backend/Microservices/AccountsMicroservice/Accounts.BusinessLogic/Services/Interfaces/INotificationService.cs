namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, string message);
    }
}
