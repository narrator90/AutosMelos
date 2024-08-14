using Autokereskedes_BS.Data;
using Autokereskedes_BS.Data.Models;
using Autokereskedes_BS.Services.Interfaces;
using Bito_Sandor_Autokereskedes.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace Autokereskedes_BS.Services
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationDbContext _context;
        public CarServices(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets a list of images for a specific car.
        /// </summary>
        /// <param name="car">The car for which images are to be retrieved.</param>
        /// <returns>A list of CarImage objects associated with the given car.</returns>
        public List<CarImage> GetImages(Car car) => _context.Images.Include(x => x.Car).Where(x => x.Car == car).ToList();
        public List<CarImage> GetImages(int carId) => GetImages(_context.Cars.FirstOrDefault(x => x.Id == carId));
        public List<BrandSuggest> GetAllSuggest() => _context.brandSuggests.Include(x => x.User).ToList();
        public void CarBrandSuggest(BrandSuggest cb) { _context.brandSuggests.Add(cb);  _context.SaveChanges(); }
        public void NewCarBrandBySuggest(BrandSuggest bs)
        {
            _context.Brands.Add(new CarBrand
            {
                Name = bs.SuggestedName
            });
            _context.brandSuggests.Remove(bs);
            _context.SaveChanges();
        }

        public void BrandSuggestDelete(BrandSuggest bs)
        {
            _context.brandSuggests.Remove(bs);
            _context.SaveChanges();
        }

        public Car GetCarById(int id) => _context.Cars.Include(x => x.CarModel).ThenInclude(m => m.CarBrand).Include(x => x.User).FirstOrDefault(x => x.Id == id);
        public List<Car> GetAllCar() => _context.Cars.Include(x=>x.CarModel).ThenInclude(m => m.CarBrand).Include(x=>x.User).ToList();
        public List<Car> GetCarsByUserId(string userid) => _context.Cars.Include(x => x.CarModel).ThenInclude(m => m.CarBrand).Include(x => x.User).Where(x => x.UserId == userid).ToList();
        public List<Car> GetCarsByUser(ApplicationUser user) => _context.Cars.Include(x => x.CarModel).ThenInclude(m => m.CarBrand).Include(x => x.User).Where(x => x.User == user).ToList();
        public List<CarModel> GetAllCarModel() => _context.CarModels.Include(c => c.CarBrand).ToList();
        public List<CarModel> GetAllCarModelByBrand(CarBrand brand) => _context.CarModels.Include(c => c.CarBrand).Where(x => x.CarBrand == brand).ToList();
        public List<CarModel> GetAllCarModelByBrandId(int id) => _context.CarModels.Include(x => x.CarBrand).Where(m => m.CarBrandId == id).ToList();
        public List<ApplicationUser> GetAllUserWithThisCarModel(CarModel cm) => _context.Users.Include(u => u.Cars).Where(c => c.Cars.Any(m => m.CarModelId == cm.Id)).ToList();
        public List<CarBrand> GetAllCarBrand() => _context.Brands.ToList();
        public CarModel GetCarModelByNameAndBrandId(string name, int? brandID) => _context.CarModels.FirstOrDefault(m => m.Name == name && m.CarBrandId == brandID);
        public bool Create<T> (T entity) where T : class
        {
            try
            {
                _context.Add<T>(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateModel(CarModel model)
        {
            try
            {
                _context.CarModels.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateBrand(CarBrand cb)
        {
            try
            {
                _context.Brands.Add(cb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateCar(Car c)
        {
            try
            {
                _context.Cars.Add(c);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update<T> (T model) where T : class
        {
            try
            {
                _context.Update<T>(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateModel(CarModel model)
        {
            try
            {
                _context.CarModels.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateBrand(CarBrand cb)
        {
            try
            {
                _context.Brands.Update(cb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCar(Car c)
        {
            try
            {
                _context.Cars.Update(c);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete<T>(T model) where T : class
        {
            try
            {
                _context.Remove<T>(model);
                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
