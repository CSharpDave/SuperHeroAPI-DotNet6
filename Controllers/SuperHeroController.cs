using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _dbContext;
        public SuperHeroController(DataContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero vanished by Thanos");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dbContext.SuperHeroes.Add(hero);
            await _dbContext.SaveChangesAsync();

            return Ok(_dbContext.SuperHeroes.ToListAsync() );
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero vanished by Thanos");
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero vanished by Thanos");

            _dbContext.SuperHeroes.Remove(hero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

    }
}
