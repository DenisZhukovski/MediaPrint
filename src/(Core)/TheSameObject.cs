using System.Collections;

namespace MediaPrint.Core
{
    public class TheSameObject
    {
        private readonly object _expected;
        private readonly object _actual;

        public TheSameObject(object expected, object actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public bool ToBool()
        {
            if (object.ReferenceEquals(_expected, _actual))
            {
                return true;
            }

            if (_expected != null)
            {
                if (_expected is IEnumerable expectedItems && _actual is IEnumerable actualItems)
                {
                    return new TheSameCollcetion(expectedItems, actualItems).ToBoolean();
                }
                else if (_expected is IDictionary expectedDictionary && _actual is IDictionary actualDictionary)
                {
                    return new TheSameDictionary(expectedDictionary, actualDictionary).ToBool();
                }
                else
                {
                    return _expected.Equals(_actual);
                }
            }

            return _actual == null;
        }
    }
}
