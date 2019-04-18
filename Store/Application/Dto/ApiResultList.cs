using System;

namespace Store
{
    public class ApiResultList<T>
    {
        public T Result { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate => DateTime.Now;
    }
}
