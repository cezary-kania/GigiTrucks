﻿using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.SubmitCart;

public record SubmitCart(Guid CustomerId) : IRequest;