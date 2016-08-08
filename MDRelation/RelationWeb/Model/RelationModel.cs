using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationWeb.Model
{
    public class RelationModel
    {
        public string aid { get; set; }

        public string eid { get; set; }

        public MDRelation.Dal.Entity.RelationType type { get; set; }

        public List<string> aids { get; set; }

    }
}
