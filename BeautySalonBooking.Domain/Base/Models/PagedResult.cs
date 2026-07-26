using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Domain.Base.Models
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
        public IReadOnlyList<T> Items { get; set; } = new List<T>();

        public PagedResult() { }

        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items.ToList();
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        // متد کمکی برای تبدیل به نوع دیگر
        public PagedResult<TResult> Map<TResult>(Func<T, TResult> mapFunc)
        {
            return new PagedResult<TResult>
            {
                Items = Items.Select(mapFunc).ToList(),
                TotalCount = TotalCount,
                PageNumber = PageNumber,
                PageSize = PageSize,
                TotalPages = TotalPages
            };
        }
    }
}
