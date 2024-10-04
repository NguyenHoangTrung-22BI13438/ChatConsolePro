using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBusinessLogic.Model
{
    public class AddMessageModel
    {
        public int RelationshipID {  get; set; }
        public string Content { get; set; }
        public int AuthorID { get; set; }
    }
}
