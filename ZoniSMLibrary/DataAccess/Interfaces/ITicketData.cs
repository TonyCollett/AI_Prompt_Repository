
namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface ITicketData
{
    Task CreateTicketAsync(Ticket ticket);
    Task CreateMultipleTicketsAsync(IEnumerable<Ticket> tickets);
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<List<Ticket>> GetAllActiveTicketsAsync();
    Task<Ticket> GetTicketAsync(string id);
    Task<List<Ticket>> GetUserCreatedTicketsAsync(User user);
    Task<List<Ticket>> GetUserAssignedTicketsAsync(User user);
    Task UpdateTicket(Ticket ticket);
    Task SetStatusAsync(Ticket ticket, Status status);
    Task CloseTicketsAsync(int numberOfDays);
    Task<int> GetOpenTicketCountAsync();
    Task<int> GetClosedTicketCountAsync();
}