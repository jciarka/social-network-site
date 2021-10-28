using BD2.API.Database.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Controllers
{
    [Route("[controller]")]
    public class ImagesController : ExtendedControllerBase
    {
        public IImagesRepository _repo;
        public ImagesController(IImagesRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Image(Guid id)
        {
            var image = await _repo.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return File(image.Binary, image.MimeType);
        }

    }
}
