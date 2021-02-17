using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace Lisans.DataAccess.Repository
{
    /// <summary>
    /// Models katmanımızda bulunan her T tipi için aşağıda tanımladığımız fonksiyonları gerçekleştirebilecek generic bir repository tanımlıyoruz.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Tüm veriyi getir.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Veriyi Where metodu ile getir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// İstenilen veriyi single olarak getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verilen entityi ekle.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Verilen entity'i günceller.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Verilen entity'i siler.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// predicate göre veriler silinir.
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false);

        /// <summary>
        /// Aynı kayıt eklememek için objeyi kontrol ederek true veya false dönderir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Tablodaki sayıyı gönderir.
        /// </summary>
        int Count();

        /// <summary>
        /// Verilen sorguya göre tablodaki sayıyı gönderir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
    }
}
