using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Samsonite.OMS.DTO;
using Samsonite.OMS.Database;
using Samsonite.Utility.Common;

namespace Samsonite.OMS.Service
{
    public class ProductService
    {
        #region 保存产品
        /// <summary>
        /// 转换成产品信息列表
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static List<ProductImportDto> ConvertToProducts(string excelPath)
        {
            List<ProductImportDto> objs = new List<ProductImportDto>();
            ExcelHelper helper = new ExcelHelper(excelPath);
            var table = helper.ExcelToDataTable("Sheet1");
            foreach (DataRow row in table.Rows)
            {
                string _sku = VariableHelper.SaferequestStr(row[7].ToString());
                if (!string.IsNullOrEmpty(_sku))
                {
                    var obj = new ProductImportDto
                    {
                        Name = VariableHelper.SaferequestStr(row[0].ToString()),
                        MatlGroup = VariableHelper.SaferequestStr(row[1].ToString()),
                        Description = VariableHelper.SaferequestStr(row[2].ToString()),
                        ProductName = VariableHelper.SaferequestStr(row[3].ToString()),
                        Material = VariableHelper.SaferequestStr(row[4].ToString()),
                        GdVal = VariableHelper.SaferequestStr(row[5].ToString()),
                        EAN = VariableHelper.SaferequestStr(row[6].ToString()),
                        SKU = VariableHelper.SaferequestStr(row[7].ToString()),
                        MarketPrice = VariableHelper.SaferequestDecimal(row[8].ToString()),
                        IsCommon = VariableHelper.SaferequestIntToBool(row[9].ToString()),
                        IsSet = VariableHelper.SaferequestIntToBool(row[10].ToString()),
                        IsGift = VariableHelper.SaferequestIntToBool(row[11].ToString()),
                        IsUsed = VariableHelper.SaferequestIntToBool(row[12].ToString()),
                        Result = true,
                        ResultMsg = string.Empty

                    };

                    objs.Add(obj);
                }
            }
            return objs;
        }

        /// <summary>
        /// 保存产品信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FuncResult SaveProducts(ProductImportDto item)
        {
            FuncResult _result = new FuncResult();
            using (var db = new ebEntities())
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    if (item.IsCommon)
                    {
                        //只有sku不为空,Material长度为5位,GdVal长度为4位
                        if (!string.IsNullOrEmpty(item.SKU) && item.Material.Length == 5 && item.GdVal.Length == 4)
                        {
                            string _ProductId = item.Material + item.GdVal;
                            sb.AppendLine($"if exists(select * from Product where SKU = '{item.SKU}')");
                            sb.AppendLine("begin");
                            sb.AppendLine($"UPDATE Product set Name='{item.Name}',MatlGroup='{item.MatlGroup}',Description='{item.Description}',ProductName='{item.ProductName}',Material='{item.Material}',GdVal='{item.GdVal}',ProductId='{_ProductId}',EAN='{item.EAN}',MarketPrice={item.MarketPrice},IsDelete={VariableHelper.SaferequestBoolToInt(!item.IsUsed)},IsCommon={VariableHelper.SaferequestBoolToInt(item.IsCommon)},IsGift={VariableHelper.SaferequestBoolToInt(item.IsGift)},IsSet={VariableHelper.SaferequestBoolToInt(item.IsSet)},EditDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'");
                            sb.AppendLine($" WHERE SKU = '{item.SKU}';");
                            sb.AppendLine("end");
                            sb.AppendLine("else");
                            sb.AppendLine("begin");
                            sb.AppendLine("insert into Product(GroupDesc,Name,MatlGroup,Description,ProductName,Material,GdVal,ProductId,EAN,SupplyPrice,MarketPrice,SalesPrice,IsDelete,IsCommon,IsGift,IsSet,SKU,AddDate,EditDate)");
                            sb.AppendLine($"values('','{item.Name}','{item.MatlGroup}','{item.Description}','{item.ProductName}','{item.Material}','{item.GdVal}','{_ProductId}','{item.EAN}',0,{item.MarketPrice},0,{VariableHelper.SaferequestBoolToInt(!item.IsUsed)},{VariableHelper.SaferequestBoolToInt(item.IsCommon)},{VariableHelper.SaferequestBoolToInt(item.IsGift)},{VariableHelper.SaferequestBoolToInt(item.IsSet)},'{item.SKU}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                            sb.AppendLine("end");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(item.SKU)) throw new Exception($"The SKU can not be empty!");
                            if (item.Material.Length != 5) throw new Exception($"The length of Material must be 5!");
                            if (item.GdVal.Length != 4) throw new Exception($"The length of GdVal must be 4!");
                        }
                    }
                    else
                    {
                        //只有sku不为空
                        if (!string.IsNullOrEmpty(item.SKU))
                        {
                            sb.AppendLine($"if exists(select * from Product where SKU = '{item.SKU}')");
                            sb.AppendLine("begin");
                            sb.AppendLine($"UPDATE Product set Name='{item.Name}',MatlGroup='{item.MatlGroup}',Description='{item.Description}',ProductName='{item.ProductName}',Material='{item.Material}',GdVal='{item.GdVal}',EAN='{item.EAN}',MarketPrice={item.MarketPrice},IsDelete={VariableHelper.SaferequestBoolToInt(!item.IsUsed)},IsCommon={VariableHelper.SaferequestBoolToInt(item.IsCommon)},IsGift={VariableHelper.SaferequestBoolToInt(item.IsGift)},IsSet={VariableHelper.SaferequestBoolToInt(item.IsSet)},EditDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'");
                            sb.AppendLine($" WHERE SKU = '{item.SKU}';");
                            sb.AppendLine("end");
                            sb.AppendLine("else");
                            sb.AppendLine("begin");
                            sb.AppendLine("insert into Product(GroupDesc,Name,MatlGroup,Description,ProductName,Material,GdVal,EAN,SupplyPrice,MarketPrice,SalesPrice,IsDelete,IsCommon,IsGift,IsSet,SKU,AddDate,EditDate)");
                            sb.AppendLine($"values('','{item.Name}','{item.MatlGroup}','{item.Description}','{item.ProductName}','{item.Material}','{item.GdVal}','{item.EAN}',0,{item.MarketPrice},0,{VariableHelper.SaferequestBoolToInt(!item.IsUsed)},{VariableHelper.SaferequestBoolToInt(item.IsCommon)},{VariableHelper.SaferequestBoolToInt(item.IsGift)},{VariableHelper.SaferequestBoolToInt(item.IsSet)},'{item.SKU}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                            sb.AppendLine("end");
                        }
                        else
                        {
                            throw new Exception($"The SKU can not be empty!");
                        }
                    }

                    if (db.Database.ExecuteSqlCommand(sb.ToString()) > 0)
                    {
                        _result.Result = true;
                        _result.Message = string.Empty;
                    }
                    else
                    {
                        throw new Exception($"SKU:{item.SKU} save fail!");
                    }
                }
                catch (Exception ex)
                {
                    _result.Result = false;
                    _result.Message = ex.Message;
                }
            }
            return _result;
        }
        #endregion

