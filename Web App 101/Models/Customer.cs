namespace Web_App_101.Models
{
    public class Customer
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly int _ficoScore;
        private readonly int _id;

        public Customer(int id, string firstName, string lastName, int ficoScore)
        {
            _firstName = firstName;
            _lastName = lastName;
            _ficoScore = ficoScore;
            _id = id;
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public int FicoScore
        {
            get { return _ficoScore; }
        }

        public int Id
        {
            get { return _id; }
        }
    }
}