using System;
using System.Collections.Generic;

namespace MediaPrint.UnitTests
{
    public class PrintableClassWithIPrintableArray : IPrintable
    {
        private readonly string _description;
        private readonly IEnumerable<PrintableClass> _items;

        public PrintableClassWithIPrintableArray(string description, IEnumerable<PrintableClass> items)
        {
            _description = description;
            _items = items;
        }

        public void PrintTo(IMedia media)
        {
            _ = media ?? throw new ArgumentNullException(nameof(media));
            media
                .Put("Desciption", _description)
                .Put("Items", _items);
        }

        public override string ToString()
        {
            return this.ToJson().ToString();
        }
    }
}
