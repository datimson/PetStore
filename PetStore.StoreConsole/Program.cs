using System;
using System.Net.Http;
using System.Threading.Tasks;
using PetStore.Services;

namespace PetStore.StoreConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Available Pets:");

            var service = new PetStoreService(new PetstoreClient(new HttpClient()));
            var availablePets = await service.GetAvailablePets();
            var sortedByCategory = service.SortPetsByCategoryAndName(availablePets);

            foreach (var category in sortedByCategory)
            {
                Console.WriteLine(category.Key);

                foreach (var pet in category.Value)
                {
                    Console.WriteLine($"    { pet.Name }, Id: {pet.Id})");
                }
            }

            Console.ReadLine();
        }
    }
}
