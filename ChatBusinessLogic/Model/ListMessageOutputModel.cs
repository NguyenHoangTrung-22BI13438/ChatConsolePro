using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessLogic.Model
{
    public class ListMessageOutputModel
    {
        public required string[] Content { get; set; }
        public required int[] AuthorID { get; set; }
        public required string[] AuthorAvatar { get; set; }
        public required string[] Time { get; set; }
        public required int[] Status { get; set; }
        public int Count { get; set; }
    }

}
