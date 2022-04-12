﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BASE.MICRONET.Account.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCustomer { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