        #region 保存产品库存
        /// <summary>
        /// 转换成产品库存信息列表
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static List<ProductInventoryImportDto> ConvertToProductInventorys(string excelPath)
        {
            List<ProductInventoryImportDto> objs = new List<ProductInventoryImportDto>();
            ExcelHelper helper = new ExcelHelper(excelPath);
            var table = helper.ExcelToDataTable("Sheet1");
            foreach (DataRow row in table.Rows)
            {
                string _sku = VariableHelper.SaferequestStr(row[2].ToString());
                if (!string.IsNullOrEmpty(_sku))
                {
                    var obj = new ProductInventoryImportDto
                    {
                        MallSapCode = VariableHelper.SaferequestStr(row[0].ToString()),
                        MallProductName = VariableHelper.SaferequestStr(row[1].ToString()),
                        ProductType = VariableHelper.SaferequestStr(row[2].ToString()),
                        SKU = VariableHelper.SaferequestStr(row[3].ToString()),
                        OuterProduct = VariableHelper.SaferequestStr(row[4].ToString()),
                        OuterSku = VariableHelper.SaferequestStr(row[5].ToString()),
                        SalesPrice = VariableHelper.SaferequestDecimal(row[6].ToString()),
                        IsOnSale = VariableHelper.SaferequestIntToBool(row[7].ToString()),
                        IsUsed = VariableHelper.SaferequestIntToBool(row[8].ToString()),
                        Result = true,
                        ResultMsg = string.Empty
                    };
                    objs.Add(obj);
                }
            }
            return objs;
        }

        /// <summary>
        /// 保存产品库存信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static FuncResult SaveProductInventorys(ProductInventoryImportDto item)
        {
            FuncResult _result = new FuncResult();
            using (var db = new ebEntities())
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    int _productType = VariableHelper.SaferequestInt(item.ProductType);

                    //店铺编码和sku不能为空
                    if (!string.IsNullOrEmpty(item.MallSapCode) && _productType > 0 && _productType < 3 && !string.IsNullOrEmpty(item.SKU) && !string.IsNullOrEmpty(item.OuterProduct))
                    {
                        sb.AppendLine($"if exists(select * from MallProduct where MallSapCode='{item.MallSapCode}' and MallProductId='{item.OuterProduct}' and MallSkuId = '{item.OuterSku}')");
                        sb.AppendLine("begin");
                        sb.AppendLine($"Update MallProduct set MallProductTitle='{item.MallProductName}',SKU='{item.SKU}',SalesPrice='{item.SalesPrice}',IsOnSale={VariableHelper.SaferequestBoolToInt(item.IsOnSale)},IsUsed={VariableHelper.SaferequestBoolToInt(item.IsUsed)},EditDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'");
                        sb.AppendLine($" WHERE (MallSapCode='{item.MallSapCode}' and MallProductId='{item.OuterProduct}' and MallSkuId = '{item.OuterSku}');");
                        sb.AppendLine("end");
                        sb.AppendLine("else");
                        sb.AppendLine("begin");
                        sb.AppendLine($"Insert Into MallProduct (MallSapCode,MallProductTitle,MallProductPic,MallProductId,MallSkuId,MallSkuPropertiesName,ProductType,SKU,SalesPrice,Quantity,IsOnSale,IsUsed,EditDate) values('{item.MallSapCode}','{item.MallProductName}','','{item.OuterProduct}','{item.OuterSku}','',{_productType},'{item.SKU}',{item.SalesPrice},0,{VariableHelper.SaferequestBoolToInt(item.IsOnSale)},{VariableHelper.SaferequestBoolToInt(item.IsUsed)},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')");
                        sb.AppendLine("end");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(item.MallSapCode)) throw new Exception($"The MallSapCode can not be empty!");
                        if (_productType < 0 || _productType > 3) throw new Exception($"The type of product is incorrect!");
                        if (string.IsNullOrEmpty(item.SKU)) throw new Exception($"The SKU can not be empty!");
                        if (string.IsNullOrEmpty(item.OuterProduct)) throw new Exception($"The Outer Product ID can not be empty!");
                    }

