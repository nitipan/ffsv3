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

            Get["/materialtypes"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM MaterialType");
                    return Response.AsJson(result);
                }
            };

            Get["/materials"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM Material ORDER BY MaterialID");
                    return Response.AsJson(result);
                }
            };

            Get["/asmeexemptioncurves"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT* FROM ASMEExemptionCurves");
                    return Response.AsJson(result);
                }
            };

            Get["/componentshapes"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM ComponentShape");
                    return Response.AsJson(result);
                }
            };

            Get["/reductions"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM [ReductionInTheMAT]");
                    return Response.AsJson(result);
                }
            };

            Get["/thicknessdatas"] = x =>
            {
                using (var db = DbContext.Get())
                {
                    IEnumerable<dynamic> result = db.Connection.Query<dynamic>("SELECT * FROM ThicknessData");
                    return Response.AsJson(result);
                }
            };

        }
    }
}
