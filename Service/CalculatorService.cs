namespace CoreMVCWebApp.Service
{
    public interface ICalculatorService
    {
        int Add(int n1, int n2);
        int Subtract(int n1, int n2);
        int Multiply(int n1, int n2);
        int Divide(int n1, int n2);
    }

    public class CalculatorService : ICalculatorService
    {
        public int Add(int n1, int n2)
        {
            return n1 + n2;
        }

        public int Divide(int n1, int n2)
        {
            return (n1 - n2);
        }

        public int Multiply(int n1, int n2)
        {
            return(n1 * n2);
        }

        public int Subtract(int n1, int n2)
        {
            return(n1 - n2);
        }
    }
}
