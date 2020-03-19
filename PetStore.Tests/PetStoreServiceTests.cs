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

        public class SortPetsByCategoryAndName
        {
            [Fact]
            public void WhenNoPets_ThenReturnEmptyDictionary()
            {
                var mockPetstoreClient = new Mock<IPetstoreClient>();
                var service = new PetStoreService(mockPetstoreClient.Object);
                var sortedPets = service.SortPetsByCategoryAndName(null);

                Assert.NotNull(sortedPets);
                Assert.Empty(sortedPets);
            }

            [Fact]
            public void WhenPets_ThenReturnSortedDictionary()
            {
                var categoryA = new Category { Id = 1, Name = "Category A" };
                var categoryB = new Category { Id = 2, Name = "Category B" };

                var pet1 = new Pet { Id = 1, Name = "Pet 1", Status = PetStatus.Available, Category = categoryA };
                var pet2 = new Pet { Id = 2, Name = "Pet 2", Status = PetStatus.Available, Category = categoryA };
                var pet3 = new Pet { Id = 3, Name = "Pet 3", Status = PetStatus.Available, Category = categoryB };
                var pets = new List<Pet> { pet1, pet2, pet3 };

                // categories are sorted by name descending and pets by name descending
                var expected = new Dictionary<string, List<Pet>>
                {
                    { categoryB.Name, new List<Pet> { pet3 } },
                    { categoryA.Name, new List<Pet> { pet2, pet1 } }
                };

                var mockPetstoreClient = new Mock<IPetstoreClient>();
                var service = new PetStoreService(mockPetstoreClient.Object);
                var actual = service.SortPetsByCategoryAndName(pets);

                Assert.Equal(expected, actual);
            }
        }
    }
}
