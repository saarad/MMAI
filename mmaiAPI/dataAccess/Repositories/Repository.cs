using dataAccess.Interfaces;


namespace dataAccess
{
    /// <summary>
    /// todo: class can be removed or used as a base class for generic CRUD operations (create, read, update and delete) 
    /// </summary>
    public class Repository : IRepository
    {
        protected readonly IDbConnectionFactory _dbFactory;

        public Repository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

    }
}