﻿using System;
using System.Collections.Generic;

namespace MediaPrint.UnitTests
{
    public class TestClassWithDictionary : IPrintable
    {
        private readonly string _description;
        private readonly Dictionary<string, object> _items;

        public TestClassWithDictionary(string description, IEnumerable<KeyValuePair<string, object>> items)
            : this(description, new Dictionary<string, object>(items))
        {
        }

        public TestClassWithDictionary(string description, Dictionary<string, object> items)
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
    }
}
