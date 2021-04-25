using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Services
{
    public interface IMockDataGenerator
    {
        Task<bool> GenerateSchoolsAsync();
        Task<bool> GenerateTeachersAsync();
        Task<bool> GenerateStudentsAsync();
    }
}
