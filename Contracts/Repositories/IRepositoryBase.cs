using System.Linq.Expressions;

namespace Domain.Contracts;
public interface IRepositoryBase<T>
{
    IEnumerable<T> FindAll();
    IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}

