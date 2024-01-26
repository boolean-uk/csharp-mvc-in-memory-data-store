namespace exercise.wwwapi.Model
{
    public abstract class BaseObject
    {
        private static uint _id = 0;
        public uint ID { get; internal set; }

        public BaseObject()
        {

        }
    }
}
