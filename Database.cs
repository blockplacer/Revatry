using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RevatryFramework
{
    //Extremely Simple Database runs on the ram
    public class Database
    {


        List<Table> Tables = new List<Table>();
        /// <summary>
        /// Sets the database
        /// </summary>
        public void AddTable(string name,int RowCount)
        {
            Tables.Add(new Table(name, RowCount));
        }

        public string Serialize()
        { return JsonConvert.SerializeObject(this); }
    }

    public class Table
    {
        public Row[] items;//object object
        public string name;

        public Table(string name,int RowCount)
        {  items = new Row[RowCount];  this.name = name; }//10

    }

    public class Row
    {
        public string name;
        public List<object> items = new List<object>();
        public int primary; //Key Item ID to check by doing that it could find id
        public void AddItem(object obj)
        {
            items.Add(obj);
        }

        public Row(string name)
        {
            this.name = name;
        }
    }


}
