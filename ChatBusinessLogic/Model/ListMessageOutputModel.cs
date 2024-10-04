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
        public string[] Content { get; set; }
        public int[] AuthorID { get; set; }
        public string[] AuthorAvatar { get; set; }
        public string[] Time { get; set; }
        public int[] Status { get; set; }
        public int Count {  get; set; }
    }
}
