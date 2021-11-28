using System;
using System.Collections.Generic;
using MediaPrint.Core;

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

        public override bool Equals(object obj)
        {
            return obj is PrintableClassWithIPrintableArray array
                && _description == array._description
                && new TheSameCollcetion(_items, array._items).ToBoolean();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_description, _items);
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
