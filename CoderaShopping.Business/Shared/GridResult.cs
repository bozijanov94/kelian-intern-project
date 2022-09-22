using System.Collections.Generic;

namespace CoderaShopping.Business
{
    public class GridResult<T>
    {
        public int TotalItems;
        public IList<T> Items;

        public GridResult()
        {
            Items = new List<T>();
        }
    }
}