using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MDRelation.Dal.Entity
{
    [BsonIgnoreExtraElements]
    public class Relation
    {


        /// <summary>
        /// 关联AccountID
        /// </summary>
        [BsonElement("aid")]
        public string AccountId { get; set; }


        /// <summary>
        /// 关联类型
        /// </summary>
        [BsonElement("t")]
        public RelationType Type { get; set; }


        /// <summary>
        /// 关联实体ID
        /// 如 任务ID, 群组ID 
        /// </summary>
        [BsonElement("eid")]
        public string EntityId { get; set; }

    }


    public enum RelationType
    {

        Project=1,

        Group=2,

        Task=3,

        Folder=4,




    }



}
