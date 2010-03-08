namespace Web_App_101.Models
{
    public class Audit
    {
        private readonly decimal _amount;
        private readonly int _id;

        public Audit(int id, decimal amount)
        {
            _id = id;
            _amount = amount;
        }

        public decimal Amount
        {
            get { return _amount; }
        }

        public int Id
        {
            get { return _id; }
        }
    }
}