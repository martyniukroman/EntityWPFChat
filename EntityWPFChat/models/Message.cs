using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityWPFChat.models {
    public class Message {
        public int ID { set; get; }
        public string MessageContent { set; get; }
        public string PictureLink { set; get; }
        public DateTime dateTime { set; get; }
        public string Color { set; get; }

        public Person Sender { set; get; }
        public Person Receiver { set; get; }

    }
}
