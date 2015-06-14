namespace DAL.Specifications.POCO.User
{
    public class ByIsInstalled : Specification<DAL.POCO.UserCertificate>
    {
        public ByIsInstalled(bool value)
            : base(element => element.IsInstalled == value)
        {
        }

    }
}
