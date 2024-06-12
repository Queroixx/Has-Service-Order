namespace OsDsII.api.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public StatusServiceOrder Status { get; set; }
        public DateTimeOffset OpeningDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset FinishDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Comment> Comments { get; set; }

        public bool CanFinish()
        {
            return StatusServiceOrder.OPEN.Equals(Status);
        }
        public void FinishOS()
        {
            if (!CanFinish())
            {
                throw new Exception($"Service order cannot be finished due to current status {Status}");
            }

            Status = StatusServiceOrder.FINISHED;
            FinishDate = DateTimeOffset.Now;
        }

        public void Cancel()
        {
            if (!CanFinish())
            {
                throw new Exception($"Service order cannot be canceled due to current status {Status}");
            }

            Status = StatusServiceOrder.CANCELED;
        }
    }
}