namespace GigiTrucks.Services.Newsletter.Application.DTOs;

public record SubscriptionStatusDto(
    Guid SubscriberId, 
    bool IsActive);