﻿using GigiTrucks.Services.Carts.Application.DTOs;

namespace GigiTrucks.Services.Carts.Api.Requests;

public record CreateCartRequest(IList<CartItemDto> Items);