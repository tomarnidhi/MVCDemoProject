using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;

namespace ManagerLayer
{
    public class ItemML
    {
        public ItemML()
        {

        }
        public List<ItemBL> ListAllItem()
        {
            ItemDL DL = new ItemDL();
             return DL.ListAll();
        }

        public void InsertItem(string Name, string Desc, double Price)
        {
            ItemDL DL = new ItemDL();
            DL.InsertItem(Name, Desc, Price);
        }

        public void UpdateItem(string Name, string Desc, double Price,Guid Id)
        {
            ItemDL DL = new ItemDL();
            DL.UpdateItem(Name, Desc, Price,Id);
        }

        public void DeleteItem(Guid Id)
        {
            ItemDL DL = new ItemDL();
            DL.DeleteItem(Id);
        }
    }
}
