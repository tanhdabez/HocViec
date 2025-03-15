using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class CommonHelper
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;
            if (now < dateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }

        public static bool IsEligibleForWork(int age, string gender)
        {
            if (gender == "1")
            {
                return (age >= 15 && age <= 61);
            }
            else if (gender == "2")//nữ
            {
                return (age >= 15 && (age < 56 || (age == 56 && DateTime.Today.Month >= 5)));
            }
            else
            {
                // Unknown gender
                return false;
            }
        }

        public static void MapProperties<T>(T source, T destination)
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.CanWrite) // Chỉ cập nhật nếu có setter
                {
                    var value = prop.GetValue(source);
                    prop.SetValue(destination, value);
                }
            }
        }
        

        public static (T model, List<string> errors) MapAndValidate<T>(object request) where T : new()
        {
            if (request == null)
            {
                return (default, new List<string> { "Dữ liệu đầu vào không hợp lệ." });
            }

            T model = new T(); // Tạo object mới từ kiểu T
            PropertyInfo[] modelProperties = typeof(T).GetProperties();
            PropertyInfo[] requestProperties = request.GetType().GetProperties();

            List<string> errors = new List<string>();

            foreach (var prop in modelProperties)
            {
                // Nếu là thuộc tính "Id" thì bỏ qua
                if (prop.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var sourceProp = requestProperties.FirstOrDefault(p => p.Name == prop.Name);
                if (sourceProp != null && prop.CanWrite)
                {
                    var value = sourceProp.GetValue(request);
                    prop.SetValue(model, value);
                }

                // Kiểm tra Required
                var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttr != null)
                {
                    var value = prop.GetValue(model);
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        errors.Add(requiredAttr.ErrorMessage ?? $"{prop.Name} không được để trống.");
                    }
                }
            }

            return errors.Any() ? (default, errors) : (model, null);
        }
    }
}
