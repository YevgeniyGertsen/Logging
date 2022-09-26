namespace WebApplication3.Models
{
    public interface IMathService
    {
        decimal Divider(decimal a, decimal b);
    }

    public class MathService : IMathService
    {
        private readonly ILogger<MathService> _logger;

        public MathService(ILogger<MathService> logger)
        {
            _logger = logger;
        }

        public decimal Divider(decimal a, decimal b)
        {
            _logger.LogInformation("Parametr #1 {param_1}", a);
            _logger.LogInformation("Parametr #2 {param_2}", b);
            decimal result = 0;
            try
            {
                result = a / b;
            }
            catch (DivideByZeroException ex)
            {
                _logger.LogError(ex, "Деление на ноль запрещено");
                throw ex;
            }
            return result;
        }
    }
}
