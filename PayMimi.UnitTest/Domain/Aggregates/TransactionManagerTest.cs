using System.Threading.Tasks;
using AutoBogus;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using PayMimi.Domain.Aggregates;
using PayMimi.Domain.Entities;
using PayMimi.Test.Fixtures;

namespace PayMimi.Test.Domain.Aggregates;

public class TransactionManagerTest
{
    [Test]
    public async Task PendingTransactionCanBePrecessingAndAfterCompleted()
    {
        var tx = TransactionFixtures.Pending.Any;

        var manager = new TransactionManager(tx);
        
        await manager.Process();

        tx.Status.Should().Be(TransactionStatus.Processing);
        
        await manager.Complete();
        
        tx.Status.Should().Be(TransactionStatus.Completed);
    }
    
    [Test]
    public async Task PendingTransactionCanBePrecessingAndAfterFailed()
    {
        var tx = TransactionFixtures.Pending.Any;

        var manager = new TransactionManager(tx);
        
        await manager.Process();

        tx.Status.Should().Be(TransactionStatus.Processing);
        
        await manager.Fail();
        
        tx.Status.Should().Be(TransactionStatus.Failed);
    }
}