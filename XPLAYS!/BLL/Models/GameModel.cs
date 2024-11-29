using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GameModel
    {
        public Game Record { get; set; }
        public string Name => Record.Name;

        ////////////
        public List<Publisher> publishers;
        
        
    }
}
