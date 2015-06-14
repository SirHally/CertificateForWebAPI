﻿using System;
using System.Linq;
using System.Linq.Expressions;
using DAL.Specifications.Expressions;

namespace DAL.Specifications
{
    public class OrSpecification<T> : Specification<T>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public OrSpecification(params Specification<T>[] spec)
            : base(spec.Skip(1).Aggregate((Expression<Func<T, bool>>)spec.First(), (current, specification) => current.Or(specification)))
        {
        }
    }
}
