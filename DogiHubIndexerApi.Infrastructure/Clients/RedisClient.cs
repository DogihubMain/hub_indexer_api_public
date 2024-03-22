using DogiHubIndexerApi.Application.Clients;
using StackExchange.Redis;

namespace DogiHubIndexerApi.Infrastructure.Clients
{
    public class RedisClient : IRedisClient
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;

        public RedisClient(string connectionString)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _database = _connectionMultiplexer.GetDatabase();
        }

        private void EnsureDatabaseConnection()
        {
            if (_database is null)
                throw new InvalidOperationException("RedisClient is not configured with a connection string.");
        }

        public async Task<string?> StringGetAsync(string key)
        {
            EnsureDatabaseConnection();
            RedisValue value = await (_database.StringGetAsync(key) ?? Task.FromResult(RedisValue.Null));

            if (value.IsNull)
            {
                return null;
            }

            return value.ToString();
        }

        public async Task<RedisValue[]> SortedSetRangeByRankAsync(string key, long start = 0, long stop = -1, Order order = Order.Ascending)
        {
            EnsureDatabaseConnection();
            return await _database.SortedSetRangeByRankAsync(key, start, stop, order);
        }

        public Task<RedisValue[]> SetMembersAsync(string key)
        {
            EnsureDatabaseConnection();
            return _database.SetMembersAsync(key);
        }
    }
}
