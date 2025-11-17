namespace _12_Dependency_Injection_2.Services
{
    public class ScopedRandomNumberService : IRandomNumberService
    {
        private readonly int _randomNumber;
        public ScopedRandomNumberService()
        {
            _randomNumber=new Random().Next(1,1000);
        }
        public int GetRandomNumber()
        {
           return _randomNumber;
        }
    }
}
