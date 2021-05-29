namespace lab4
{
    abstract class Vehicle
    {
        public string SerialNum {get; private set;}

        public Vehicle(string serialNum)
        {
            SerialNum = serialNum;
        }
    }
    class Trolley : Vehicle
    {
        public Trolley(string serialNum) : base(serialNum) {}

        public override string ToString()
        {
            return "Trolley " + SerialNum;
        }
    }
}