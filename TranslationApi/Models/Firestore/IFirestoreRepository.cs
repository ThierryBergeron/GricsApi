using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Models.Firestore
{
    public interface IFirestoreRepository<T>
    {
        T Get(T Record);
        List<T> GetAll();
        T Add(T Record);
        bool Update(T Record);
        bool Delete(T Record);
        Task<T> GetAsync(T Record);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T Record);
        Task<bool> UpdateAsync(T Record);
        Task<bool> DeleteAsync(T Record);
        Task<T> GetByIdAsync(string id);
        Query GetQuery();
    }
}
