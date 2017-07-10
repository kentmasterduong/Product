
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ProductManagement.Controllers.Core;
using ProductManagement.Models;
using ProductManagement.Controllers.DAL;

namespace ProductManagement.Controllers.BL
{
    public class MeasureBL : IBL
    {
        public int DeleteData(IModel measureModel)
        {
            int returnCode = MeasureDAO.DeleteData(measureModel as Measure);
            return returnCode;
        }

        public int InsertData(IModel measureModel)
        {
            int returnCode = MeasureDAO.InsertData(measureModel as Measure);
            return returnCode;
        }

        public int SearchData(IModel measureModel, out DataTable dtResult)
        {
            int returnCode = MeasureDAO.SearchData(measureModel as Measure, out dtResult);
            return returnCode;
        }

        public int UpdateData(IModel measureModel)
        {
            int returnCode = MeasureDAO.UpdateData(measureModel as Measure);
            return returnCode;
        }

        public int MeasureCodeIsExisted(IModel measureModel)
        {
            int returnCode = MeasureDAO.MeasureCodeIsExisted(measureModel as Measure);
            return returnCode;
        }

        public int MeasureIDIsExisted(IModel measureModel)
        {
            int returnCode = MeasureDAO.MeasureIDIsExisted(measureModel as Measure);
            return returnCode;
        }

        public int SelectMeasureByID(IModel measureModel, out DataTable dtResult)
        {
            int returnCode = MeasureDAO.SelectMeasureByID(measureModel as Measure, out dtResult);
            return returnCode;
        }

        public int SelectMeasureByCode(IModel measureModel, out DataTable dtResult)
        {
            int returnCode = MeasureDAO.SelectMeasureByCode(measureModel as Measure, out dtResult);
            return returnCode;
        }
        public int CountData(Measure measureModel)
        {
            return MeasureDAO.CountData(measureModel);
        }

        public List<MeasureDropdownlist> SelectDropdownData()
        {
            List<MeasureDropdownlist> list;
            MeasureDAO.SelectSimpleData(out list);
            return list;
        }
    }
}
