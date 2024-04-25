using MediatR;

namespace GigiTrucks.Services.Discounts.Application.Queries.GetUserDiscounts;

public record GetUserDiscountsQuery(Guid UserId) 
    : IRequest;