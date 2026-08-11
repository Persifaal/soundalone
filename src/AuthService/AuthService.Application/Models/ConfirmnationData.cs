using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record ConfirmnationData 
    {
        public Guid CodeId { get; set; }
        public string HashCode { get; set; }
        public int Attempts { get; set; }
        public int MaxAttempts { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
    }
}
