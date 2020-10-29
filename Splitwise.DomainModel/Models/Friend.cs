using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.DomainModel.Models
{
    public class Friend
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User1 { get; set; }

        public string FriendId { get; set; }
        [ForeignKey("FriendId")]
        public ApplicationUser User2 { get; set; }
    }
}
