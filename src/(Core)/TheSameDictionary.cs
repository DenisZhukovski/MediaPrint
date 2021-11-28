using System.Collections;

namespace MediaPrint.Core
{
    internal class TheSameDictionary
    {
        private readonly IDictionary _expected;
        private readonly IDictionary _actual;

        public TheSameDictionary(IDictionary expected, IDictionary actual)
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

            if (_expected.Keys.Count == _actual.Keys.Count)
            {
                foreach (var key in _expected.Keys)
                {
                    if (!_actual.Contains(key) || !new TheSameObject(_actual[key], _expected[key]).ToBool())
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
