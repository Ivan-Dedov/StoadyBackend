using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;

namespace Stoady.DataAccess.Repositories
{
    public interface IDatabaseRepository
    {
        /// <summary>
        /// Получает все объекты типа <typeparamref name="T"/> из базы данных.
        /// </summary>
        /// <typeparam name="T">Тип обектов, которые необходимо получить.</typeparam>
        /// <returns>IQueryable, содержащую объекты заданного типа.</returns>
        IQueryable<T> Query<T>() where T : class, IDao;

        /// <summary>
        /// Получает все объекты типа <typeparamref name="T"/> из базы данных, удовлетворяющие заданному условию.
        /// </summary>
        /// <param name="selector">Условие, по которому необходимо профильтровать объекты.</param>
        /// <typeparam name="T">Тип обектов, которые необходимо получить.</typeparam>
        /// <returns>IQueryable, содержащую объекты заданного типа.</returns>
        IQueryable<T> Query<T>(Expression<Func<T, bool>> selector) where T : class, IDao;

        /// <summary>
        /// Добавляет объект типа <typeparamref name="T"/> в базу данных.
        /// </summary>
        /// <param name="item">Объект, который необходимо добавить.</param>
        /// <typeparam name="T">Тип объекта, который необходимо добавить.</typeparam>
        /// <returns>ID объекта, добавленного в базу данных.</returns>
        Task<long> Add<T>(T item) where T : class, IDao;

        /// <summary>
        /// Добавляет множество объектов типа <typeparamref name="T"/> в базу данных.
        /// </summary>
        /// <param name="items">Объекты, который необходимо добавить.</param>
        /// <typeparam name="T">Тип объектов, которые необходимо добавить.</typeparam>
        Task AddRange<T>(IEnumerable<T> items) where T : class, IDao;

        /// <summary>
        /// Удаляет объект из базы данных по ID.
        /// </summary>
        /// <param name="id">ID объекта, который нужно удалить.</param>
        /// <typeparam name="T">Тип удаляемого объекта.</typeparam>
        void Remove<T>(long id) where T : class, IDao;

        /// <summary>
        /// Удаляет объект из базы данных.
        /// </summary>
        /// <param name="item">Объект, который нужно удалить.</param>
        /// <typeparam name="T">Тип удаляемого объекта.</typeparam>
        void Remove<T>(T item) where T : class, IDao;

        /// <summary>
        /// Удаляет объекты из базы данных.
        /// </summary>
        /// <param name="items">Объекты, который нужно удалить.</param>
        /// <typeparam name="T">Тип удаляемых объектов.</typeparam>
        void RemoveRange<T>(IEnumerable<T> items) where T : class, IDao;

        /// <summary>
        /// Добавляет объект в ChangeTracker.
        /// </summary>
        /// <param name="item">Объект, обновления которого нужно отслеживать.</param>
        /// <typeparam name="T">Тип объекта.</typeparam>
        void Update<T>(T item) where T : class, IDao;

        /// <summary>
        /// Добавляет объекты в ChangeTracker.
        /// </summary>
        /// <param name="items">Объекты, обновления которых нужно отслеживать.</param>
        /// <typeparam name="T">Тип объектов.</typeparam>
        void UpdateRange<T>(IEnumerable<T> items) where T : class, IDao;

        /// <summary>
        /// Сохраняет внесенные изменения.
        /// </summary>
        /// <returns>Количество измененных записей.</returns>
        Task<int> SaveChangesAsync();
    }
}
