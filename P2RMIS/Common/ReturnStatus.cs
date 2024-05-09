
namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// A class to contain the result of an action and its associtated message.
    /// </summary>
    public class ReturnStatus
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }

}