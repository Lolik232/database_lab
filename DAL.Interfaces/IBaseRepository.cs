namespace DAL.Interfaces
{
    public interface IBaseGetRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);

        public interface WithGetAll : IBaseGetRepository<T>
        {
            IEnumerable<T> GetAll();
            Task<IEnumerable<T>> GetAllAsync();
        }
    }

    public interface IBaseRemoveRepository<T> where T : class
    {
        bool Remove(int id);
        public interface WithRemoveAll : IBaseRemoveRepository<T>
        {

            bool RemoveAll();
        }
    }
}
