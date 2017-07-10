
using System;

using System.Data;
using ProductManagement.Controllers.Core;
using ProductManagement.Models;
using ProductManagement.Controllers.DAL;

namespace ProductManagement.Controllers.BL
{
    public class ItemBL : IBL
    {
        public int DeleteData(IModel itemModel)
        {
            int returnCode = ItemDAO.DeleteData(itemModel as Item);
            return returnCode;
        }

        public int CountData(Item itemModel)
        {
            return ItemDAO.CountData(itemModel);
        }

        public int InsertData(IModel itemModel)
        {
            int returnCode = ItemDAO.InsertData(itemModel as Item);
            return returnCode;
        }

        public int SearchData(IModel itemModel, out DataTable dtResult)
        {
            int returnCode = ItemDAO.SearchData(itemModel as Item, out dtResult);
            return returnCode;
        }

        public int UpdateData(IModel itemModel)
        {
            int returnCode = ItemDAO.UpdateData(itemModel as Item);
            return returnCode;
        }

        public int ItemCodeIsExisted(IModel itemModel)
        {
            int returnCode = ItemDAO.ItemCodeIsExisted(itemModel as Item);
            return returnCode;
        }

        public int ItemIDIsExisted(IModel itemModel)
        {
            int returnCode = ItemDAO.ItemIDIsExisted(itemModel as Item);
            return returnCode;
        }

        public int SelectItemByID(IModel itemModel, out DataTable dtResult)
        {
            int returnCode = ItemDAO.SelectItemByID(itemModel as Item, out dtResult);
            return returnCode;
        }

        public int SelectItemByCode(IModel itemModel, out DataTable dtResult)
        {
            int returnCode = ItemDAO.SelectItemByCode(itemModel as Item, out dtResult);
            return returnCode;
        }
    }
}
