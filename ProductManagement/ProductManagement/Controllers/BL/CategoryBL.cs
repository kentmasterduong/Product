
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ProductManagement.Controllers.Core;
using ProductManagement.Controllers.DAL;
using ProductManagement.Models;

namespace ProductManagement.Controllers.BL
{
    public class CategoryBL : IBL
    {
        public int DeleteData(IModel categoryModel)
        {
            int returnCode = CategoryDAO.DeleteData(categoryModel as Category);
            return returnCode;
        }

        public int InsertData(IModel categoryModel)
        {
            int returnCode = CategoryDAO.InsertData(categoryModel as Category);
            return returnCode;
        }

        public int SearchData(string searchCondition, out DataTable dtResult)
        {
            throw new NotImplementedException();
        }

        public int SearchData(IModel categoryModel, out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SearchData(categoryModel as Category, out dtResult);
            return returnCode;
        }

        public int UpdateData(IModel categoryModel)
        {
            int returnCode = CategoryDAO.UpdateData(categoryModel as Category);
            return returnCode;
        }

        public int CategoryIsExisted(IModel categoryModel)
        {
            int returnCode = CategoryDAO.CategoryIsExisted(categoryModel as Category);
            return returnCode;
        }

        public int CategoryIDIsExisted(IModel categoryModel)
        {
            int returnCode = CategoryDAO.CategoryIDIsExisted(categoryModel as Category);
            return returnCode;
        }

        public int SelectCategoryByID(IModel categoryModel, out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SelectCategoryByID(categoryModel as Category, out dtResult);
            return returnCode;
        }

        public int SelectCategoryByCode(IModel categoryModel, out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SelectCategoryByCode(categoryModel as Category, out dtResult);
            return returnCode;
        }

        public int CountData(Category categoryModel)
        {
            return CategoryDAO.CountData(categoryModel);
        }

        public List<CategoryDropdownlist> SelectDropdownData()
        {
            List<CategoryDropdownlist> list;
            CategoryDAO.SelectSimpleData(out list);
            return list;
        }
    }
}
