using GigiTrucks.Services.Newsletter.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Queries.GetNewsletter;

public class GetNewsletterHandler : IRequestHandler<GetNewsletter, NewsletterDto>
{
    public Task<NewsletterDto> Handle(GetNewsletter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}