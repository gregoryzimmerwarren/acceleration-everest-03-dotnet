namespace DomainModels.Entities
{
    public static class StringExtensions
    {
        public static string CpfFormatter(this string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace(",", "").Replace("-", "");
        }
    }   
}
