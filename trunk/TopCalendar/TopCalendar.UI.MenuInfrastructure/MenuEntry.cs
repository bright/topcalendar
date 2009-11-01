using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace TopCalendar.UI.MenuInfrastructure
{
    public class MenuEntry
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public ICommand Command { get; set; }
        public ICollection<MenuEntry> Items { get; set; }

        public MenuEntry()
        {
            Items = new List<MenuEntry>();
        }

        /// <summary>
        /// Konwertuje "biznesowy" obiekt MenuEntry na obiekt GUI MenuItem
        /// </summary>
        /// <returns>Obiekt MenuItem odpowiadaj¹cy 1:1 danemu MenuEntry</returns>
        public MenuItem AsMenuItem()
        {
            var obj = new MenuItem()
                       {
                           Name = Name,
                           Header = Header,
                           Command = Command,
                       };

            foreach (var item in Items)
            {
                obj.Items.Add(item.AsMenuItem());
            }

            return obj;
        }
    }
}
