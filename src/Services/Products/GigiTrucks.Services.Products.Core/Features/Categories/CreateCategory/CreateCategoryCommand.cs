﻿using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Categories.CreateCategory;

public record CreateCategoryCommand(
    Guid CategoryId,
    string Name, 
    Guid? ParentCategoryId) : IRequest<OneOf<Success,Error<string>>>;