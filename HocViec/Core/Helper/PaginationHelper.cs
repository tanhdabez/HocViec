using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class PaginationHelper
    {
        public static List<T>? GetPage<T>(List<T>? sourceList, int pageNumber, int pageSize)
        {
            ValidatePageParameters(pageNumber, pageSize);

            return sourceList?.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        private static void ValidatePageParameters(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("Page number and page size must be greater than zero.");
            }
        }
    }
}
