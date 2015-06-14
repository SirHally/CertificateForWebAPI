using System.Collections.Generic;
using System.Linq;

namespace DAL.Specifications.POCO.User
{
    /// <summary>
    /// Спецификация по имени пользователя
    /// </summary>
    public class ById : Specification<DAL.POCO.UserCertificate>
    {
        public ById(int value)
            : base(element => element.Id == value)
        {
        }

        public ById(IEnumerable<int> value)
            : base(element => value.Contains(element.Id))
        {
        }
    }
}
