﻿using CarWebService.Dtos;
using CarWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CarWebService.Managers
{
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