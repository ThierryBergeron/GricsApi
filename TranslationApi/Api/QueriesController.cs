using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models;
using SimpleApi.Models.UserRequest;
using SimpleApi.Services;

namespace SimpleApi.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryServices _queryServices;
        public QueriesController(IQueryServices queryServices)
        {
            _queryServices = queryServices;
        }
        [HttpGet]
        public IActionResult DefaultGet()
        {
            return NotFound();
        }

        /// <summary>
        /// Complex query including
        /// 1- Field (name)
        /// 2- Filter (constraint=value);
        /// 3- GroupBy (name)
        /// 
        /// 
        /// Example 
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        [HttpPost()]
        async public Task<IActionResult> Query(SimpleQuery simpleQuery)
        {
            try
            {
                var target = simpleQuery.Target.ToLower();

                // dirty solution
                if (target == "student")
                {
                    var students = await _queryServices.QueryStudentAsync(simpleQuery);
                    return Ok(students);
                }
                if (target == "teacher")
                {
                    var teachers = await _queryServices.QueryTeacherAsync(simpleQuery);
                    return Ok(teachers);
                }
                if (target == "school")
                {
                    var schools = await _queryServices.QuerySchoolAsync(simpleQuery);
                    return Ok(schools);
                }
                if (target == "reportcard")
                {
                    var reportCard = await _queryServices.QueryReportCardAsync(simpleQuery);
                    return Ok(reportCard);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(500, "Internal servor error");
            }
        }


    }
}
