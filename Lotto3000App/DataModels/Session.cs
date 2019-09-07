using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModels
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public IEnumerable<int> WinningCombination { get; set; }
        public DateTime Created { get; set; }
        public bool IsClosed { get; set; }
      
    }
}
