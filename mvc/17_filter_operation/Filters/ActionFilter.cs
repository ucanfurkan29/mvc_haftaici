using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace _17_filter_operation.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) //İşlem sonrası action çalıştıktan sonra tetiklenir
        {
            Debug.WriteLine("Action executed.");
        }
        public void OnActionExecuting(ActionExecutingContext context) //işlem sırası metodu
                                                                     //action metodunun çalışmasından önce tetiklenir
        {
            Debug.WriteLine("Action executing");
        }

    }
}
