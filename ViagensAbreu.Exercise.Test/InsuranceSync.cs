using FluentAssertions;
using ViagensAbreu.Exercise.Application.Services;
using ViagensAbreu.Exercise.Domain.LocalAggregator;
using ViagensAbreu.Exercise.Domain.ProviderAggregator;

namespace ViagensAbreu.Exercise.Test;

public class InsuranceSync
{
    [Fact]
    // Given the insurance exists in the provider list and in the local list
    // When the sync process is executed,
    // Then the provider list should remain unchanged
    public void ExistInBothList()
    {
        // Arrange
        ProviderRecord[] providerList = [
            new ProviderRecord("Provider-1", DateTime.UtcNow, DateTime.UtcNow.AddDays(1), 100),
            new ProviderRecord("Provider-2", DateTime.UtcNow, DateTime.UtcNow.AddDays(1), 200)
        ];
        
        LocalRecord[] localList = [
            new LocalRecord(0, "Provider-1", 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1))
        ];
        
        ProviderRecord[] expectedResult =  [providerList[0]];
        
        // Act
        var insuranceService = new InsuranceService();
        var result = insuranceService.SyncInsurances(providerList, localList);

        // Assert
        result.Providers.Should().BeEquivalentTo(expectedResult);
        result.Status.Unchanged.Should().Be(1);
        result.Status.Added.Should().Be(0);
        result.Status.Removed.Should().Be(1);
    }

    [Fact]
    // Given the insurance exists in the provider list and not in the local list
    // When the sync process is executed,
    // Then the provider list should remove the insurance
    public void AddToProviderList()
    {
        // Arrange
        ProviderRecord[] providerList = [];
        
        LocalRecord[] localList = [
            new LocalRecord(0, "Provider-3", 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1))
        ];
        
        ProviderRecord[] expectedResult =  [
            new ProviderRecord(localList[0].ProviderName, localList[0].BeginDate, localList[0].EndDate, localList[0].Price)
        ];
        
        // Act
        var insuranceService = new InsuranceService();
        var result = insuranceService.SyncInsurances(providerList, localList);

        // Assert
        result.Providers.Should().BeEquivalentTo(expectedResult);
        result.Status.Unchanged.Should().Be(0);
        result.Status.Added.Should().Be(1);
        result.Status.Removed.Should().Be(0);
    }
    
    [Fact]
    // Given the insurance exists in the local list and not in the provider list
    // When the sync process is executed,
    // Then the provider list should add the insurance
    public void RemoveFromProviderList()
    {
        // Arrange
        ProviderRecord[] providerList = [
            new ProviderRecord("Provider-1", DateTime.UtcNow, DateTime.UtcNow.AddDays(1), 100)
        ];
        
        LocalRecord[] localList = [];
        
        ProviderRecord[] expectedResult =  [];
        
        // Act
        var insuranceService = new InsuranceService();
        var result = insuranceService.SyncInsurances(providerList, localList);

        // Assert
        result.Providers.Should().BeEquivalentTo(expectedResult);
        result.Status.Unchanged.Should().Be(0);
        result.Status.Added.Should().Be(0);
        result.Status.Removed.Should().Be(1);
    }
}