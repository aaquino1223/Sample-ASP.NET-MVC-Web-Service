using CarWebService.Dtos;
using CarWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CarWebService.Managers
{
    //TODO: Add more meaningful results for each call
    public class CarManager
    {
        private CarDbContext _Context = new CarDbContext();

        public CarManager(CarDbContext context)
        {
            _Context = context;
        }

        public async Task<bool> TryCreateCar(CarDto car)
        {
            try
            {
                _Context.Cars.Add(new Car
                {
                    Id = Guid.NewGuid(),
                    Name = car.Name,
                    Color = car.Color,
                    YearMade = car.YearMade
                });

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> TryUpdateCar(string id, CarDto car)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid carGuid))
                    return false;

                var dbCar = await _Context.Cars.FindAsync(carGuid);

                if (dbCar == null)
                    return false;

                if (car.Name != null)
                {
                    dbCar.Name = car.Name;
                }

                if (car.Color != null)
                {
                    dbCar.Color = car.Color;
                }

                if (car.YearMade != null)
                {
                    dbCar.YearMade = car.YearMade;
                }

                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<CarDto> GetCarsByYearMade(string yearMade)
        {
            List<CarDto> cars = null;
            try
            {
                cars = _Context.Cars.Where(x => x.YearMade == yearMade).Select(y => new CarDto() { Name = y.Name, Color = y.Color, YearMade = y.YearMade } ).ToList();
            }
            catch (Exception ex)
            {
                //TODO: Log it somewhere
            }

            return cars;
        }
    }
}