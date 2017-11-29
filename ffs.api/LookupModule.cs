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
        private IDbContext db;

        public LookupModule(IDbContext db) : base("/api/lookup")
        {
            this.db = db;

            Get["/units"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM Unit");
                return Response.AsJson(result);

            };

            Get["/methodologies"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM Methodology");
                return Response.AsJson(result);

            };

            Get["/assessmentLevel"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM AssessmentLevel");
                return Response.AsJson(result);

            };

            Get["/equipmenttypes"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM EquipmentType");
                return Response.AsJson(result);

            };

            Get["/componenttypes/{equipmentTypeId}"] = x =>
           {
               var equipmentTypeId = (int)x.equipmentTypeId;



               IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT ComponentTypeID, ComponentTypeName  FROM  ComponentType WHERE EquipmentTypeID = @EquipmentTypeID", new { EquipmentTypeID = equipmentTypeId });
               return Response.AsJson(result);

           };

            Get["/designcodes/{equipmentTypeId}"] = x =>
            {
                var equipmentTypeId = (int)x.equipmentTypeId;



                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT DesignCodeID, DesignCodeName FROM DesignCode WHERE EquipmentTypeID = @EquipmentTypeID", new { EquipmentTypeID = equipmentTypeId });
                return Response.AsJson(result);

            };

            Get["/materialtypes"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM MaterialType");
                return Response.AsJson(result);

            };

            Get["/materials"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM Material ORDER BY MaterialID");
                return Response.AsJson(result);

            };

            Get["/asmeexemptioncurves"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT* FROM ASMEExemptionCurves");
                return Response.AsJson(result);

            };

            Get["/componentshapes"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM ComponentShape");
                return Response.AsJson(result);

            };

            Get["/reductions"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM [ReductionInTheMAT]");
                return Response.AsJson(result);

            };

            Get["/thicknessdatas"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM ThicknessData");
                return Response.AsJson(result);

            };

            Get["/standardpitcharts"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM TheStandardPitChart");
                return Response.AsJson(result);

            };

            Get["/fabricationTolerance/{equipmentTypeId}"] = x =>
            {
                var equipmentTypeId = (int)x.equipmentTypeId;


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT FabricationToleranceID,FabricationToleranceName FROM FabricationTolerance Where EquipmentTypeID = @EquipmentTypeID", new { EquipmentTypeID = equipmentTypeId });
                return Response.AsJson(result);

            };

            Get["/weldorientarion"] = x =>
            {


                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM WeldOrientarion");
                return Response.AsJson(result);

            };


            Get["/generic/{table}"] = x =>
            {
                string table = x.table;

                IEnumerable<dynamic> result = this.db.Connection.Query<dynamic>("SELECT * FROM " + table);
                return Response.AsJson(result);
            };

        }
    }
}
