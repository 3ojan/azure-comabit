// <copyright file="RegisterBaseItem.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Identity.Dto
{
    public class RegisterBaseItem
    {
        public string EMail { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Company { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string BusinessTaxId { get; set; }

        public string UstId { get; set; }

        public string ContactPersonFirstname { get; set; }

        public string ContactPersonLastname { get; set; }
    }
}