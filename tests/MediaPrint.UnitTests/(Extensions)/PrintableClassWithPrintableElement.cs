using System;

namespace MediaPrint.UnitTests
{
    public class PrintableClassWithPrintableElement : IPrintable
    {
        private readonly string _description;
        private readonly IPrintable _item;

        public PrintableClassWithPrintableElement(string description, IPrintable item)
        {
            _description = description;
            _item = item;
        }
       
        public void PrintTo(IMedia media)
        {
            _ = media ?? throw new ArgumentNullException(nameof(media));
            media
                .Put("Desciption", _description)
                .Put("Item", _item);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }
    }
}
