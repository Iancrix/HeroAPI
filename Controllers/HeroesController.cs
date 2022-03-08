using AutoMapper;
using HeroAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.Controllers
{
    [ApiController]
    [Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        private readonly HeroAcademiaContext context;
        private readonly IMapper mapper;

        public HeroesController(HeroAcademiaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<HeroDTO>>> GetHero()
        {
            return await context.Heroes.Select(hero => mapper.Map<HeroDTO>(hero)).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> PostHero([FromBody] Hero hero)
        {
            context.Heroes.Add(hero);
            await context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHeroById),
                new { id = hero.Id },
                mapper.Map<HeroDTO>(hero)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeroDTO>> GetHeroById([FromRoute] long id)
        {
            var hero = await context.Heroes.Include(x => x.Sidekicks).FirstOrDefaultAsync(x => x.Id == id);

            if (hero == null)
            {
                return NotFound();
            }

            return mapper.Map<HeroDTO>(hero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HeroDTO>> UpdateHero([FromBody] Hero hero, [FromRoute] long id)
        {
            if (hero.Id != id)
            {
                return BadRequest(new
                {
                    message = "The hero id don't match."
                });
            }

            var heroExists = await context.Heroes.AnyAsync(x => x.Id == id);

            if (!heroExists)
            {
                return NotFound();
            }

            context.Heroes.Update(hero);
            await context.SaveChangesAsync();

            return mapper.Map<HeroDTO>(hero);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero([FromRoute] long id)
        {
            var hero = await context.Heroes.Include(x => x.Sidekicks).FirstOrDefaultAsync(x => x.Id == id);

            if (hero == null)
            {
                return NotFound();
            }

            context.Heroes.Remove(hero);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = $"The hero with id {id} was deleted successfully!"
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchHero([FromRoute] long id, [FromBody] JsonPatchDocument<HeroDTOPatch> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var hero = await context.Heroes.Include(x => x.Sidekicks).FirstOrDefaultAsync(x => x.Id == id);

            if (hero == null)
            {
                return NotFound();
            }

            var heroDTO = mapper.Map<HeroDTOPatch>(hero);
            patchDoc.ApplyTo(heroDTO, ModelState);

            var isValid = TryValidateModel(heroDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(heroDTO, hero);
            await context.SaveChangesAsync();

            return Ok(heroDTO);
        }
    }
}
