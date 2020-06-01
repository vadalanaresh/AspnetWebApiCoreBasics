using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CouresesLibrary.Api.Helpers;
using CouresesLibrary.Api.Models;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CouresesLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/authorCollections")]
    public class AuthorsCollectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        public AuthorsCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }



        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthorCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authors = _courseLibraryRepository.GetAuthors(ids);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorsCollection(IEnumerable<AuthorForCreationDto> authorsCollection)
        {
            var authorEntityCollection = _mapper.Map<IEnumerable<Author>>(authorsCollection);
            foreach (var author in authorEntityCollection)
            {
                _courseLibraryRepository.AddAuthor(author);
            }

            _courseLibraryRepository.Save();
            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorEntityCollection);
            var ids = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetAuthorCollection", new { ids }, authorCollectionToReturn);
        }
    }
}
