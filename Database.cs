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
        /// <summary>
        /// Loads json info to database
        /// </summary>
        /// <param name="data">The JSON</param>
        /// <returns>Database objects</returns>
        public static Database LoadFromJSON(string data)
        { return JsonConvert.DeserializeObject<Database>(data); }
        /// <summary>
        /// Adds item to database
        /// </summary>
        /// <param name="table">Table</param>
        /// <param name="Row">Row</param>
        /// <param name="obj">Item</param>
        public void AddItem(string table,string Row,object obj)
        {
            Tables.Find(x => x.name == table).items.ToList().Find(x => x.name == Row).items.Add(obj);

        }

        public void AddRow(string table,Row row)
        {
            Tables.Find(x => x.name == table).items.ToList().Add(row);
        }
        /// <summary>
        /// Retrieves item easly from a row
        /// </summary>
        /// <param name="table">Table contains row</param>
        /// <param name="idRow">Identifier row,Contains key to find data</param>
        /// <param name="identifier">To Search (must be int currently in future this going to be expanded to strings)</param>
        public object GetItem(string table, int identifier)
        {
            var found = Tables.Find(x => x.name == table);
            int id = found.items[found.primary].items.FindIndex(x => (int)x == identifier);
             return found.items[id].items[id];//.items.ToList().Find(x => x.name == Row).items
        }
    }

    public class Table
    {
        public Row[] items; //Items that table stores
        public string name; //Identifier of table
        public int primary = 0; //Key Item ID to check by doing that it could find id
        //Constructor:
        public Table(string name,int RowCount)
        {  items = new Row[RowCount];  this.name = name; }

    }

    public class Row
    {
        public string name;
        //Items
        public List<object> items = new List<object>();
        
        //Adds item to items
        public void AddItem(object obj)
        {
            items.Add(obj);
        }
        //Constructor 1:
        public Row()
        { }
        //Constructor 2:
        public Row(string name)
        {
            this.name = name;
        }
    }

}
