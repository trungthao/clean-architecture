using System.Linq.Expressions;

namespace Domain.Contracts;
public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChange = false);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
}

