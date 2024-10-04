using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessLogic.Model
{
    public class FriendOutputModel
    {
        public string[] Name { get; set; }
        public string[] LastMessage { get; set; }
        public int[] Status { get; set; }
        public string[] Avatar { get; set; }
        public int Count {  get; set; }
        public int[] RelationshipID {  get; set; }
    }
}
