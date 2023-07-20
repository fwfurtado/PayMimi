using PayMimi.Domain.Entities;
using Stateless;
using AsyncCallback = System.Func<PayMimi.Domain.Entities.Transaction, System.Threading.Tasks.Task>;

namespace PayMimi.Domain.Aggregates;

public class TransactionManager
{
    private readonly Transaction _tx;
    private readonly StateMachine<TransactionStatus, TransactionTrigger> _stateMachine;
    private readonly AsyncCallback? _onStartProcessing;
    private readonly AsyncCallback? _onComplete;
    private readonly AsyncCallback? _onFail;

    public TransactionStatus Status => _tx.Status;

    public TransactionManager(Transaction tx, AsyncCallback? onStartProcessing = null, AsyncCallback? onComplete = null,
        AsyncCallback? onFail = null)
    {
        _tx = tx;
        _onStartProcessing = onStartProcessing;
        _onComplete = onComplete;
        _onFail = onFail;
        _stateMachine = new StateMachine<TransactionStatus, TransactionTrigger>(() => _tx.Status, s => _tx.Status = s);
        SetupStateMachine();
    }

    private void SetupStateMachine()
    {
        _stateMachine.OnUnhandledTriggerAsync((status, trigger)
            => throw new InvalidTransactionStatusTransitionException(
                $"Invalid transaction status transition. Cannot transition perform a '{trigger}' action on a {status} transaction."));

        _stateMachine.Configure(TransactionStatus.Pending)
            .Permit(TransactionTrigger.Process, TransactionStatus.Processing);

        _stateMachine.Configure(TransactionStatus.Processing)
            .OnEntryFromAsync(TransactionTrigger.Process, () => _onStartProcessing?.Invoke(_tx) ?? Task.CompletedTask)
            .Permit(TransactionTrigger.Complete, TransactionStatus.Completed)
            .Permit(TransactionTrigger.Fail, TransactionStatus.Failed);

        _stateMachine.Configure(TransactionStatus.Completed)
            .OnExitAsync(() => _onComplete?.Invoke(_tx) ?? Task.CompletedTask);

        _stateMachine.Configure(TransactionStatus.Failed)
            .OnEntryAsync(() => _onFail?.Invoke(_tx) ?? Task.CompletedTask);
    }


    public Task Process()
    {
        return _stateMachine.FireAsync(TransactionTrigger.Process);
    }

    public Task Complete()
    {
        return _stateMachine.FireAsync(TransactionTrigger.Complete);
    }

    public Task Fail()
    {
        return _stateMachine.FireAsync(TransactionTrigger.Fail);
    }
}

public enum TransactionTrigger
{
    Process,
    Complete,
    Fail,
}

public class InvalidTransactionStatusTransitionException : InvalidOperationException
{
    public InvalidTransactionStatusTransitionException(string message) : base(message)
    {
    }
}