using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ffs.api
{
    public class LookupModule : NancyModule
    {
        public LookupModule() : base("/api/lookup")
        {
            Get["/units"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM Unit");
                    return Response.AsJson(result);
                }
            };

            Get["/methodologies"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM Methodology");
                    return Response.AsJson(result);
                }
            };

            Get["/assessmentLevel"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM AssessmentLevel");
                    return Response.AsJson(result);
                }
            };

            Get["/equipmenttypes"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM EquipmentType");
                    return Response.AsJson(result);
                }
            };

            Get["/componenttypes/{equipmentTypeId}"] = x =>
           {
               var equipmentTypeId = (int)x.equipmentTypeId;

               using (var db = DbContext.Get())
               {
                   IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT ComponentTypeID, ComponentTypeName  FROM  ComponentType WHERE EquipmentTypeID = @EquipmentTypeID", new { EquipmentTypeID = equipmentTypeId });
                   return Response.AsJson(result);
               }              
           };

            Get["/designcodes/{equipmentTypeId}"] = x =>
            {
                var equipmentTypeId = (int)x.equipmentTypeId;

                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT DesignCodeID, DesignCodeName FROM DesignCode WHERE EquipmentTypeID = @EquipmentTypeID", new { EquipmentTypeID = equipmentTypeId });
                    return Response.AsJson(result);
                }
            };
        }
    }
}
