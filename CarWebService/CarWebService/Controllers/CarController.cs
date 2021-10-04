using CarWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarWebService.Controllers
{
    public class CarController : ApiController
    {
        private CarDbContext _CarDbContext = new CarDbContext();

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/
        [HttpGet]
        [Route("api/Car/{yearMade}")]
        public IHttpActionResult GetCarsByYearMade(string yearMade)
        {
            List<Car> cars = new List<Car>();
            try
            {
                cars = _CarDbContext.Cars.Where(x => x.YearMade == yearMade).ToList();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok(cars);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Car")]
        public async Task<IHttpActionResult> Post([FromBody] CarDto car)
        {
            try
            {
                _CarDbContext.Cars.Add(new Car
                {
                    Id = Guid.NewGuid(),
                    Name = car.Name,
                    Color = car.Color,
                    YearMade = car.YearMade
                });

                await _CarDbContext.SaveChangesAsync();
            }
            catch
            {
                return InternalServerError();
            }

            return Ok();
        }

        // PUT api/<controller>/5
        //public void Put(string id, [FromBody] CarDto car)
        //{

        //}

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }

    public class CarDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string YearMade { get; set; }
    }
}