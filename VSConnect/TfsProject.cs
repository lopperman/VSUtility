using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect
{

    public class TfsProject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TfsProject()
        {

        }

        public TfsProject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
