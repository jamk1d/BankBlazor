namespace BankBlazor_ClassLibrary.DTOs
{
    public class CustomerDto
    {
        public string Gender { get; set; } = null!;

        public string Givenname { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Streetaddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string? Telephonenumber { get; set; }

        public string? Emailaddress { get; set; }

        public DateOnly? Birthday { get; set; }


    }
}
