namespace _12_Dependency_Injection_2.Services
{
    public class TransientRandomNumberService
    {
        private readonly int _randomNumber;
        public TransientRandomNumberService()
        {
            _randomNumber = new Random().Next(1, 1000);
        }
        public int GetRandomNumber()
        {
            return _randomNumber;
        }
    }
}
