
using Core.Common;
using PagedList;
using ProductManagement.Controllers.BL;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class ItemController : Controller
    {
        [HttpGet]
        public ActionResult Index(Item itemModel, int page = 1)
        {
            if (itemModel == null)
            {
                itemModel = new Item();
                itemModel.lstItems = new StaticPagedList<Item>(new List<Item>(), 1, 20, 0);
            }
            else
            {
                if (itemModel.code == null && itemModel.name == null && itemModel.category_id == null)
                {
                    if ((Session["Code"] != null || Session["Name"] != null || Session["Category"] != null) && page > 1)
                    {
                        itemModel.code = Session["Code"] == null? "" : Session["Code"].ToString();
                        itemModel.name = Session["Name"] == null? "" : Session["Name"].ToString();
                        itemModel.category_id = (int?)Session["Category"];
                    }
                    else
                    {
                        itemModel = new Item();
                    }
                }
                else
                {
                    Session["Code"] = itemModel.code;
                    Session["Name"] = itemModel.name;
                    Session["Category"] = itemModel.category_id;
                }
            }


            itemModel.page = page;
            itemModel.page_count = new ItemBL().CountData(itemModel);
            TempData["SearchCount"] = itemModel.page_count + " row(s) has found.";
            DataTable dtResult = new DataTable();
            new ItemBL().SearchData(itemModel, out dtResult);

            itemModel.lstItems = new StaticPagedList<Item>(CommonMethod.DataTableToDto<Item>(dtResult), itemModel.page, 20, itemModel.page_count);
            SelectList listCategory = new SelectList(new CategoryBL().SelectDropdownData(), "id", "name");
            ViewBag.ListCategory = listCategory;

            return View(itemModel);
        }



        [HttpGet]
        public ActionResult Add()
        {
            return View("Add", LoadItemAddForm(new Item()));
        }

        [HttpPost]
        public ActionResult Add(Item itemModel)
        {
            if (ModelState.IsValid)
            {
                //Kiem tra duplicate code
                DataTable dtResult = new DataTable();
                new ItemBL().SelectItemByCode(itemModel, out dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    TempData["Duplicate"] = "Duplicate Code";

                }
                else
                {
                    new ItemBL().InsertData(itemModel);

                    TempData["Successful"] = "Added Item";
                    return RedirectToAction("Add");

                }

            }

            return View("Add", LoadItemAddForm(itemModel));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Item itemModel = new Item();

            if (id == null)
            {
                TempData["Danger"] = "Data not found";
                return RedirectToAction("Index");
            }
            else
            {

                itemModel.id = id;
                DataTable dtResult = new DataTable();
                new ItemBL().SelectItemByID(itemModel, out dtResult);
                if (dtResult.Rows.Count > 0)
                {
                    foreach (DataRow row in dtResult.Rows)
                    {
                        itemModel.code = row["code"].ToString();
                        itemModel.name = row["name"] == null? "" : row["name"].ToString();
                        itemModel.description = row["description"] == null? "":row["description"].ToString();
                        itemModel.category_id = CommonMethod.ParseInt32(row["category_id"].ToString());
                        itemModel.dangerous = row["dangerous"]==null? false:(bool)row["dangerous"];
                        itemModel.discontinued_datetime = (DateTime)row["discontinued_datetime"];
                        itemModel.inventory_expired = (int?)row["inventory_expired"];
                        itemModel.inventory_list_price = (decimal?)row["inventory_list_price"];
                        itemModel.inventory_measure_id = (int?)row["inventory_measure_id"];
                        itemModel.inventory_standard_cost = (decimal?)row["inventory_standard_cost"];
                        itemModel.manufacture_class = row["manufacture_class"].ToString()== null? "":row["manufacture_class"].ToString();
                        itemModel.manufacture_color = row["manufacture_color"]== null? "":row["manufacture_color"].ToString();
                        itemModel.manufacture_day = (decimal?)row["manufacture_day"];
                        itemModel.manufacture_finished_goods = row["manufacture_finished_goods"] == null? false:(bool)row["manufacture_finished_goods"];
                        itemModel.manufacture_make = row["manufacture_make"] == null? false:(bool)row["manufacture_make"];
                        itemModel.manufacture_size = row["manufacture_size"] == null ? "" : (string)row["manufacture_size"];
                        itemModel.manufacture_size_measure_id = (int?)row["manufacture_size_measure_id"];
                        itemModel.manufacture_style = row["manufacture_style"]== null? "":(string)row["manufacture_style"];
                        itemModel.manufacture_tool = row["manufacture_tool"] == null? false: (bool)row["manufacture_tool"];
                        itemModel.manufacture_weight = row["manufacture_weight"] == null? "":(string)row["manufacture_weight"];
                        itemModel.manufacture_weight_measure_id = (int?)row["manufacture_weight_measure_id"];
                        itemModel.specification = row["specification"]== null? "" :(string)row["specification"];
                    }


                }
                else
                {
                    TempData["Danger"] = "Data not found";
                    return RedirectToAction("Index");

                }
            }

            return View(LoadItemAddForm(itemModel));
        }


        [HttpPost]
        public ActionResult Edit(Item itemModel)
        {
            if (ModelState.IsValid)
            {
                new MeasureBL().UpdateData(itemModel);

                TempData["Success"] = "Edited successfuly";
                return RedirectToAction("Index");
            }

            return View("Index", LoadItemAddForm(itemModel));
        }

        private Item LoadItemAddForm(Item item)
        {
            if (item == null)
            {
                item = new Item();
            }
            SelectList listCategory = new SelectList(new CategoryBL().SelectDropdownData(), "id", "name");
            ViewBag.ListCategory = listCategory;


            MeasureBL measureBL = new MeasureBL();
            SelectList listMeasure = new SelectList(new MeasureBL().SelectDropdownData(), "id", "name");
            ViewBag.listMeasure = listMeasure;


            return item;
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Item itemModel = new Item();
            itemModel.id = id;
            if (new ItemBL().ItemIDIsExisted(itemModel) == 1)
            {
                new ItemBL().DeleteData(itemModel);
                TempData["Success"] = "Deleted successfuly";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Deleted successfuly";
            return RedirectToAction("Index");
        }
    }
}