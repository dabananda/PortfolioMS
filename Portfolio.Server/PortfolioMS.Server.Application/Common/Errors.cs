using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortfolioMS.Server.Application.Common
{
    public static class Errors
    {
        public static readonly Error None = new("", "");
        public static readonly Error NullValue = new("General.Null", "A null value was provided.");
    }
}
