using Investry.Application.Common;
using Investry.Application.Contracts.Infrastructure;
using Investry.Application.Features.Wallet.Commands.ConfirmDeposit;
using MediatR;

namespace Investry.Application.Features.Wallet.Commands.ProcessWebhook
{
    public class ProcessWebhookHandler : IRequestHandler<ProcessWebhookCommand, Result<bool>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;

        public ProcessWebhookHandler(IPaymentService paymentService, IMediator mediator)
        {
            _paymentService = paymentService;
            _mediator = mediator;
        }
        async Task<Result<bool>> IRequestHandler<ProcessWebhookCommand, Result<bool>>.Handle(ProcessWebhookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var webhookResult = _paymentService.ParseWebhookEvent(
                    request.Payload, request.Signature);

                if (webhookResult.EventType == "checkout.session.completed")
                {
                    await _mediator.Send(new ConfirmDepositCommand(
                        SessionId: webhookResult.SessionId,
                        Amount: webhookResult.Amount,
                        UserId: webhookResult.Metadata["userId"]
                    ));
                }

                return Result<bool>.Success(true);
            }
            catch
            {
                return Result<bool>.Success(false);
            }
        }
    }
}
