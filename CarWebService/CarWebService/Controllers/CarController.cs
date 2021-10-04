using CarWebService.Dtos;
using CarWebService.Managers;
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
        private CarManager _CarManager = new CarManager();
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
            List<CarDto> cars = _CarManager.GetCarsByYearMade(yearMade);

            return cars != null ? Ok(cars) : (IHttpActionResult)InternalServerError();
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/Car")]
        public async Task<IHttpActionResult> Post([FromBody] CarDto car)
        {
            var created = await _CarManager.TryCreateCar(car);

            return created ? Ok() : (IHttpActionResult)InternalServerError();
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
}