using System;

namespace MediaPrint.UnitTests
{
    public class PrintableClass : IPrintable
    {
        public PrintableClass(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }

        public void PrintTo(IMedia media)
        {
            _ = media ?? throw new ArgumentNullException(nameof(media));
            media.Put(nameof(Name), Name)
                 .Put(nameof(Date), Date);
        }

        public override bool Equals(object obj)
        {
            return obj is PrintableClass @class &&
                   Name == @class.Name &&
                   Date == @class.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Date);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }
    }
}
