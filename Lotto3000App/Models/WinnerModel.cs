using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class WinnerModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public IEnumerable<int> TicketCombination { get; set; }
        public Prize Prize { get; set; }
        public int SessionId { get; set; }
    }
    public enum Prize
    {
        Car = 1,
        Vacation = 2,
        Tv = 3,
        GiftCard = 4,
        NoPrize = 5

    }
}
