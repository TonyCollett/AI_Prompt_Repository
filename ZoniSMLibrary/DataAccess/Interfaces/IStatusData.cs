
namespace ZoniSMLibrary.DataAccess.Interfaces;

public interface IStatusData
{
    Task CreateStatusAsync(Status status);
    Task CreateMultipleStatusesAsync(IEnumerable<Status> statuses);
    Task RemoveStatusAsync(Status status);
    Task<List<Status>> GetAllStatusesAsync(bool ignoreCache = false);
    Task<Status> GetStatusByNameAsync(string statusName);
    Task UpdateStatusesAsync(IEnumerable<Status> statuses);
}