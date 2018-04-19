using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSConnect
{
    public class TfsArea
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TfsArea()
        {

        }

        public TfsArea(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
    public class TfsTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TfsTeam()
        {

        }

        public TfsTeam(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
