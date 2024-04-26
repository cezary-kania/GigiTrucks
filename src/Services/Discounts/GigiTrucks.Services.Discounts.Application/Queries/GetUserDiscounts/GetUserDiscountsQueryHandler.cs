using MediatR;

namespace GigiTrucks.Services.Discounts.Application.Queries.GetUserDiscounts;

internal sealed class GetUserDiscountsQueryHandler
    : IRequestHandler<GetUserDiscountsQuery>
{
    public Task Handle(GetUserDiscountsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
