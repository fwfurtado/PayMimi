using System;
using System.Threading;
using AutoBogus;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using PayMimi.Domain.Entities;
using PayMimi.Exceptions;

namespace PayMimi.Test.Domain.Entities;

public class TransactionTest
{
    private readonly Faker _faker = new(); 
    
    [Test]
    public void TransactionMustNotHaveNegativeAmount()
    {
        var customer = AutoFaker.Generate<Customer>();
        var negativeAmount = _faker.Random.Decimal(max: 0M);
        
        var action = () => Transaction.CreateIncoming(customer, negativeAmount);

        action.Should().Throw<NegativeAmountException>();
    }
    
    [Test]
    public void TransactionMustHaveCustomer()
    {
        var amount = _faker.Random.Decimal(min: 1M);
        
        var action = () => Transaction.CreateIncoming(null!, amount);

        action.Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void TransactionDateMustBeBothUtcAndInThePast()
    {
        var customer = AutoFaker.Generate<Customer>();
        var amount = _faker.Random.Decimal(min: 1M);
        
        var transaction = Transaction.CreateIncoming(customer, amount);

        transaction.Date.Kind.Should().Be(DateTimeKind.Utc);
        Thread.Sleep(10);
        transaction.Date.Should().BeBefore(DateTime.UtcNow);
    }
}