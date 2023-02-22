using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ZoniSMLibrary.DataAccess;
public class MongoTicketData : ITicketData
{
    private readonly IDbConnection _db;
    private readonly IMemoryCache _cache;
    private readonly IMongoCollection<Ticket> _ticketCollection;
    private const string CacheName = "TicketData";

    public MongoTicketData(IDbConnection db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
        _ticketCollection = db.TicketCollection;
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        var output = _cache.Get<List<Ticket>>(CacheName);
        if (output is null)
        {
            var results = await _ticketCollection.FindAsync(t => t.Archived == false);
            output = results.ToList();

            _cache.Set(CacheName, output, TimeSpan.FromMinutes(1));
        }

        return output;
    }

    public async Task<List<Ticket>> GetAllActiveTicketsAsync()
    {
        var output = _cache.Get<List<Ticket>>(CacheName + "+ActiveItems");
        if (output is null)
        {
            var results = await _ticketCollection.FindAsync(t => t.Status.Name == "New" || t.Status.Name.Contains("Pending"));
            output = results.ToList();

            _cache.Set(CacheName + "+ActiveItems", output, TimeSpan.FromMinutes(1));
        }

        return output;
    }

    public async Task<List<Ticket>> GetUserCreatedTicketsAsync(User user)
    {
        var output = _cache.Get<List<Ticket>>(CacheName + user.Id + "Created");
        if (output is null)
        {
            var results = await _ticketCollection.FindAsync(t => t.CreatedBy.Id == user.Id);
            output = results.ToList();

            _cache.Set(CacheName + user.Id + "Created", output, TimeSpan.FromMinutes(1));
        }

        return output;
    }

    public async Task<List<Ticket>> GetUserAssignedTicketsAsync(User user)
    {
        var output = _cache.Get<List<Ticket>>(CacheName + user.Id + "Assigned");
        if (output is null)
        {
            var results = await _ticketCollection.FindAsync(t => t.AssignedUser.Id == user.Id);
            output = results.ToList();

            _cache.Set(CacheName + user.Id + "Assigned", output, TimeSpan.FromMinutes(1));
        }

        return output;
    }

    public async Task<Ticket> GetTicketAsync(string ticketId)
    {
        IAsyncCursor<Ticket> results = await _ticketCollection.FindAsync(t => t.TicketId == ticketId);
        return results.FirstOrDefault();
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        await _ticketCollection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        _cache.Remove(CacheName);
    }

    public async Task SetStatusAsync(Ticket ticket, Status status)
    {
        ticket.Status = status;
        await _ticketCollection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        _cache.Remove(CacheName);
    }

    public async Task CreateTicketAsync(Ticket ticket)
    {
        await CreateMultipleTicketsAsync(new[] { ticket });
    }

    public async Task CreateMultipleTicketsAsync(IEnumerable<Ticket> tickets)
    {
        var client = _db.Client;

        using var session = await client.StartSessionAsync();

        session.StartTransaction();

        try
        {
            var db = client.GetDatabase(_db.DbName);
            var ticketsInTransaction = db.GetCollection<Ticket>(_db.TicketCollectionName);
            await ticketsInTransaction.InsertManyAsync(session, tickets);

            await session.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }

    public async Task CloseTicketsAsync(int numberOfDays)
    {
        var tickets = await _ticketCollection.FindAsync(t => t.Status.Name == "Completed" && t.LastUpdatedDate == DateTime.Now.AddDays(-numberOfDays));
        var ticketList = tickets.ToList();

        foreach (var ticket in ticketList)
        {
            ticket.Status = new Status { Name = "Closed" }; //TODO set updated status to closed from database
            await _ticketCollection.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }

        _cache.Remove(CacheName);
    }

    public async Task<int> GetOpenTicketCountAsync()
    {
        var tickets = await _ticketCollection.FindAsync(t => t.Status.Name == "New" || t.Status.Name == "Active");
        return tickets.ToList().Count;
    }

    public async Task<int> GetClosedTicketCountAsync()
    {
        var tickets = await _ticketCollection.FindAsync(t => t.Status.Name == "Closed" || t.Status.Name == "Completed");
        return tickets.ToList().Count;
    }
}
