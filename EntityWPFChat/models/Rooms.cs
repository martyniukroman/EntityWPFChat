using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityWPFChat.models {
    public class Rooms {

        public int ID { set; get; }
        public string Name { set; get; }
        public int PeopleCount { set; get; }
        public ICollection<Message> Messages { set; get; }

    }
}
