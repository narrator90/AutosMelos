using Autokereskedes_BS.Data;
using Autokereskedes_BS.Data.Models;
using Bito_Sandor_Autokereskedes.Data.Models;

namespace Autokereskedes_BS.Services.Interfaces
{
    public interface ICarServices
    {
        /// <summary>
        /// Deletes a brand suggestion.
        /// </summary>
        /// <param name="bs">The BrandSuggest object to be deleted.</param>
        void BrandSuggestDelete(BrandSuggest bs);

        /// <summary>
        /// Adds a new car brand suggestion.
        /// </summary>
        /// <param name="cb">The BrandSuggest object to be added.</param>
        void CarBrandSuggest(BrandSuggest cb);

        /// <summary>
        /// Generic method to create an entity of type T.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool Create<T>(T entity) where T : class;

        /// <summary>
        /// Creates a new car brand.
        /// </summary>
        /// <param name="cb">The CarBrand object to be created.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool CreateBrand(CarBrand cb);

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="c">The Car object to be created.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool CreateCar(Car c);

        /// <summary>
        /// Creates a new car model.
        /// </summary>
        /// <param name="model">The CarModel object to be created.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool CreateModel(CarModel model);

        /// <summary>
        /// Retrieves all cars, including related model, brand, and user information.
        /// </summary>
        /// <returns>A list of all Car objects.</returns>
        List<Car> GetAllCar();

        /// <summary>
        /// Retrieves all car brands.
        /// </summary>
        /// <returns>A list of all CarBrand objects.</returns>
        List<CarBrand> GetAllCarBrand();

        /// <summary>
        /// Retrieves all car models, including the related brand information.
        /// </summary>
        /// <returns>A list of all CarModel objects.</returns>
        List<CarModel> GetAllCarModel();

        /// <summary>
        /// Retrieves all car models for a specific brand.
        /// </summary>
        /// <param name="brand">The CarBrand object representing the brand.</param>
        /// <returns>A list of CarModel objects associated with the specified brand.</returns>
        List<CarModel> GetAllCarModelByBrand(CarBrand brand);

        /// <summary>
        /// Retrieves all car models for a specific brand by the brand's ID.
        /// </summary>
        /// <param name="id">The ID of the car brand.</param>
        /// <returns>A list of CarModel objects associated with the specified brand ID.</returns>
        List<CarModel> GetAllCarModelByBrandId(int id);

        /// <summary>
        /// Retrieves all brand suggestions.
        /// </summary>
        /// <returns>A list of all BrandSuggest objects.</returns>
        List<BrandSuggest> GetAllSuggest();

        /// <summary>
        /// Retrieves all users who own a specific car model.
        /// </summary>
        /// <param name="cm">The CarModel object representing the car model.</param>
        /// <returns>A list of ApplicationUser objects who own the specified car model.</returns>
        List<ApplicationUser> GetAllUserWithThisCarModel(CarModel cm);

        /// <summary>
        /// Retrieves a car by its ID, including related model, brand, and user information.
        /// </summary>
        /// <param name="id">The ID of the car to retrieve.</param>
        /// <returns>The Car object with the specified ID, or null if not found.</returns>
        Car GetCarById(int id);

        /// <summary>
        /// Retrieves a car model by its name and brand ID.
        /// </summary>
        /// <param name="name">The name of the car model.</param>
        /// <param name="brandID">The ID of the car brand (optional).</param>
        /// <returns>The CarModel object matching the specified name and brand ID, or null if not found.</returns>
        CarModel GetCarModelByNameAndBrandId(string name, int? brandID);

        /// <summary>
        /// Retrieves all cars associated with a specific user.
        /// </summary>
        /// <param name="user">The ApplicationUser object representing the user.</param>
        /// <returns>A list of Car objects associated with the specified user.</returns>
        List<Car> GetCarsByUser(ApplicationUser user);

        /// <summary>
        /// Retrieves all cars associated with a specific user by their user ID.
        /// </summary>
        /// <param name="userid">The ID of the user.</param>
        /// <returns>A list of Car objects associated with the specified user.</returns>
        List<Car> GetCarsByUserId(string userid);

        /// <summary>
        /// Creates a new car brand from a suggestion and removes the suggestion from the database.
        /// </summary>
        /// <param name="bs">The BrandSuggest object containing the new car brand's name.</param>
        void NewCarBrandBySuggest(BrandSuggest bs);

        /// <summary>
        /// Generic method to update an entity of type T.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="model">The entity to be updated.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool Update<T>(T model) where T : class;

        /// <summary>
        /// Updates a car brand.
        /// </summary>
        /// <param name="cb">The CarBrand object to be updated.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool UpdateBrand(CarBrand cb);

        /// <summary>
        /// Updates a car.
        /// </summary>
        /// <param name="c">The Car object to be updated.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool UpdateCar(Car c);

        /// <summary>
        /// Updates a car model.
        /// </summary>
        /// <param name="model">The CarModel object to be updated.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool UpdateModel(CarModel model);

        /// <summary>
        /// Generic method to delete an entity of type T.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="model">The entity to be deleted.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool Delete<T>(T model) where T : class;

        /// <summary>
        /// Gets a list of images for a specific car.
        /// </summary>
        /// <param name="car">The car for which images are to be retrieved.</param>
        /// <returns>A list of CarImage objects associated with the given car.</returns>
        List<CarImage> GetImages(Car car);

        /// <summary>
        /// Gets a list of images for a car by its ID.
        /// </summary>
        /// <param name="carId">The ID of the car.</param>
        /// <returns>A list of CarImage objects associated with the car with the given ID.</returns>
        List<CarImage> GetImages(int carId);
    }
}
