using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_demo.Models
{
    [Serializable]
    public class TransactionRange
    {
        public string Id { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal LowAmount { get; set; }
        
        [Range(0, double.MaxValue)]
        public decimal HighAmount { get; set; }

        public int? Delay { get; set; } = 0;
        
        public bool Busy { get; set; } = false;


        /// <summary>
        /// The numbmer of times this transactions ought to be processed
        /// </summary>
        public int Repetitions { set; get; }
        
        public TransactionRange()
        {
        }
    }
    
    
    [Serializable]
    public class Transaction
    {
        public string Id { get; set; }
        
        public decimal Amount { set; get; }
        
        public Transaction()
        {
        }
    }
}