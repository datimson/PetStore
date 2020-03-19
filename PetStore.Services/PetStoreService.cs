using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public class PetStoreService
    {
        private readonly IPetstoreClient _client;
        public PetStoreService(IPetstoreClient client)
        {
            _client = client;
        }

        public async Task<List<Pet>> GetAvailablePets()
        {
            var pets = await _client.FindPetsByStatusAsync(new[] { Anonymous.Available });

            if (pets == null)
            {
                return new List<Pet>();
            }

            return pets.ToList();
        }
    }
}
