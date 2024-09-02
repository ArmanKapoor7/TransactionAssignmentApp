using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransactionAssignmentApp.Models
{
    public partial class Transaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public int? Credit { get; set; }
        public int? Debit { get; set; }
        public int Balance { get; set; }
        public int Id { get; set; }
    }
}
