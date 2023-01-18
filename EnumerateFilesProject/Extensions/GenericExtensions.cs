
namespace EnumerateFilesProject.Extensions;

public static class GenericExtensions
{
    /// <summary>
    /// Case sensitive contains on multiple tokens
    /// </summary>
    /// <param name="sender">string to work on</param>
    /// <param name="tokens">string values to check if any exists</param>
    public static bool ContainsAny(this string sender, params string[] tokens)
    {
        foreach (var token in tokens)
        {
            if (sender.Contains(token))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IsNull(this object sender)
    {
        return sender == null || sender == DBNull.Value || Convert.IsDBNull(sender) == true;
    }

    /// <summary>
    /// Is the instance of a class null
    /// </summary>
    /// <typeparam name="T">Concrete class type</typeparam>
    /// <param name="senderInstance">Instance of concrete class</param>
    /// <returns>True if null, false if not null</returns>
    public static bool IsNull<T>(this T senderInstance) where T : new() => senderInstance is null;

    /// <summary>
    /// Is the instance of a class not null
    /// </summary>
    /// <typeparam name="T">Concrete class type</typeparam>
    /// <param name="senderInstance">Instance of concrete class</param>
    /// <returns>True if not null, false if null</returns>
    public static bool IsNotNull<T>(this T senderInstance) where T : new() => !senderInstance.IsNull();

}