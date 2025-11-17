namespace _12_Dependency_Injection_2.Services
{
    public class SingletonRandomNumberService
    {
        private readonly int _randomNumber;
        public SingletonRandomNumberService()
        {
            _randomNumber=new Random().Next(1, 1000);
        }
        public int GetRandomNumber()
        {
            return _randomNumber;
        }
    }
}
