using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CouresesLibrary.Api.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CouresesLibrary.Api.Controllers
{
    [Route("api/authors/{authorId}/Courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CourseDto>> GetCourses(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();

            var courses = _courseLibraryRepository.GetCourses(authorId);
            if (courses == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) return NotFound();
            var course = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (course == null) return NotFound();
            return Ok(_mapper.Map<CourseDto>(course));
        }
    }
}
