using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModels
{
    public class Winner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Fullname{ get; set; }
        [NotMapped]
        public IEnumerable<int> TicketCombination { get; set; }
        public int Prize { get; set; }
        public int SessionId { get; set; }
    }
    

}
