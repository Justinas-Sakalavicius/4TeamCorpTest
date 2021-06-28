using External.Dto;
using System;
using System.Linq;

namespace External.Generator
{
    public class ClientGenerator : IClientGenerator
    {
        private static Random random;
        public ClientGenerator()
        {
            random = new Random();
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ClientDto Generate()
        {
            return new ClientDto
            {
                FirstName = RandomString(8),
                MiddleName = RandomString(2),
                LastName = RandomString(12),
                Age = random.Next(1, 100)
            };
        }
    }
}
