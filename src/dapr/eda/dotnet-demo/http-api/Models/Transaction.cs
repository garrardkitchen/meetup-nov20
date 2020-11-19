using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_demo.Models
{
    [Serializable]
    public class Transaction
    {
        public string Id { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public Transaction()
        {
        }
    }
}