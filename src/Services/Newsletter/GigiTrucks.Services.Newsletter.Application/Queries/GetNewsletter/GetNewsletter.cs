using GigiTrucks.Services.Newsletter.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Queries.GetNewsletter;

public record GetNewsletter(Guid NewsletterId) : IRequest<NewsletterDto>;