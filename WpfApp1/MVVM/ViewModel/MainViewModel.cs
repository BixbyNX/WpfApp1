using System;
using System.Collections.ObjectModel;
using WpfApp1.Core;
using WpfApp1.MVVM.Model;

namespace WpfApp1.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjects
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /* commands */
        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }

        private string _mesage;
        private object profileImage;

        public String Message
        {
            get { return _mesage; }
            set
            {
                _mesage = value;
                OnPropertyChanged();
            }

        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";

            });

            Messages.Add(new MessageModel
            {
                Username = "Ken",
                UsernameColor = "black",
                ImageSource = "https://i.imgur.com/i2szTsp.png",
                Message = "Test",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true,

            });



            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Gian",
                    UsernameColor = "#409aff",
                    ImageSource = "https://i.imgur.com/i2szTsp.png",
                    Message = "Test",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false,
                });


            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Bunny",
                    UsernameColor = "Green",
                    ImageSource = "https://i.imgur.com/i2szTsp.png",
                    Message = "Test",
                    Time = DateTime.Now,
                    IsNativeOrigin = true,

                });
            }


            Messages.Add(new MessageModel
            {
                Username = "KIKO",
                UsernameColor = "Green",
                ImageSource = "https://i.imgur.com/i2szTsp.png",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = true,

            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Ken {i}",
                    Imagesource = "https://i.imgur.com/i2szTsp.png",
                    Messages = Messages
                });
            }

        }
    }

}

