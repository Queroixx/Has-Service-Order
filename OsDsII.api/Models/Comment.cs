using System.Diagnostics.CodeAnalysis;

namespace OsDsII.api.Models
{

    public class Comment
    {
        public long Id { get; set; }
        [NotNull]
        public string Description { get; set; } = null!;
        public int ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; }
        public DateTimeOffset SendDate { get; set; } = DateTimeOffset.UtcNow;


        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}