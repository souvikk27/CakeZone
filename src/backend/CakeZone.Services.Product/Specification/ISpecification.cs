﻿using System.Linq.Expressions;

namespace CakeZone.Services.Product.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        Expression<Func<T, object>>? GroupBy { get; }
        Expression<Func<T, object>>? ThenBy { get; }

        int? Take { get; }
        int? Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
