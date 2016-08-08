using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using MD.Configuration;
using MDRelation.Dal;
using MDRelation.Dal.Entity;
using RelationWeb.Model;

namespace RelationWeb.Controllers
{
    [Route("api/[controller]")]
    public class RelationController : Controller
    {
        private readonly IOptions<AppSettings> settings;

        private readonly RelationProvider provider;

        public RelationController(IOptions<AppSettings> settings)
        {
            this.settings = settings;
            provider = new RelationProvider(settings.Value.MongoUri);
        }

        [HttpPost("set")]
        public bool Set([FromBody]RelationModel model)
        {
            var relation = new Relation()
            {
                AccountId = model.aid,
                EntityId = model.eid,
                Type = model.type
            };
            var r = provider.AddRelation(relation);

           return r!=null;
        }

        [HttpPost("setmulti")]
        public bool SetMulti([FromBody] RelationModel model)
        {
            foreach (string item in model.aids)
            {
                var relation = new Relation()
                {
                    AccountId = item,
                    EntityId = model.eid,
                    Type =model.type
                };
                provider.AddRelation(relation);
            }

            return true;
        }


        [HttpPost("del")]
        public bool Delete([FromBody] RelationModel model)
        {
            var accountId = model.aid;
            var entityId = model.eid;

            var hasAid = !string.IsNullOrEmpty(accountId);
            var hasEId = !string.IsNullOrEmpty(entityId);

            if (hasAid && hasEId)
            {
                return provider.DeleteRelationByAccountAndEntityId(accountId, entityId);
            }
            else if (hasAid)
            {
                return provider.DeleteRelationByAccountId(accountId);
            }
            else if (hasEId)
            {
                return provider.DeleteRelationByEntityId(entityId);
            }
            return false;
        }






    }
}
