using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleApi.Models.Repositories;
using SimpleApi.Services;

namespace SimpleApi.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MockGeneratorController : ControllerBase
    {
        private readonly IMockDataGenerator _mockData;
        private readonly ISchoolRepository _schoolRepository;

        public MockGeneratorController(IMockDataGenerator mockGenerator, ISchoolRepository schoolRepository)
        {
            _mockData = mockGenerator;
            _schoolRepository = schoolRepository;
        }

        [HttpPost("generate_schools")]
        async public Task<IActionResult> GenerateSchools()
        {
            //await _mockData.GenerateSchoolsAsync();
            return Ok();
        }

        [HttpPost("generate_teachers")]
        async public Task<IActionResult> GenerateTeachers()
        {
            //await _mockData.GenerateTeachersAsync();
            return Ok();
        }

        [HttpPost("generate_students")]
        async public Task<IActionResult> GenerateStudents()
        {
            //await _mockData.GenerateStudentsAsync();
            return Ok();
        }
    }
}
