using DotNetCapTest.Web.Entities;
using DotNetCore.CAP;

namespace DotNetCapTest.Web.Services
{
    public class TransactionConsumer : ICapSubscribe
    {
        private readonly AppDbContext _db;

        public TransactionConsumer(AppDbContext db)
        {
            _db = db;
        }

        [CapSubscribe("transaction")]
        public async Task ConsumeAsync(Transaction transaction, CancellationToken ct)
        {
            _db.Transactions.Add(new Transaction
            {
                Id = transaction.Id,
                CreatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync(ct);

            Console.WriteLine($"Transaction was saved: {transaction}");
        }
    }
}
