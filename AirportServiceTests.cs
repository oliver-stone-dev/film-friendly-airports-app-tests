using film_friendly_airports_app;
using Microsoft.EntityFrameworkCore;
using film_friendly_airports_app.Models;
using film_friendly_airports_app.Services;

namespace film_friendly_airports_app_tests;

[TestClass]
public class AirportServiceTests
{
    private Database _database;

    public AirportServiceTests()
    {
        //Create in memory test database
        var options = new DbContextOptionsBuilder<Database>()
        .UseInMemoryDatabase("TestDatabase")
        .Options;

        _database = new(options);
    }


    //Test if we get the correct airport when an airport id is entered
    [TestMethod]
    public void GetSpecificAirportTest()
    {
        var airport1 = new Airport();
        airport1.AirportId = 1;
        airport1.Name = "Heathrow";
        airport1.Code = "LHR";
        airport1.NoTerminals = 4;

        var airport2 = new Airport();
        airport2.AirportId = 2;
        airport2.Name = "Gatwick";
        airport2.Code = "GTW";
        airport2.NoTerminals = 2;

        _database.Airports.Add(airport1);
        _database.Airports.Add(airport2);

        _database.SaveChanges();

        AirportService service = new(_database);

        var data = service.GetAirportById(2);

        Assert.AreEqual<Airport>(data, airport2);
    }
}
