namespace CoreMVCWebApp.Service
{

    public interface IHelloWorldService
    {
        string SaysHello();
    }
    public class HelloWorldService : IHelloWorldService
    {
        public HelloWorldService()
        {

        }

        public string SaysHello()
        {
            return ".. Hello from Hello Service";
        }
    }
}
