using System;

namespace MediaPrint.UnitTests
{
    public class TestClass : IPrintable
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TestClass @class &&
                   Name == @class.Name &&
                   Date == @class.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Date);
        }

        public void PrintTo(IMedia media)
        {
            media.Put("Name", Name)
                 .Put("Date", Date);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }
    }
}
