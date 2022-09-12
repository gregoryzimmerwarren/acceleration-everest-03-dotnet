namespace Data.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailConfirmation { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int Number { get; set; }
        public bool EmailSms { get; set; }
        public bool Whatsapp { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
