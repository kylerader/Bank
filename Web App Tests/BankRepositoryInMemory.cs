using Web_App_101.Models;

namespace Web_App_Tests
{
    public class BankRepositoryInMemory : IBankRepository
    {
        private static readonly Bank ShittyBank = new Bank("ShittyBank", 720);
        
        public Bank GetBank()
        {
            return ShittyBank;
        }
    }
}