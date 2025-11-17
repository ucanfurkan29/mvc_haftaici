namespace _12_Dependency_Injection_2.Services
{
    public class ScopedHelperService
    {
        private readonly ScopedRandomNumberService _scopedService;
        public ScopedHelperService(ScopedRandomNumberService service)
        {
            _scopedService = service;
        }
        public int GetScopedRandom()
        {
            return _scopedService.GetRandomNumber();
        }
    }
}
