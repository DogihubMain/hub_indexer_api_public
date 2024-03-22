
using StackExchange.Redis;

namespace DogiHubIndexerApi.Application.Clients
{
    public interface IRedisClient
    {
        Task<string?> StringGetAsync(string key);
        Task<RedisValue[]> SortedSetRangeByRankAsync(string key, long start = 0, long stop = -1, Order order = Order.Ascending);
        Task<RedisValue[]> SetMembersAsync(string key);
    }
}
