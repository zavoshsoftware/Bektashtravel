using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helpers
{
    public static class GuidChecker
    {
        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }
    }
}