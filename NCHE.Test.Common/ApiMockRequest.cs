
using CommonLibraries.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NCHE.Test.Common
{
    public class ApiMockRequest : IApiRequestService
    {
        private bool IsList(Type type)
        {
            bool IsGenericCollectionType(Type type)
            {
                return type.IsGenericType && (typeof(ICollection<>) == type.GetGenericTypeDefinition());
            }
            bool IsGenericEnumerableType(Type type)
            {
                return type.IsGenericType && (typeof(IEnumerable<>) == type.GetGenericTypeDefinition());
            }
            bool IsListCollectionType(Type type)
            {
                return type.IsGenericType && (typeof(IList<>) == type.GetGenericTypeDefinition());
            }
            return Array.Exists(type.GetInterfaces(), IsGenericCollectionType)
                || Array.Exists(type.GetInterfaces(), IsGenericEnumerableType)
                || Array.Exists(type.GetInterfaces(), IsListCollectionType);
        }
        public T Response<T>() where T:class
        {
            var res = (T)Activator.CreateInstance(typeof(T));
            Type type = res.GetType();
            var isList = IsList(type);
            if (isList)
            {
                Type itemType = type.GetGenericArguments()[0];
                var list = CreateListFromType(itemType, 5);
                res = (T)list;
            }
            else
            {
                var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                foreach (var f in fields)
                {
                    var _type = f.FieldType;
                    var _isList = IsList(_type);
                    if(_type == typeof(string))
                    {
                        f.SetValue(res, "");
                    }
                    else
                    {
                        var obj = Activator.CreateInstance(_type);
                        f.SetValue(res, obj);

                    }
                    if (_isList && _type != typeof(string))
                    {
                        var args = _type.GetGenericArguments();
                        Type itemType = args[0];
                        var list = CreateListFromType(itemType, 5);
                        f.SetValue(res, list);

                    }

                }
            }
            return res;
        }
        public Task<(T result, int statusCode)> DeleteAsync<T>(string url, Dictionary<string, string> headers, object data, Func<string, string, int, Task> logRequest = null) where T : class
        {
            var urlSegments = headers["Authorization"].Replace("Bearer","").Replace(" ", "").Split(':');
            var res = Response<T>();
            return Task.FromResult((result: urlSegments[0] == Constants.OKAY_CODE || urlSegments[0] == Constants.OKAY_CREATED_CODE ? res : null ,
                statusCode: int.Parse(urlSegments[0])));

        }
        private object CreateListFromType(Type t, int num)
        {
            // Create an array of the required type
            Array values = Array.CreateInstance(t, num);

            // and fill it with values of the required type
            for (int i = 0; i < num; i++)
            {
                if (t == typeof(string))
                {
                    values.SetValue("", i);
                }
                else { 
                    values.SetValue(Activator.CreateInstance(t), i);
                }
            }


            Type genericListType = typeof(List<>);
            Type concreteListType = genericListType.MakeGenericType(t);

            object list = Activator.CreateInstance(concreteListType, new object[] { values });


            return list;
        }
        public  Task<(T result, int statusCode)> GetAsync<T>(string url, Dictionary<string, string> headers, Func<string, string, int, Task> logRequest = null) where T : class
        {
            var urlSegments = url.Split('?');
            if(urlSegments.Length > 1)
            {
                var segs = urlSegments[1].Split("=");
                var invoice = segs[1];
                var res = Response<T>();
                return Task.FromResult((result: invoice == Constants.OKAY_CODE ? res : null,
                    statusCode: int.Parse(Constants.OKAY_CODE)));
            }
            throw new Exception("Error on GET request");
            
        }
       
        public Task<(T result, int statusCode)> PatchAsync<T>(string url,  Dictionary<string, string> headers, object data, Func<string, string, int, Task> logRequest = null) where T : class
        {
            var authInfo = data.GetType().GetProperty("auth_info");
            var sessionId = authInfo.GetType().GetProperty("session_id").ToString();
            var res = Response<T>();
            return Task.FromResult((result: sessionId == Constants.OKAY_CODE || sessionId == Constants.OKAY_CREATED_CODE ? res : null,
                statusCode: int.Parse(sessionId)));
        }

        public Task<(T result, int statusCode)> PostAsync<T>(string url,  Dictionary<string, string> headers, object data, Func<string, string, int, Task> logRequest = null) where T : class
        {
            Type t = data.GetType();
            PropertyInfo authInfoProperty = t.GetProperty("invoice_number");
            object invoice = authInfoProperty.GetValue(data, null);
            var res = Response<T>();
            return Task.FromResult((result: invoice == Constants.OKAY_CODE ? res : null,
                statusCode: int.Parse(invoice.ToString())));
        }

        public Task<(T result, int statusCode)> PutAsync<T>(string url,  Dictionary<string, string> headers, object data, Func<string, string, int, Task> logRequest = null) where T : class
        {
            var urlSegments = headers["Authorization"].Replace("Bearer","").Replace(" ", "").Split(':');
            var res = Response<T>();
            return Task.FromResult((result: urlSegments[0] == Constants.OKAY_CODE || urlSegments[0] == Constants.OKAY_CREATED_CODE ? res : null,
                statusCode: int.Parse(urlSegments[0])));
        }
    }
}
