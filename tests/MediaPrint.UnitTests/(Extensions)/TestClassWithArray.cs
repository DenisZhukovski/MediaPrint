using System.Collections.Generic;

namespace MediaPrint.UnitTests
{
    public class TestClassWithArray : IPrintable
    {
        private readonly string _description;
        private readonly IEnumerable<TestClass> _items;

        public TestClassWithArray(string description, IEnumerable<TestClass> items)
        {
            _description = description;
            _items = items;
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("Desciption", _description)
                .Put("Items", _items);
        }
    }
}
