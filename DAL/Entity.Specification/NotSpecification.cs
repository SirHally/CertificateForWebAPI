using System;
using System.Linq.Expressions;
using DAL.Specifications.Expressions;

namespace DAL.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {

        public NotSpecification(Specification<T> specification):
            base(((Expression<Func<T, bool>>)specification).Not())
        {
        }
    }
}
