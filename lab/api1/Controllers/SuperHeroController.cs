using api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api1.Data;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase

    {
        private superHerosDb _db;

        public SuperHeroController(superHerosDb db)
        {
            _db = db;
        }
        private static List<SuperHero> superHeroes = new List<SuperHero>
           {
               new SuperHero{Id=1, Name="spiderman",SuperPower="spider web"},
               new SuperHero{Id=2, Name="ironMan",SuperPower="the Suit"},
               new SuperHero{Id=3, Name="Hulk",SuperPower="the green thing"}

           };
        

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
           
            return Ok(await _db.superHeroes.ToListAsync());
        }


        [HttpGet("{_id}")]
        public async Task<ActionResult<SuperHero>> Get(int _id)
        {
            SuperHero? myHero = await _db.superHeroes.FindAsync(_id);
        
            return myHero == null? BadRequest("hero not found"):Ok(myHero);
        }


        [HttpPut]
        public async Task<ActionResult<SuperHero>> update(SuperHero myHero)
        {
            SuperHero? superHero = await _db.superHeroes.FindAsync(myHero.Id);
            if (superHero == null)
                return BadRequest("hero not found");
            superHero.Name=myHero.Name;
            superHero.SuperPower = myHero.SuperPower;
            await _db.SaveChangesAsync();
            return Ok(await _db.superHeroes.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> addHero(SuperHero myHero)
        {

            await _db.superHeroes.AddAsync(myHero);
            await _db.SaveChangesAsync();
            return Ok(await _db.superHeroes.ToListAsync());
        }


        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> deleteHero(int id)
        {
            SuperHero? myHero = await _db.superHeroes.FindAsync(id);
            if (myHero == null)
                return NotFound("Hero is not found");
            _db.superHeroes.Remove(myHero);
            await _db.SaveChangesAsync();
            return Ok(await _db.superHeroes.ToListAsync());
        }
    }
}
