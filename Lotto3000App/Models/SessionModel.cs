using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public IEnumerable<int> WinningCombination { get; set; }
        public DateTime Created { get; set; }
        public bool IsClosed { get; set; }


        public SessionModel()
        {
            Created = DateTime.Now;
            IsClosed = false;
        }
    }
}
