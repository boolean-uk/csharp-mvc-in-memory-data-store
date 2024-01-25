namespace exercise.wwwapi.Model
{
    public class InternalProduct : Product
    {
        private int _availabelID = 1;
        public int Id { get; set; }

        public InternalProduct(string name, string category, int price) : base(name, category, price)
        {
            Id = _availabelID++;
        }
    }
}
