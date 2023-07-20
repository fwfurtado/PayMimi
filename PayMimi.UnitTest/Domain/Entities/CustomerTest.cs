using AutoBogus;
using FluentAssertions;
using NUnit.Framework;
using PayMimi.Domain.Entities;
using PayMimi.Domain.ValueObjects;

namespace PayMimi.Test.Domain.Entities;

public class CustomerTest
{
    [Test]
    public void CustomerMustHaveAName()
    {
        var name = AutoFaker.Generate<Name>();
        var customer = new Customer(name);


        name.FullName.Should().Be(customer.Name);
    }
}