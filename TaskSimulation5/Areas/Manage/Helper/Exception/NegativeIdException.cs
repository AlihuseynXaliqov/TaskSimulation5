namespace TaskSimulation5.Areas.Manage.Helper.Exception
{
    public class NegativeIdException:System.Exception
    {
        public NegativeIdException() : base("Id menfi ve ya sifir ola bilmez") { }
        
        public NegativeIdException(string message) : base(message) { }
    }
}

