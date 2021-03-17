using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FTSS.DP.DapperORM
{
    public class Common
    {
        public static DynamicParameters GetSearchParams()
        {
            var p = new DynamicParameters();
            int errorCode = 0;
            string errorMessage = "";
            p.Add("@ErrorCode", errorCode, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            p.Add("@ErrorMessage", errorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetSearchParams(string token)
        {
            if(string.IsNullOrEmpty(token))
                throw new ArgumentNullException("امضای کاربری یافت نشد");
            var p = GetSearchParams();
            p.Add("@Token", token);

            return p;
        }

        public static DynamicParameters GetSearchParams(Models.Database.BaseSearchParams filterParams)
        {
            if (filterParams == null)
                throw new ArgumentNullException("خطا در نحوه ارسال درخواست رخ داده است");
            if(filterParams.StartIndex<0 || (filterParams.StartIndex == 0 && filterParams.PageSize <= 0))
			{
                throw new ArgumentException("خطا در نحوه ارسال درخواست رخ داده است");
			}
            var p = GetSearchParams(filterParams.Token);
            
            //Pagination
            p.Add("@StartIndex", filterParams.StartIndex, System.Data.DbType.Int32);
            p.Add("@PageSize", filterParams.PageSize, System.Data.DbType.Int32);
            p.Add("@Sort", filterParams.Sort, System.Data.DbType.String);

            int actualSize = 0;
            p.Add("@ActualSize", actualSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetDataParams(Models.Database.BaseModel data)
        {
            if (data == null)
                throw new ArgumentNullException("خطا در نحوه ارسال درخواست رخ داده است!");
            var p = GetSearchParams(data.Token);
            return p;
        }



        /// <summary>
        /// Convert database query result to DBResult type
        /// </summary>
        /// <param name="p"></param>
        /// <param name="data"></param>
        /// <param name="actualSize"></param>
        /// <returns></returns>
        public static Models.Database.DBResult GetResult(DynamicParameters p, object data)
        {
            if (p == null)
                throw new ArgumentNullException("خطا در نحوه ارسال درخواست رخ داده است");
            var rst = new Models.Database.DBResult()
            {
                ErrorCode = p.Get<int>("ErrorCode"),
                ErrorMessage = p.Get<string>("ErrorMessage"),

                Data = data
            };

            try
            {
                //if database query for searching, try to catch ActualSize output param for pagination
                rst.ActualSize = p.Get<int>("ActualSize");
            }
            catch (Exception)
            {
                rst.ActualSize = data == null ? 0 : 1;
            }

            return rst;
        }

        public static DynamicParameters GenerateParams(object data, List<string> exceptFields = null)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (data != null)
            {
                foreach (PropertyInfo propertyInfo in data.GetType().GetProperties().ToList())
                {
                    bool finded = false;
                    if (exceptFields != null && exceptFields.Count > 0)
                    {
                        foreach (var item in exceptFields)
                        {
                            if (item == propertyInfo.Name)
                            {
                                finded = true;
                                break;
                            }
                        }
                        if (finded == false)
                            parameters.Add("@" + propertyInfo.Name, IsDateTime(propertyInfo.GetValue(data)) ? propertyInfo.GetValue(data) : SafeFarsiStr(propertyInfo.GetValue(data)) ?? null);
                    }
                    else
                    {
                        parameters.Add("@" + propertyInfo.Name, IsDateTime(propertyInfo.GetValue(data)) ? propertyInfo.GetValue(data) : SafeFarsiStr(propertyInfo.GetValue(data)) ?? null);
                    }
                }
            }

            return parameters;
        }

        private static object SafeFarsiStr(object input)
        {
            if (input == null)
                return null;
            var text = input.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                return (object)text.Replace("ﮎ", "ک").Replace("ﮏ", "ک").Replace("ﮐ", "ک").Replace("ﮑ", "ک").Replace("ك", "ک").Replace("ي", "ی").Replace("ی", "ی").Replace("اِ", "ا").Replace("اُ", "ا").Replace("اَ", "ا").Replace("اً", "ا").Replace("اٌ", "ا").Replace("اٍ", "ا").Replace("اّ", "ا");
            }
            return null;
        }
        private static bool IsDateTime(object txtDate)
        {
            DateTime tempDate;
            string date = null;
            if (txtDate != null)
                date = txtDate.ToString();
            return DateTime.TryParse(date, out tempDate);
        }
    }
}
