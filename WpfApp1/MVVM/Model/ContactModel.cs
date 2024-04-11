using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MVVM.Model
{
    internal class ContactModel
    {
        public string Username { get; set; }
        public string Imagesource { get; set; }
        public ObservableCollection<MessageModel> Messages {  get; set; }
        public string LastMessage => Messages.Last().Message;
    }
}
