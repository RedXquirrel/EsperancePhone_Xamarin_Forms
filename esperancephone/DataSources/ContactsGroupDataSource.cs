using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using esperancephone.Interfaces;
using esperancephone.Ioc;
using esperancephone.Pages;
using esperancephone.ViewModels;

namespace esperancephone.DataSources
{
    public class ContactsGroupDataSource : List<ContactListItemViewModel>
    {
        public string Title { get; set; }
        public string ShortName { get; set; } // used for jump lists, must be 1 character
        public string Subtitle { get; set; }
        private ContactsGroupDataSource(string title, string shortName, string subtitle)
        {
            Title = title;
            ShortName = shortName;
            Subtitle = subtitle;
        }

        public static IList<ContactsGroupDataSource> Groups { private set; get; }

        static ContactsGroupDataSource()
        {
            List<ContactsGroupDataSource> Groups = new List<ContactsGroupDataSource>
            {
                new ContactsGroupDataSource("A", "A", string.Empty)
                {
                    new ContactListItemViewModel
                    {
                        IsPersonant = false,
                        IconKey = "\uf007",
                        DisplayName = "My Display Name",
                        FirstName = "My First Name",
                        LastName = "My Last Name"
                    },

                },
                new ContactsGroupDataSource("B", "B", string.Empty)
                {
                    new ContactListItemViewModel
                    {
                        IsPersonant = false,
                        IconKey = "\uf007",
                        DisplayName = "My Display Name",
                        FirstName = "My First Name",
                        LastName = "My Last Name"
                    },
                },
            };

            ContactsGroupDataSource.Groups = Groups;
        }

    }
}