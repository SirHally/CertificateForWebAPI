namespace DAL.Specifications.POCO.User
{
    public class ByThumbprint : Specification<DAL.POCO.UserCertificate>
    {
        public ByThumbprint(string value)
            : base(element => element.Thumbprint == value)
        {
        }

    }
}
