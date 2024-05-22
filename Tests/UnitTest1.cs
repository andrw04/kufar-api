using KufarAPI;
using KufarAPI.Models;
using NetTopologySuite.Geometries;

namespace Tests;

public class Task2Tests
{
    private List<SellAd> _ads = new();

    public Task2Tests()
    {
        _ads.Add(new SellAd()
        {
            AdId = 235654844,
            AdLink = "https://re.kufar.by/vi/235654844",
            SellAdParameters = new SellAdParameters()
            {
                Area = "Советский",
                Floor = 3,
                Metro = "",
                Latitude = 53.9605f,
                Longitude = 27.6146f,
            }
        });
    }
    
    
    [Fact]
    public void CorrectShapeContains1Test()
    {
        // Arrange
        var coordinates = new Coordinate[]
        {
            new Coordinate(53.970517, 27.552804),
            new Coordinate(53.902546, 27.557292),
            new Coordinate(53.958167, 27.722978),
            new Coordinate(53.970517, 27.552804)
        };
        
        // Act
        var result = Task2.GetAdsInShape(_ads, coordinates);
        // Assert
        Assert.Equal(result, _ads);
    }
    
    [Fact]
    public void CorrectShapeEmptyTest()
    {
        // Arrange
        var coordinates = new Coordinate[]
        {
            new Coordinate(53.952314, 27.404861),
            new Coordinate(53.960244, 27.290798),
            new Coordinate(53.960244, 27.290798),
            new Coordinate(53.952314, 27.404861)
        };
        
        // Act
        var result = Task2.GetAdsInShape(_ads, coordinates);
        // Assert
        Assert.Empty(result);
    }
    
    
    [Fact]
    public void IncorrectShapeTest()
    {
        // Arrange
        var coordinates = new Coordinate[]
        {
            new Coordinate(53.970517, 27.552804),
            new Coordinate(53.970517, 27.552804)
        };
        
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Task2.GetAdsInShape(_ads, coordinates));
    }
}