                    if (db.Database.ExecuteSqlCommand(sb.ToString()) > 0)
                    {
                        _result.Result = true;
                        _result.Message = string.Empty;
                    }
                    else
                    {
                        throw new Exception($"SKU:{item.SKU} save fail!");
                    }
                }
                catch (Exception ex)
                {
                    _result.Result = false;
                    _result.Message = ex.Message;
                }
            }
            return _result;
        }
        #endregion

        /// <summary>
        /// 根据各国家的SKU标准,格式化SKU
        /// </summary>
        /// <param name="objSku"></param>
        /// <param name="objProductIDConfig">配置中的ProductIDConfig</param>
        /// <returns></returns>
        public static string FormatSku(string objSku, int objProductIDConfig)
        {
            string _result = string.Empty;
            objSku = objSku.Replace("'", "");
            //不带*Sku
            if (objProductIDConfig == 1)
            {
                _result = objSku;
            }
            //带*Sku
            else if (objProductIDConfig == 2)
            {
                if (objSku.IndexOf("*") == -1)
                {
                    if (objSku.Length == 8)
                    {
                        _result = $"{objSku.Substring(0, 3)}*{objSku.Substring(3, 5)}";
                    }
                    else if (objSku.Length == 9)
                    {
                        _result = $"{objSku.Substring(0, 3)}*{objSku.Substring(4, 5)}";
                    }
                    else
                    {
                        _result = objSku;
                    }
                }
                else
                {
                    _result = objSku;
                }
            }
            //Material-Grid
            else if (objProductIDConfig == 3)
            {
                _result = objSku;
            }
            else
            {
                _result = objSku;
            }

            return _result;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="objMallSapCode"></param>
        /// <param name="objProductType"></param>
        /// <param name="objSku"></param>
        /// <param name="objQuantity"></param>
        public static void UpdateProductInventory(string objMallSapCode, ProductType objProductType, string objSku, int objQuantity)
        {
            using (var db = new ebEntities())
            {
                if (!string.IsNullOrEmpty(objSku))
                {
                    db.Database.ExecuteSqlCommand($"update MallProduct set Quantity=Quantity-{objQuantity} where MallSapCode='{objMallSapCode}' and ProductType={(int)objProductType} and Sku='{objSku}'");
                }
            }
        }

        /// <summary>
        /// 根据账号类型,显示不同库存数
        /// </summary>
        /// <param name="objUserType"></param>
        /// <param name="objInventory"></param>
        /// <returns></returns>
        public static int ShowInventory(int objUserType, int objInventory)
        {
            int _result = 0;
            if (objUserType == (int)UserType.InternalStaff)
            {
                //内部人员显示真实库存
                _result = objInventory;
            }
            else
            {
                //总仓数量显示打50%
                _result = objInventory / 2;
            }
            return _result;
        }

        /// <summary>
        /// 打折库存数量到电商平台
        /// </summary>
        /// <param name="objMallSapCode"></param>
        /// <param name="objInventory"></param>
        /// <returns></returns>
        public static int DiscountInventory(string objMallSapCode, int objInventory)
        {
            int _result = 0;
            /***推送规则如下:***/
            /***规则1:
            大于5件:8折显示
            2-5件:减1件
            1件不变***/
            /***规则2(天猫新秀丽背包旗舰店&天猫Lipault官方旗舰店):
            大于10件:减5
            6-10件:减2件
            5件以下不变***/
            if (objMallSapCode == "1162480" || objMallSapCode== "1168204")
            {
                if (objInventory > 10)
                {
                    _result = objInventory - 5;
                }
                else if (objInventory <= 10 && objInventory >= 6)
                {
                    _result = objInventory - 2;
                }
                else
                {
                    _result = objInventory;
                }
            }
            else
            {
                if (objInventory > 5)
                {
                    _result = (int)Math.Floor(objInventory * 0.8);
                }
                else if (objInventory <= 5 && objInventory >= 2)
                {
                    _result = objInventory - 1;
                }
                else
                {
                    _result = objInventory;
                }
            }
            return _result;
        }
    }
}
