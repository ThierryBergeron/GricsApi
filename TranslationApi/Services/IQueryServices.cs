using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.UserRequest;

namespace SimpleApi.Services
{
    public interface IQueryServices
    {
        Task<IEnumerable<Student>> QueryStudentAsync(SimpleQuery query);
        Task<IEnumerable<Teacher>> QueryTeacherAsync(SimpleQuery query);
        Task<IEnumerable<School>> QuerySchoolAsync(SimpleQuery query);
        Task<IEnumerable<ReportCard>> QueryReportCardAsync(SimpleQuery query);
    }
}
