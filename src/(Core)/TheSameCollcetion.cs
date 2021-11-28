using System.Collections;

namespace MediaPrint.Core
{
    public class TheSameCollcetion
    {
        private readonly IEnumerable _expected;
        private readonly IEnumerable _actual;

        public TheSameCollcetion(IEnumerable expected, IEnumerable actual)
        {
            _expected = expected;
            _actual = actual;
        }

        public bool ToBoolean()
        {
            if (object.ReferenceEquals(_expected, _actual))
            {
                return true;
            }

            var contains = false;
            foreach (var expectedItem in _expected)
            {
                contains = true;
                if (!ActualContains(expectedItem))
                {
                    return false;
                }
            }

            return contains;
        }

        private bool ActualContains(object item)
        {
            var contains = false;
            foreach (object actualItem in _actual)
            {
                if (new TheSameObject(item, actualItem).ToBool())
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
    }
}
