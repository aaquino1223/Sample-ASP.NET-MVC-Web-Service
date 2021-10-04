using CarWebService.Dtos;
using CarWebService.Managers;
using CarWebService.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarWebService.Controllers
{
    [Authorize]
    public class CarController : ApiController
    {
        private CarManager _CarManager = new CarManager(new CarDbContext());

        [HttpGet]
        [Route("api/Car/{yearMade}")]
        public IHttpActionResult GetCarsByYearMade(string yearMade)
        {
            List<CarDto> cars = _CarManager.GetCarsByYearMade(yearMade);

            return cars != null ? Ok(cars) : (IHttpActionResult)InternalServerError();
        }

        [HttpPost]
        [Route("api/Car")]
        public async Task<IHttpActionResult> PostCar([FromBody] CarDto car)
        {
            var created = await _CarManager.TryCreateCar(car);

            return created ? Ok() : (IHttpActionResult)InternalServerError();
        }

        [HttpPatch]
        [Route("api/Car/{id}")]
        public async Task<IHttpActionResult> Edit(string id, [FromBody] JsonPatchDocument<CarDto> patch)
        {
            var carDto = new CarDto();

            patch.ApplyTo(carDto);
            var created = await _CarManager.TryUpdateCar(id, carDto);

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