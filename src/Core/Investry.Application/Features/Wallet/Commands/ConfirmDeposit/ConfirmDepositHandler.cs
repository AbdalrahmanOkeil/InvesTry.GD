using Investry.Application.Common;
using Investry.Application.Contracts.Persistence;
using Investry.Domain.Enums;
using MediatR;

namespace Investry.Application.Features.Wallet.Commands.ConfirmDeposit
{
    public class ConfirmDepositHandler : IRequestHandler<ConfirmDepositCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmDepositHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(ConfirmDepositCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _unitOfWork.WalletRepository.GetByUserIdAsync(request.UserId);
            if (wallet is null)
                return Result<bool>.Failure(new List<Error> { new Error("Wallet.NotFound", "Wallet not found for the user.", ErrorType.NotFound) });

            var transaction = await _unitOfWork.WalletRepository.GetTransactionBySessionIdAsync(request.SessionId);
            if (transaction is null)
                return Result<bool>.Failure(new List<Error> { new Error("Transaction.NotFound", "No transaction found for the provided session ID.", ErrorType.NotFound) });

            if (transaction.Status == TransactionStatus.Completed)
                return Result<bool>.Failure(new List<Error> { new Error("Transaction.AlreadyCompleted", "This transaction has already been completed.", ErrorType.Conflict) });

            wallet.Deposit(request.Amount);
            transaction.Status = TransactionStatus.Completed;

            await _unitOfWork.SaveAsync();

            return Result<bool>.Success(true);
        }
    }
}
