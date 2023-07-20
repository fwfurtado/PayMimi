using System.Collections.Generic;
using AutoBogus;
using Bogus;
using PayMimi.Domain.Entities;

namespace PayMimi.Test.Fixtures;

public static class TransactionFixtures
{
    private static readonly AutoFaker<Transaction> Generator;


    static TransactionFixtures()
    {
        Generator = new AutoFaker<Transaction>();

        Generator
            .RuleFor(x => x.Amount, f => f.Random.Decimal(min: 1M, max: 100000M))
            .RuleFor(x => x.Date, f => f.Date.Past())
            .RuleSet("pending", rules => rules
                .RuleFor(tx => tx.Status, TransactionStatus.Pending)
            )
            .RuleSet("processing", rules => rules
                .RuleFor(tx => tx.Status, TransactionStatus.Processing)
            )
            .RuleSet("completed", rules => rules
                .RuleFor(tx => tx.Status, TransactionStatus.Completed)
            )
            .RuleSet("failed", rules => rules
                .RuleFor(tx => tx.Status, TransactionStatus.Failed)
            )
            .RuleSet("income", rules => rules
                .RuleFor(tx => tx.Type, TransactionType.Incoming)
            )
            .RuleSet("outgoing", rules => rules
                .RuleFor(tx => tx.Type, TransactionType.Incoming)
            );
    }

    public static class Pending
    {
        public static Transaction Income => Generator.Generate("pending,income");
        public static Transaction OutGoing => Generator.Generate("pending,income");
        public static Transaction Any => Generator.Generate("pending");

        public static IList<Transaction> Incomes(int count) => Generator.Generate(count, "pending,income");
        public static IList<Transaction> Outgoings(int count) => Generator.Generate(count, "pending,outgoing");
        public static IList<Transaction> Many(int count) => Generator.Generate(count, "pending");
    }

    public static class Processing
    {
        public static Transaction Income => Generator.Generate("processing,income");
        public static Transaction OutGoing => Generator.Generate("processing,income");
        public static Transaction Any => Generator.Generate("processing");

        public static IList<Transaction> Incomes(int count) => Generator.Generate(count, "processing,income");
        public static IList<Transaction> Outgoings(int count) => Generator.Generate(count, "processing,outgoing");
        public static IList<Transaction> Many(int count) => Generator.Generate(count, "processing");
    }

    public static class Completed
    {
        public static Transaction Income => Generator.Generate("completed,income");
        public static Transaction OutGoing => Generator.Generate("completed,income");
        public static Transaction Any => Generator.Generate("completed");

        public static IList<Transaction> Incomes(int count) => Generator.Generate(count, "completed,income");
        public static IList<Transaction> Outgoings(int count) => Generator.Generate(count, "completed,outgoing");
        public static IList<Transaction> Many(int count) => Generator.Generate(count, "completed");
    }

    public static class Failed
    {
        public static Transaction Income => Generator.Generate("failed,income");
        public static Transaction OutGoing => Generator.Generate("failed,income");
        public static Transaction Any => Generator.Generate("failed");

        public static IList<Transaction> Incomes(int count) => Generator.Generate(count, "failed,income");
        public static IList<Transaction> Outgoings(int count) => Generator.Generate(count, "failed,outgoing");
        public static IList<Transaction> Many(int count) => Generator.Generate(count, "failed");
    }
}