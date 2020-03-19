using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PetStore.Services;
using Xunit;

namespace PetStore.Tests
{
    public class PetStoreServiceTests
    {
        public class GetAvailablePets
        {
            [Fact]
            public async Task WhenNoPets_ThenReturnEmptyList()
            {
                var mockPetstoreClient = new Mock<IPetstoreClient>();
                mockPetstoreClient.Setup(x => x.FindPetsByStatusAsync(new[] { Anonymous.Available })).ReturnsAsync(null as ICollection<Pet>);

                var service = new PetStoreService(mockPetstoreClient.Object);
                var pets = await service.GetAvailablePets();

                Assert.NotNull(pets);
                Assert.Empty(pets);
            }

            [Fact]
            public async Task WhenPets_ThenReturnList()
            {
                var mockPetstoreClient = new Mock<IPetstoreClient>();
                var pets = new Pet[] {
                    new Pet { Id = 1, Name = "Test 1", Status = PetStatus.Available },
                    new Pet { Id = 2, Name = "Test 2", Status = PetStatus.Available }
                };
                mockPetstoreClient.Setup(x => x.FindPetsByStatusAsync(new[] { Anonymous.Available })).ReturnsAsync(pets);

                var service = new PetStoreService(mockPetstoreClient.Object);
                var petsResult = await service.GetAvailablePets();

                Assert.NotNull(petsResult);
                Assert.Equal(pets, petsResult);
            }
        }
    }
}
