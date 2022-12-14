using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.Customers;

public class CreateCustomer
{
    public CreateCustomer(
        string fullName,
        string email,
        string emailConfirmation,
        string cpf,
        string cellphone,
        string country,
        string city,
        string address,
        string postalCode,
        int number,
        bool emailSms,
        bool whatsapp,
        DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        EmailConfirmation = emailConfirmation;
        Cpf = cpf;
        Cellphone = cellphone;
        Country = country;
        City = city;
        Address = address;
        PostalCode = postalCode;
        Number = number;
        EmailSms = emailSms;
        Whatsapp = whatsapp;
        DateOfBirth = dateOfBirth;
    }

    public string FullName { get; set; }
    public string Email { get; set; }
    public string EmailConfirmation { get; set; }
    public string Cpf { get; set; }
    public string Cellphone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public int Number { get; set; }
    public bool EmailSms { get; set; }
    public bool Whatsapp { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }
}